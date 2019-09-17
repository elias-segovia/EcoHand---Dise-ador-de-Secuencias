using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.EventModels
{
    public class SecuenciaSeleccionadaEvent
    {
        public string Accion { get; set; }
        
        public int Id { get; set; }

        public SecuenciaSeleccionadaEvent(int id , string accion)
        {
            Accion = accion;

            Id = id;
        }


    }
}
