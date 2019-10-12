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
    public class SecuenciaDetailsViewModel : Screen
    {




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

        private ILoggedInUser _user;



        public SecuenciaDetailsViewModel(IEventAggregator events, ILoggedInUser user)
        {
            _events = events;
            _events.Subscribe(this);
            _user = user;

            Secuencia = new BindingList<Secuenciable>();

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

        }




    }        
        

    
}
