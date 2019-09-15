using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public  class LoginViewModel: Conductor<object>
    {

        public LoginViewModel()
        {
            
        }
        public void CargarShell()
        {
            ActivateItem(new ShellViewModel());
        }
    }
}
