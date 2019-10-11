using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class DialogEventoViewModel : Screen
    {
        #region Agregar Evento MSj

        private string _mensaje;

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

        public string Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
                NotifyOfPropertyChange(() => Mensaje);
            }
        }

        public DialogEventoViewModel(string msj, int maxPosicion)
        {
            _mensaje = msj;
            _MaxPosicion = maxPosicion;
        }

      

        public void Aceptar()
        {
            IsCancelled = false;

            ValidarEntrada();
            TryClose(true);
        }

        private void ValidarEntrada()
        {
            if (Input < 0)
                Input = 0;
            if (_MaxPosicion == -1)
            {
                //es un evento por tiempo
                
            }
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
