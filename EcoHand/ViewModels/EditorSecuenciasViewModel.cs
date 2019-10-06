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

namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen
    {

        private BindingList<ISecuenciable> _secuencia;

        public BindingList<ISecuenciable> Secuencia
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

        private GestoModel _selectedGesto;

        public GestoModel SelectedGesto
        {
            get
            {
                return _selectedGesto;
            }
            set
            {
                _selectedGesto = value;
                NotifyOfPropertyChange(() => SelectedGesto);

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

        private EventoModel _selectedEvento;

        public EventoModel SelectedEvento
        {
            get
            {
                return _selectedEvento;
            }
            set
            {
                _selectedEvento = value;
                NotifyOfPropertyChange(() => SelectedEvento);

            }
        }


        private IEventAggregator _events;

        private ILoggedInUser _user;

        public EditorSecuenciasViewModel(IEventAggregator events, ILoggedInUser user)
        {
            _events = events;
            _events.Subscribe(this);
            _user = user;
            Secuencia = new BindingList<ISecuenciable>();

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

            Eventos = new BindingList<EventoModel>();

            Eventos.Add(Tiempo);
            Eventos.Add(SaltoFSR);

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

        public void AgregarASecuencia()
        {
            SelectedGesto.Posicion = Secuencia.Count;
            this.Secuencia.Add(SelectedGesto);
        }

        public void AgregarEventoASecuencia()
        {

            SelectedEvento.Posicion = Secuencia.Count;
            //Secuencia.add
        }

        public void EliminarDeSecuencia()
        {
            this.Secuencia.RemoveAt(Secuencia.Count - 1);
            NotifyOfPropertyChange(() => Secuencia);
        }

        public async void GuardarSecuencia()
        {

            APIController.Model.Secuencia s = new APIController.Model.Secuencia();

            s.UsuarioID = _user.Id;
            s.Nombre = Nombre;
            s.FechaCreacion = DateTime.Now;
            s.FechaModificacion = s.FechaModificacion;


            //s.CodigoEstructura = CrearEstructura();
            s.CodigoEstructura = "";

            s.CodigoEjecutable = CrearArduinoCodigo();


            await SecuenciaHandler.Crear(s);
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


            XmlSerializer serializer = new XmlSerializer(Secuencia.GetType(), new Type[] { typeof(GestoModel), typeof(EventoModel) });

            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, Secuencia);

            return stringwriter.ToString();
        }
    }
}
