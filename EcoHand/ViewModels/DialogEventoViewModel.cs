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

        public int Input
        {
            get { return _input; }
            set
            {
                Input = value;
                NotifyOfPropertyChange(() => Input);
            }
        }

        public bool IsCancelled { get; set; }

        public string Mensaje
        {
            get
            {
                return Mensaje;
            }
            set
            {
                _mensaje = value;
                NotifyOfPropertyChange(() => Mensaje);
            }
        }

        public DialogEventoViewModel(string msj)
        {
            _mensaje = msj;
        }

        public DialogEventoViewModel()
        {
        }

        public void Aceptar()
        {

        }

        public void Cancel()
        {

        }

        #endregion

    }
}
