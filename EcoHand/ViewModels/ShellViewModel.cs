using Caliburn.Micro;
using EcoHand.EventModels;
using EcoHand.Handlers;
using EcoHand.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class ShellViewModel : Conductor<object> , IHandle<LogOnEvent>, IHandle<CrearCuentaEvent>
    {



        private bool _EstaLogueado;

        private IEventAggregator _events;

        private SimpleContainer _container;

        public ShellViewModel(SimpleContainer container, IEventAggregator events )
        {
            _events = events;
            events.Subscribe(this);
            _container = container;

            ActivateItem(_container.GetInstance<InicioViewModel>());
        }


        public bool EstaLogueado
        {
            get
            {
                return _EstaLogueado;
            }
            set
            {
                _EstaLogueado = value;
                NotifyOfPropertyChange(() => MenuEsVisible);
            }
        }


        public bool MenuEsVisible
        {
            get
            {
                return EstaLogueado;
            }
        }

        public BindableCollection<GestoModel> Gestos { get; set; }
        
        private async Task<BindableCollection<GestoModel>> CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindableCollection<GestoModel>();
            foreach (var item in resp)
            {
                Gestos.Add(new GestoModel() { Id = item.ID, Nombre = item.Nombre });
            }

            return Gestos;
        }
        public async void LoadListaGesto()
        {

            try
            {
                var resp = await CargarListaDeGestosAsync();

                ActivateItem(new ListadoGestosViewModel(resp));
            }
            catch (Exception e)
            {

                //mensaje no se pudo cargar los gestos
            }


        }

        public void LoadEditor()
        {
            ActivateItem(new EditorDeGestosViewModel());
        }

        public void Handle(LogOnEvent message)
        {
            EstaLogueado = true;
            //puuedo pasar los datos del usuario ACA
            LoadListaGesto();
        }

        public void Handle(CrearCuentaEvent message)
        {
            ActivateItem(_container.GetInstance<RegistroViewModel>());
        }
    }
}
