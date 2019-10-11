using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("Gesto")]
    public class GestoModel : Secuenciable
    {
        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        public DateTime FechaCreacion { get; set; }
        [XmlIgnore]
        public DateTime FechaModificacion { get; set; }
        [XmlElement("Pulgar")]
        public int PosPulgar { get; set; }
        [XmlElement("Indice")]
        public int Posindice { get; set; }
        [XmlElement("Mayor")]
        public int PosMayor { get; set; }
        [XmlElement("Anular")]
        public int PosAnular { get; set; }
        [XmlElement("Meñique")]
        public int PosMeñique { get; set; }
        [XmlIgnore]
        public int UsuarioID { get; set; }

        [XmlIgnore]
        public string Hexa
        {
            get
            {
                return "D1" + (PosPulgar*2).ToString("X2") +
                       "D2" + (Posindice*4).ToString("X2") +
                       "D3" + (PosMayor*4).ToString("X2") +
                       "D4" + (PosAnular*4).ToString("X2") +
                       "D5" + (PosMeñique*4).ToString("X2");
            }
        }
        
    }
    
}
