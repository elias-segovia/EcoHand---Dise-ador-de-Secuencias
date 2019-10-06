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

        public string Nombre { get; set; }

        public TipoEvento Tipo { get; set; }

        public int ValorEntrada { get; set; }

        public string Hexa
        {
            get
            {
                return ObtenerCodigo() + ValorEntrada.ToString("X2");
            }
        }

        public string DisplayPos
        {
            get
            {
                return "[" + Posicion + "] " + Nombre;
            }
        }
        

        private string ObtenerCodigo()
        {
            //segun el tipo tiene un prefijo 
            switch (Tipo)
            {
                case TipoEvento.Tiempo:
                    return "TT";
                case TipoEvento.SaltoFSR:
                    return "S0";
                default: return "";

            }
        }



    }


}

