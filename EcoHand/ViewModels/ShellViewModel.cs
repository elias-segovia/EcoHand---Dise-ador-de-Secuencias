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





        private IEventAggregator _events;

        private SimpleContainer _container;

        private ILoggedInUser _user;

        private string _userName;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public ShellViewModel(SimpleContainer container, IEventAggregator events , ILoggedInUser user)
        {
            _events = events;
            events.Subscribe(this);
            _container = container;
            _user = user;
            UserName = user.UserName;
            
            ActivateItem(_container.GetInstance<ListadoGestosViewModel>());
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
            ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());
        }

        public void LoadEditor(EditarGestoEvent events)
        {
            ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());
            _events.PublishOnUIThread(events);
        }

        public void LogOut()
        {
            var parent = Parent as MainViewModel;
            parent.LoadInicio();
        }

        public void Handle(LogOnEvent message)
        {
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
