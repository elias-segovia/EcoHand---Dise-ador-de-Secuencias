using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoHand.EventModels;
using System.ComponentModel;

namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen
    {

        private IEventAggregator _events;

        public EditorSecuenciasViewModel(IEventAggregator events)
        {
            _events = events;
        }

        private BindingList<string> _secuencia;

        public void AgregarASecuencia()
        {

        }

        public void EliminarDeSecuencia()
        {

        }

        public void GuardarSecuencia()
        {

        }



    }
}
