using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    
    public class EventoModel : ISecuenciable
    {
        public int Posicion { get; set; }
        public int SecuenciaID { get; set; }

        TipoEvento Tipo { get; set; }

        int ValorEntrada { get; set; }

    }


}

