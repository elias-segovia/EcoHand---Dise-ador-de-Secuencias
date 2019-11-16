using Caliburn.Micro;
using EcoHand.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<CrearCuentaEvent>
    {

        private IEventAggregator _events;

        private SimpleContainer _container;

        public void LoadInicio()
        {
            ActivateItem(_container.GetInstance<InicioViewModel>());
        }

        public MainViewModel(SimpleContainer container, IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            _container = container;

            LoadInicio();
        }
        public void Handle(LogOnEvent message)
        {
            ActivateItem(_container.GetInstance<ShellViewModel>());
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
    }
}
