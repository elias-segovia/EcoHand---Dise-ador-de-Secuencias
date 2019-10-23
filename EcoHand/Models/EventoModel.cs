using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{

    [XmlType("Evento")]
    [XmlInclude(typeof(SaltoEvento)), XmlInclude(typeof(TiempoEvento)), XmlInclude(typeof(MoverDedoEvento)), XmlInclude(typeof(TipoEvento))]
    public abstract class EventoModel : Secuenciable
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


        protected abstract string ObtenerValorEnHexa();



        protected abstract string ObtenerCodigo();


        public new string DisplayPos
        {
            get
            {
                return "[" + Posicion + "] " + Nombre + " " + ValorEntrada;
            }
        }

        public abstract EventoModel Clone();



    }


}

