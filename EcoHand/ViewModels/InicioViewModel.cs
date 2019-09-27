using APIController.Model.DTO_IN;
using Caliburn.Micro;
using EcoHand.EventModels;
using EcoHand.Models;
using EcohandBussinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class InicioViewModel : Screen
    {

        private ILoggedInUser _loggedInUser;

        private string _usuario;

        private string _contraseña;

        private IEventAggregator _events;
        public InicioViewModel( IEventAggregator events , ILoggedInUser user)
        {
            _events = events;
            _loggedInUser = user;
        }
        public string Usuario
        {

            get { return _usuario; }
            set
            {
                _usuario = value;
                NotifyOfPropertyChange(() => Usuario);
                NotifyOfPropertyChange(() => PuedeLoguearse);
            }
        }


        public string Contraseña
        {
            get { return _contraseña; }
            set
            {
                _contraseña = value;
                NotifyOfPropertyChange(() => Contraseña);
                NotifyOfPropertyChange(() => PuedeLoguearse);
            }
        }

        string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                NotifyOfPropertyChange(() => Error);
                NotifyOfPropertyChange(() => ErrorEsVisible);
            }
        }



        public bool PuedeLoguearse
        {
            get
            {
                return (Usuario?.Count() > 0 && Contraseña?.Count() > 0) ? true : false;
            }
        }

        public bool ErrorEsVisible
        {
            get
            {
                return Error?.Count() > 0;
            }
        }




        public void CrearCuenta()
        {
            _events.PublishOnUIThread(new CrearCuentaEvent(""));
        }



        public async void Ingresar()
        {
            if (PuedeLoguearse)
            {

                DTO_In_Usuario usuario = new DTO_In_Usuario(Usuario, Contraseña);
                try
                {
                    bool result = await UsuarioHandler.Ingresar(usuario);

                    if (result)
                    {

                        Error = "";

                        _loggedInUser.Id = 1;
                        _loggedInUser.UserName = "pepe";

                        _events.PublishOnUIThread(new LogOnEvent());
                        //ir a  home
                    }
                    else
                    {
                        Error = "El usuario o contraseña no es valido";
                    }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                }
            }
            else
            {
                Error = "Ingrese usuario y contraseña";
            }


        }

    }
}
