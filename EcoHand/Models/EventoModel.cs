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
    public class EventoModel : Secuenciable
    {
        public TipoEvento Tipo { get; set; }

        [XmlElement("Entrada")]
        public int ValorEntrada { get; set; }
        [XmlIgnore]
        public string Hexa
        {
            get
            {
                return ObtenerCodigo() + ObtenerValorEnHexa();
            }
        }


        private string ObtenerValorEnHexa()
        {
            switch (Tipo)
            {
                case TipoEvento.Tiempo:
                    return (25.5 * ValorEntrada).ToString("X2");

                default:
                    return ValorEntrada.ToString("X2");

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
                    return "I1";
                case TipoEvento.SaltoFSRNegativo:
                    return "I0";
                case TipoEvento.SaltoIncondicional:
                    return "XX";
                case TipoEvento.SaltoMusculo:
                    return "S1";
                case TipoEvento.SaltoMusculoRelajado:
                    return "S0";
                default: return "";

            }
        }

        public new string DisplayPos
        {
            get
            {
                return "[" + Posicion + "] " + Nombre + " " + ValorEntrada;
            }
        }

        public EventoModel Clone()
        {
            return new EventoModel()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                Posicion = this.Posicion,
                ValorEntrada = this.ValorEntrada,
                Tipo = this.Tipo
            };
        }



    }


}

