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
using APIController;
using APIController.Model.DTO_IN;
using EcoHand.Handlers;
using EcohandBussinessLogic.Handlers;

namespace EcoHand.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void CrearCuenta(object sender, RoutedEventArgs e)
        {
            RegistroView registro = new RegistroView();
            registro.Show();
            this.Close();
        }

        private async void Ingresar(object sender, RoutedEventArgs e)
        {
            if (Usuario.Text == String.Empty || Contraseña.Password == String.Empty)
                MsjErrorLabel.Visibility = Visibility.Visible;
            else
            {

                DTO_In_Usuario usuario = new DTO_In_Usuario(Usuario.Text, Contraseña.Password);

                bool result = await UsuarioHandler.Ingresar(usuario);

                if (result)
                {
                    MsjErrorLabel.Visibility = Visibility.Hidden;
                    //ShellView shell = new ShellView();
                    //shell.Show();
                    SecuenciaWindow secuencia = new SecuenciaWindow();
                    secuencia.Show();
                    this.Close();
                }
                else
                {
                    MsjErrorLabel.Visibility = Visibility.Visible;
                }
            }


        }

        private void Usuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Usuario.Text == "Ingresar su nombre de usuario")
            {
                Usuario.Text = "";
            }
        }
    }
}
