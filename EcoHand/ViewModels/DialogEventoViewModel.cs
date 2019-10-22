using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class DialogEventoViewModel : Screen , IDialogo
    {
        #region Agregar Evento MSj

        

        private int _input;

        private int _MaxPosicion;

        

        public int Input
        {
            get { return _input; }
            set
            {
                _input = value;
                NotifyOfPropertyChange(() => Input);
            }
        }

        public bool IsCancelled { get; set; }

        public bool IsAccepted { get; set; }


        public DialogEventoViewModel(int maxPosicion)
        {
            
            _MaxPosicion = maxPosicion;
        }

      

        public void Aceptar()
        {
            IsCancelled = false;

            ValidarEntrada();

            TryClose(true);

            IsAccepted = true;
        }

        private void ValidarEntrada()
        {
            if (Input < 0)
                Input = 0;
     
            else
            {
                //es por salto
                Input = Input > _MaxPosicion ? _MaxPosicion : Input;
            }
        }

        public void Cancel()
        {
            IsCancelled = true;
            this.TryClose(false);
        }

        #endregion

    }
}
