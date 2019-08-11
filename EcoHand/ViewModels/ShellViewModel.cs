using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {

        public ShellViewModel()
        {
            LoadMain();
        }
        public void  LoadMain()
        {
            ActivateItem(new EditorDeGestosViewModel());
        }
    }
}
