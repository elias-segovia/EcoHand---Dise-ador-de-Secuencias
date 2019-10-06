using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public class GestoModel : ISecuenciable
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int PosPulgar { get; set; }

        public int Posindice { get; set; }

        public int PosMayor { get; set; }

        public int PosAnular { get; set; }

        public int PosMeñique { get; set; }

        public int UsuarioID { get; set; }
        public int Posicion { get ; set; }
        public int SecuenciaID { get; set; }

        public string Hexa
        {
            get
            {
                return "D1" + PosPulgar.ToString("X2") +
                       "D2" + Posindice.ToString("X2") +
                       "D3" + PosMayor.ToString("X2") +
                       "D4" + PosAnular.ToString("X2") +
                       "D5" + PosMeñique.ToString("X2");
            }
        }

        public string DisplayPos
        {
            get
            {
                return "[" + Posicion + "] " + Nombre;
            }
        }
    }
    
}
