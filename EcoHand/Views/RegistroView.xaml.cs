using APIController.Model.DTO_IN;
using EcohandBussinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EcoHand.Views
{
    /// <summary>
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : Window
    {
        public RegistroView()
        {
            InitializeComponent();
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private async void CrearCuenta(object sender, RoutedEventArgs e)
        {
            if (Usuario.Text == String.Empty || Correo.Text == String.Empty || Contraseña.Password == String.Empty)
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            if (!IsValidEmail(Correo.Text))
                ErrorMailLabel.Visibility = Visibility.Visible;
            else
            {
                ErrorMailLabel.Visibility = Visibility.Hidden;
                ErrorLabel.Visibility = Visibility.Hidden;
                ErrorUsuarioExistente.Visibility = Visibility.Hidden;

                DTO_In_Usuario usuario = new DTO_In_Usuario(Usuario.Text, Contraseña.Password, Correo.Text);

                bool result = await UsuarioHandler.Registrar(usuario);

                if (result)
                {
                    ShellView shell = new ShellView();
                    shell.Show();
                    this.Close();
                }
                else
                {
                    ErrorUsuarioExistente.Visibility = Visibility.Visible;
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Usuario.Text == "Ingresar un nombre de usuario")
            {
                Usuario.Text = "";
            }
        }

        private void Correo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Correo.Text == "Ingresar un correo electrónico")
            {
                Correo.Text = "";
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
