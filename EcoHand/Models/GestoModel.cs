using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("Gesto")]
    public class GestoModel : ISecuenciable
    {
        [XmlElement("Id")]
        public int ID { get; set; }
        [XmlElement("Nombre")]
        public string Nombre { get; set; }
        [XmlElement("Descripcion")]
        public string Descripcion { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        
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

        public int UsuarioID { get; set; }

        [XmlElement("Posicion")]
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
