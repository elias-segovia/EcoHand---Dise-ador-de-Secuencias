using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("TipoEvento")]
    public enum TipoEvento
    {
        [XmlEnum(Name = "Tiempo")]
        Tiempo, 
        [XmlEnum(Name ="EMG1")]
        SaltoMusculo1,
        SaltoMusculo2,
        SaltoIncondicional,
        [XmlEnum(Name ="FSR")]
        SaltoFSR

    }
}
