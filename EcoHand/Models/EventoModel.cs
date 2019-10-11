using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{

    [XmlType("Evento")]
    [XmlInclude(typeof(TipoEvento))]
    public class EventoModel : ISecuenciable
    {
        [XmlElement("Posicion")]
        public int Posicion { get; set; }
        public int SecuenciaID { get; set; }
        [XmlElement("Nombre")]
        public string Nombre { get; set; }
        [XmlElement("Tipo")]
        public TipoEvento Tipo { get; set; }
        [XmlElement("Entrada")]
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

