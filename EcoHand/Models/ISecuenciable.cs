using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public interface ISecuenciable
    {
         int Posicion { get; set; }
         
         int SecuenciaID { get; set; }

        string DisplayPos { get;  }
        

    }
}
