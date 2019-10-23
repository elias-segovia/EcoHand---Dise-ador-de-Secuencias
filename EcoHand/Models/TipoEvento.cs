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
        [XmlEnum(Name = "EMG0")]
        SaltoMusculo,
        [XmlEnum(Name = "EMG1")]
        SaltoMusculoRelajado,
        [XmlEnum(Name = "Incondicional")]
        SaltoIncondicional,
        [XmlEnum(Name = "FSR")]
        SaltoFSR,
        [XmlEnum(Name = "FSRNeg")]
        SaltoFSRNegativo,
        [XmlEnum(Name = "D1")]
        MoverPulgar,
        [XmlEnum(Name = "D2")]
        MoverIndice,
        [XmlEnum(Name = "D3")]
        MoverMayor,
        [XmlEnum(Name = "D4")]
        MoverAnular,
        [XmlEnum(Name = "D5")]
        MoverMeñique
    }
}
