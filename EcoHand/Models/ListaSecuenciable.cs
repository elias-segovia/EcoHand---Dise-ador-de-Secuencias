using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlRoot("Secuencia")]
    [XmlInclude((typeof(Secuenciable)))]
    public class ListaSecuenciable
    {
        [XmlArray("Elementos")]
        [XmlArrayItem("SecuItem")]
        public List<Secuenciable> ElementosDeSecuencia { get; set; }

    }
}
