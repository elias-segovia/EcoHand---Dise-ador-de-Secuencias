using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("SecuenciaItem")]
    [XmlInclude(typeof(EventoModel)), XmlInclude(typeof(GestoModel))]
    public class Secuenciable
    {
        [XmlElement("Posicion")]
        public int Posicion { get; set; }
        
        [XmlElement("Nombre")]
        public string Nombre { get; set; }

        [XmlElement("Descripcion")]
        public string Descripcion { get; set; }

        [XmlIgnore]
        public string DisplayPos
        {
            get
            {
                return "[" + Posicion + "] " + Nombre;
            }
        }

        
        
    }
}
