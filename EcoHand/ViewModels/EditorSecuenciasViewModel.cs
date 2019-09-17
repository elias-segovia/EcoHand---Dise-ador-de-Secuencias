using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoHand.EventModels;

namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen
    {

        private IEventAggregator _events;

        public EditorSecuenciasViewModel(IEventAggregator events)
        {
            _events = events;
        }

    }
}
