using APIController.Model.DTO_IN;
using Caliburn.Micro;
using EcohandBussinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class RegistroViewModel : Screen
    {
        private string _usuario;

        private string _contraseña;

        private string _email;

        public RegistroViewModel()
        {

        }

        public string Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                NotifyOfPropertyChange(() => Usuario);
            }
        }

        public string Contraseña
        {
            get { return _contraseña; }
            set
            {
                _contraseña = value;
                NotifyOfPropertyChange(() => Contraseña);
            }
        }

        public string Email
        {
            get { return _usuario; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public async Task CrearUsuario()
        {
            DTO_In_Usuario usuario = new DTO_In_Usuario(Usuario, Contraseña, Email);

            bool result = await UsuarioHandler.Registrar(usuario);

        }

    }
}
