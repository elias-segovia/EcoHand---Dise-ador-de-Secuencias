using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.EventModels
{
    public class GestoSeleccionadoEvent
    {

        //Acciones:
        //Ver
        //Editar
        //Eliminar
        public int GestoId { get; set; }

        public string Accion { get; set; }

        public GestoSeleccionadoEvent(int id, string accion)
        {
            GestoId = id;
            Accion = accion;
        }

    }
}
