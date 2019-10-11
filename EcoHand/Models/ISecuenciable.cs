using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("SecuenciaItem")]
    [XmlInclude(typeof(EventoModel)),XmlInclude(typeof(GestoModel))]
    public interface ISecuenciable
    {
         int Posicion { get; set; }
         
         int SecuenciaID { get; set; }

        string DisplayPos { get;  }
        

    }
}
