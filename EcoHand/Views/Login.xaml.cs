﻿using System;
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

        private void Ingresar(object sender, RoutedEventArgs e)
        {


            ShellView shell = new ShellView();
            shell.Show();
            this.Close();
        }
    }
}
