using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.EventModels
{

    public class CrearCuentaEvent
    {
        public string Msg { get; set; }

        public CrearCuentaEvent(string msg)
        {
            Msg = msg;
        }

    }
}
