using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public class Evento : ISecuenciable
    {
        public int Posicion { get; set; }
        public int SecuenciaID { get; set; }


    }
}
