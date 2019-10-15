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
    public class SecuenciaDetailsViewModel : Screen, IHandle<VerSecuenciaEvent>
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



        private IEventAggregator _events;

        public SecuenciaDetailsViewModel(IEventAggregator events )
        {
            _events = events;
            _events.Subscribe(this);
            
            Secuencia = new BindingList<Secuenciable>();

        }

        

        public void Handle(VerSecuenciaEvent message)
        {
            if (message.Secuencia == null) return;
            _secuenciaModel = message.Secuencia;
            Secuencia = ReconstruirSecuencia(_secuenciaModel.XmlCode);
            Nombre = _secuenciaModel.Nombre;
            Descripcion = _secuenciaModel.Descripcion;
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
