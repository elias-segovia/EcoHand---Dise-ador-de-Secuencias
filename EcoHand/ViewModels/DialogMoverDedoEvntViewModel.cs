using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class DialogMoverDedoEvntViewModel : Screen , IDialogo
    {
        #region Agregar Evento MSj

        private int _input;

      

        public int Input
        {
            get { return _input; }
            set
            {
                _input = value;
                NotifyOfPropertyChange(() => Input);
                NotifyOfPropertyChange(() => InputShow);
            }
        }

        public string InputShow
        {
            get { return "" +_input + "°"; }
        }

        public bool IsCancelled { get; set; }

        public bool IsAccepted { get; set; }


      
        public void Aceptar()
        {
            IsCancelled = false;
            IsAccepted = true;
            TryClose(true);
        }

   

        public void Cancel()
        {
            IsCancelled = true;
            this.TryClose(false);
        }

        #endregion

    }
}
