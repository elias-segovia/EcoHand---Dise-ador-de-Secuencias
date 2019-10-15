using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoHand.EventModels;
using System.ComponentModel;
using EcoHand.Models;
using EcoHand.Handlers;
using EcohandBussinessLogic.Handlers;
using System.Xml.Serialization;
using System.Dynamic;
using System.Windows;

namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen , IHandle<EditarSecuenciaEvent>
    {


        private SecuenciaModel _secuenciaModel;

        private BindingList<Secuenciable> _secuencia;

        public BindingList<Secuenciable> Secuencia
        {
            get { return _secuencia; }
            set
            {
                _secuencia = value;
                NotifyOfPropertyChange(() => Secuencia);

            }
        }

        private string _nombre;

        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                NotifyOfPropertyChange(() => Nombre);
            }
        }

        private string _descripcion;

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                NotifyOfPropertyChange(() => Descripcion);
            }
        }

        private bool _eventoAgregado;

        public bool EventoAgregado
        {

            get
            {
                return _eventoAgregado;
            }
            set
            {
                _eventoAgregado = value;
                NotifyOfPropertyChange(() => EventoAgregado);
            }
        }

        private BindingList<GestoModel> _gestos;

        public BindingList<GestoModel> Gestos
        {
            get { return _gestos; }
            set
            {
                _gestos = value;
                NotifyOfPropertyChange(() => Gestos);

            }
        }

        private Secuenciable _selectedITem;

        public Secuenciable SelectedItem
        {
            get
            {
                return _selectedITem;
            }
            set
            {
                _selectedITem = value;
                NotifyOfPropertyChange();
            }
        }


        private BindingList<EventoModel> _eventos;

        public BindingList<EventoModel> Eventos
        {
            get { return _eventos; }
            set
            {
                _eventos = value;
                NotifyOfPropertyChange(() => Eventos);

            }
        }



        private IEventAggregator _events;

        private ILoggedInUser _user;
        private bool Editando ;

        public EditorSecuenciasViewModel(IEventAggregator events, ILoggedInUser user)
        {
            _events = events;
            _events.Subscribe(this);
            _user = user;

            Secuencia = new BindingList<Secuenciable>();

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await CargarListaDeGestosAsync();
            CargarListaDeEventos();

        }

        private void CargarListaDeEventos()
        {
            EventoModel Tiempo = new EventoModel()
            {
                Tipo = TipoEvento.Tiempo,
                Nombre = "TIEMPO DE ESPERA",

            };
            EventoModel SaltoFSR = new EventoModel()
            {
                Tipo = TipoEvento.SaltoFSR,
                Nombre = "Salto Por FSR"
            };
            EventoModel SaltoFSRNegativo = new EventoModel()
            {
                Tipo = TipoEvento.SaltoFSRNegativo,
                Nombre = "Salto Por FSR falso"
            };
            EventoModel SaltoIncondicional = new EventoModel()
            {
                Tipo = TipoEvento.SaltoIncondicional,
                Nombre = "Salto Incondicional"
            };

            Eventos = new BindingList<EventoModel>();

            Eventos.Add(Tiempo);
            Eventos.Add(SaltoFSR);
            Eventos.Add(SaltoIncondicional);
            Eventos.Add(SaltoFSRNegativo);

        }

        private async Task CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindingList<GestoModel>();
            foreach (var item in resp)
            {
                Gestos.Add(new GestoModel()
                {
                    Descripcion = item.Descripcion,
                    FechaCreacion = item.FechaCreacion,
                    FechaModificacion = item.FechaModificacion,
                    ID = item.ID,
                    Nombre = item.Nombre,
                    PosAnular = item.PosAnular,
                    Posindice = item.Posindice,
                    PosMayor = item.PosMayor,
                    PosMeñique = item.PosMeñique,
                    PosPulgar = item.PosPulgar,
                    UsuarioID = item.UsuarioID
                });
            }
        }

        public void  AgregarASecuencia()
        {

            if (SelectedItem == null) return;

            SelectedItem.Posicion = Secuencia.Count();

            if (SelectedItem.GetType().Name.StartsWith("EventoModel"))
            {
                var aux = SelectedItem as EventoModel;

                int max;

                WindowManager windowManager = new WindowManager();
                dynamic settings = new ExpandoObject();
                settings.WindowStyle = WindowStyle.ThreeDBorderWindow;
                settings.Title = "Agregar Evento";
                settings.WindowState = WindowState.Normal;
                settings.ResizeMode = ResizeMode.NoResize;
                IDialogo dialogo = null;

                if (aux.Tipo == TipoEvento.Tiempo)
                {
                    dialogo = new DialogEventoTiempoViewModel();
                }
                else
                {
                    max = Secuencia.Count - 1;
                    dialogo = new DialogEventoViewModel(max);

                }


                windowManager.ShowDialog(dialogo, null, settings);

                if (dialogo.IsCancelled)
                {
                    // Handle 
                    return;
                }
                aux.ValorEntrada = dialogo.Input;

                Secuencia.Add(aux.Clone());
            }
            else
            {
                var gesto = SelectedItem as GestoModel;
                Secuencia.Add(gesto.Clone());
            }
            


        }



        public void EliminarDeSecuencia()
        {
            if (Secuencia.Count == 0) return;
            this.Secuencia.RemoveAt(Secuencia.Count - 1);
            NotifyOfPropertyChange(() => Secuencia);
        }

        public async void GuardarSecuencia()
        {

            APIController.Model.Secuencia s = new APIController.Model.Secuencia();
                       

            s.UsuarioID = _user.Id;
            s.Nombre = Nombre;
            s.Descripcion = Descripcion;
            s.FechaModificacion = s.FechaModificacion;
            s.CodigoEstructura = CrearEstructura();

            s.CodigoEjecutable = CrearArduinoCodigo();

            if (Editando)
            {
                s.FechaCreacion = _secuenciaModel.FechaCreacion;
                s.ID = _secuenciaModel.ID;
                await SecuenciaHandler.Editar(s);
            }
            else
            {
                s.FechaCreacion = DateTime.Now;
                await SecuenciaHandler.Crear(s);
            }

            LoadListaSecuencias();
            
        }

        private void LoadListaSecuencias()
        {
            var parent = this.Parent as ShellViewModel;
            parent.LoadSecuencias();
        }

        private string CrearArduinoCodigo()
        {
            string codigoArduino = "";

            foreach (var item in Secuencia)
            {
                var type = item.GetType();

                if (type.Name.StartsWith("Gesto"))
                {
                    var i = item as GestoModel;

                    codigoArduino += i.Hexa;

                }
                else
                {

                    var e = item as EventoModel;

                    codigoArduino += e.Hexa;
                }
            }

            return codigoArduino;
        }

        private string CrearEstructura()
        {

            ListaSecuenciable list = new ListaSecuenciable();

            list.ElementosDeSecuencia = Secuencia.ToList();

            // Serialize 
            Type[] types = { typeof(Secuenciable), typeof(GestoModel), typeof(EventoModel), typeof(TipoEvento) };
            XmlSerializer serializer = new XmlSerializer(typeof(ListaSecuenciable), types);
            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, list);




            return stringwriter.ToString();
        }

        public void MostrarMensajeEvento(string msj)
        {
            EventoAgregado = true;

        }

        public void Handle(EditarSecuenciaEvent message)
        {
            _secuenciaModel = message.Secuencia;
            Nombre = _secuenciaModel.Nombre;
            Descripcion = _secuenciaModel.Descripcion;

            Editando = true;

            Secuencia = ReconstruirSecuencia(_secuenciaModel.XmlCode);
        }

        private BindingList<Secuenciable> ReconstruirSecuencia(string xmlCode)
        {
            Type[] types = { typeof(Secuenciable), typeof(GestoModel), typeof(EventoModel), typeof(TipoEvento) };
            XmlSerializer serializer = new XmlSerializer(typeof(ListaSecuenciable), types);
            System.IO.StringReader reader = new System.IO.StringReader(xmlCode); 
            var list = (ListaSecuenciable)serializer.Deserialize(reader);

            return new BindingList<Secuenciable>(list.ElementosDeSecuencia);

        }
    }
}
