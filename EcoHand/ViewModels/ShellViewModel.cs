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
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<CrearCuentaEvent>
    {



        private bool _EstaLogueado;

        private IEventAggregator _events;

        private SimpleContainer _container;

        public ShellViewModel(SimpleContainer container, IEventAggregator events)
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

        
        public  void LoadListaGesto()
        {
           
           ActivateItem(_container.GetInstance<ListadoGestosViewModel>());
           
        }

        public void LoadSecuencias()
        {

            ActivateItem(_container.GetInstance<ListadoSecuenciasViewModel>());

        }


        public void LoadEditorSecuencias()
        {
            ActivateItem(_container.GetInstance<EditorSecuenciasViewModel>());
        }

        public void LoadEditor()
        {
            // ActivateItem(new EditorDeGestosViewModel());
            ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());
        }

        public void LoadEditor(EditarGestoEvent events)
        {
            // ActivateItem(new EditorDeGestosViewModel());
            ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());
            _events.PublishOnUIThread(events);
        }

        public void Handle(LogOnEvent message)
        {
            EstaLogueado = true;
            //puuedo pasar los datos del usuario ACA
            LoadListaGesto();
        }

        public void Handle(CrearCuentaEvent message)
        {
   

            if (message.Msg.Equals("NuevoUsuarioOk") || message.Msg.Equals("NuevoUsuarioCancelar"))
            {
                ActivateItem(_container.GetInstance<InicioViewModel>());
            }
            if (String.IsNullOrEmpty(message.Msg))
            {
                ActivateItem(_container.GetInstance<RegistroViewModel>());
            }
        }

        internal void LoadEditorSecuencias(EditarSecuenciaEvent evento)
        {
            ActivateItem(_container.GetInstance<EditorSecuenciasViewModel>());
            _events.PublishOnUIThread(evento);
        }
    }
}
