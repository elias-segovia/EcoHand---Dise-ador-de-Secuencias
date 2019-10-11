using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public interface IDialogo
    {
        bool IsCancelled { get; set; }
        int Input { get; set; }
    }
}
