using APIController.Model.DTO_IN;
using Caliburn.Micro;
using EcoHand.EventModels;
using EcohandBussinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class RegistroViewModel : Screen
    {
        private string _usuario;

        private string _contraseña;

        private string _email;

        private string _errorEmail;

        private string _errorUsuario;

        private string _errorContraseña;

        private IEventAggregator _events;

        public RegistroViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public string ErrorEmail
        {
            get
            {
                return _errorEmail;
            }
            set
            {
                _errorEmail = value;
                NotifyOfPropertyChange(() => ErrorEmail);
            }
        }

        public string ErrorUsuario
        {
            get
            {
                return _errorUsuario;
            }
            set
            {
                _errorUsuario = value;
                NotifyOfPropertyChange(() => ErrorUsuario);
            }
        }

        public string ErrorContraseña
        {
            get
            {
                return _errorContraseña;
            }
            set
            {
                _errorContraseña = value;
                NotifyOfPropertyChange(() => ErrorContraseña);
            }
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
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public bool EsEmailValido(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                ErrorEmail = "e-Mail Invalido";
                return false;
            }
        }

        public void Cancelar()
        {
            _events.PublishOnUIThread(new CrearCuentaEvent("NuevoUsuarioCancelar"));
        }
        public async Task CrearUsuario()
        {
            if (!ValidarDatos()) return;


            DTO_In_Usuario usuario = new DTO_In_Usuario(Usuario, Contraseña, Email);

            bool result = await UsuarioHandler.Registrar(usuario);

            if (!result)
            {

                ErrorUsuario = "Error al crear el usuario";
            }
            else
            {
                _events.PublishOnUIThread(new CrearCuentaEvent("NuevoUsuarioOk"));
            }


        }

        private bool ValidarDatos()
        {

            int i = 0;

            if (String.IsNullOrEmpty(Usuario))
            {
                ErrorUsuario = "Ingrese un nombre de usuario";
                i++;
            }
            else
            {
                ErrorUsuario = "";
            }
            if (String.IsNullOrEmpty(Email))
            {
                ErrorEmail = "Ingrese una direccion de correo valida";
                i++;
            }
            else
            {
                if (!EsEmailValido(Email))
                {
                    ErrorEmail = "La direccion de correo no es valida";
                    i++;
                }
                else
                {
                    ErrorEmail = "";
                }
            }
            if (String.IsNullOrEmpty(Contraseña))
            {
                ErrorContraseña = "Ingrese una contraseña";
                i++;
            }
            else
            {
                ErrorContraseña = "";
            }

            return i == 0;


        }
    }
}
