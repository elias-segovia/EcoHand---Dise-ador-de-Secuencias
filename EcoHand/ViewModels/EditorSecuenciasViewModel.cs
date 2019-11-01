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
    public class EditorSecuenciasViewModel : Screen, IHandle<EditarSecuenciaEvent>
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

        private Secuenciable _selectedFromSecuencia;

        public Secuenciable SelectedFromSecuencia
        {
            get { return _selectedFromSecuencia; }
            set
            {
                _selectedFromSecuencia = value;
                NotifyOfPropertyChange(() => SelectedFromSecuencia);
                NotifyOfPropertyChange(() => CanEliminarDeSecuencia);
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

        // private Secuenciable _selectedITem;

        public Secuenciable SelectedItem
        {
            get
            {
                if (SelectedGesto != null)
                    return SelectedGesto;
                if (SelectedEvento != null)
                    return SelectedEvento;
                return null;
            }

        }



        private GestoModel _selectedGesto;

        public GestoModel SelectedGesto
        {
            get
            {
                return _selectedGesto;
            }
            set
            {
                //fix choto pero parece q anda
                _selectedEvento = null;
                NotifyOfPropertyChange(() => SelectedEvento);
                _selectedGesto = value;
                NotifyOfPropertyChange(() => SelectedGesto);


            }
        }

        private EventoModel _selectedEvento;

        public EventoModel SelectedEvento
        {
            get
            {
                return _selectedEvento;
            }
            set
            {
                _selectedGesto = null;
                NotifyOfPropertyChange(() => SelectedGesto);
                _selectedEvento = value;

                NotifyOfPropertyChange(() => SelectedEvento);


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
        private bool Editando;

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
            EventoModel Tiempo = new TiempoEvento()
            {
                Tipo = TipoEvento.Tiempo,
                Nombre = "TIEMPO DE ESPERA",

            };
            EventoModel SaltoFSR = new SaltoEvento()
            {
                Tipo = TipoEvento.SaltoFSR,
                Nombre = "Salto Por FSR"
            };
            EventoModel SaltoFSRNegativo = new SaltoEvento()
            {
                Tipo = TipoEvento.SaltoFSRNegativo,
                Nombre = "Salto Por FSR falso"
            };
            EventoModel SaltoIncondicional = new SaltoEvento()
            {
                Tipo = TipoEvento.SaltoIncondicional,
                Nombre = "Salto Incondicional"
            };
            EventoModel MoverPulgar = new MoverDedoEvento()
            {
                Tipo = TipoEvento.MoverPulgar,
                Nombre = "Mover Pulgar"
            };
            EventoModel MoverIndice = new MoverDedoEvento()
            {
                Tipo = TipoEvento.MoverIndice,
                Nombre = "Mover Indice"
            };
            EventoModel MoverMayor = new MoverDedoEvento()
            {
                Tipo = TipoEvento.MoverMayor,
                Nombre = "Mover Mayor"
            };
            EventoModel MoverAnular = new MoverDedoEvento()
            {
                Tipo = TipoEvento.MoverAnular,
                Nombre = "Mover Anular"
            };
            EventoModel MoverMeñique = new MoverDedoEvento()
            {
                Tipo = TipoEvento.MoverMeñique,
                Nombre = "Mover Meñique"
            };

            Eventos = new BindingList<EventoModel>();

            Eventos.Add(Tiempo);
            Eventos.Add(SaltoFSR);
            Eventos.Add(SaltoIncondicional);
            Eventos.Add(SaltoFSRNegativo);
            Eventos.Add(MoverPulgar);
            Eventos.Add(MoverIndice);
            Eventos.Add(MoverMayor);
            Eventos.Add(MoverAnular);
            Eventos.Add(MoverMeñique);

        }

        private async Task CargarListaDeGestosAsync()
        {
            try
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
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error al cargar secuencias", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void AgregarASecuencia()
        {

            if (SelectedItem == null) return;

            SelectedItem.Posicion = Secuencia.Count();

            Type itemType = SelectedItem.GetType();

            if (itemType == typeof(GestoModel))
            {
                var gesto = SelectedItem as GestoModel;
                Secuencia.Add(gesto.Clone());
                return;
            }

            WindowManager windowManager = new WindowManager();
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ThreeDBorderWindow;
            settings.Title = "Agregar Evento";
            settings.WindowState = WindowState.Normal;
            settings.ResizeMode = ResizeMode.NoResize;
            IDialogo dialogo = null;

            EventoModel aux = null;

            if (itemType == typeof(TiempoEvento))
            {
                aux = SelectedItem as TiempoEvento;
                dialogo = new DialogEventoTiempoViewModel();
            }

            if (itemType == typeof(MoverDedoEvento))
            {
                aux = SelectedItem as MoverDedoEvento;
                dialogo = new DialogMoverDedoEvntViewModel();
            }

            if (itemType == typeof(SaltoEvento))
            {
                aux = SelectedItem as SaltoEvento;

                dialogo = new DialogEventoViewModel();
            }




            windowManager.ShowDialog(dialogo, null, settings);

            if (!dialogo.IsAccepted)
            {
                // Handle 
                return;
            }
            aux.ValorEntrada = dialogo.Input;

            Secuencia.Add(aux.Clone());


        }


        public bool CanEliminarDeSecuencia
        {
            get { return SelectedFromSecuencia != null; }
        }
        public void EliminarDeSecuencia()
        {
            for (int i = SelectedFromSecuencia.Posicion; i < Secuencia.Count; i++)
            {
                var secuenciable = Secuencia.ElementAt(i);
                secuenciable.Posicion = i - 1;
                
            }

            this.Secuencia.Remove(SelectedFromSecuencia);

            Secuencia = new BindingList<Secuenciable>(Secuencia);

            NotifyOfPropertyChange(() => Secuencia);
            
        }

        public void Cancelar()
        {
            var shell = this.Parent as ShellViewModel;

            shell.LoadSecuencias();
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

            try
            {

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
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error al guardar secuencia", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

            try
            {
                Type[] types = { typeof(Secuenciable), typeof(GestoModel), typeof(EventoModel), typeof(TipoEvento) };
                XmlSerializer serializer = new XmlSerializer(typeof(ListaSecuenciable), types);
                System.IO.StringReader reader = new System.IO.StringReader(xmlCode);
                var list = (ListaSecuenciable)serializer.Deserialize(reader);

                return new BindingList<Secuenciable>(list.ElementosDeSecuencia);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al obtener secuencia", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

        }
    }
}
