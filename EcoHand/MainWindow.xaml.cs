﻿using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Net.Http;
//using Newtonsoft.Json;
//using EcoHand.Handlers;
//using APIController.Model;

namespace EcoHand
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //modelo de la mano agrupando todas las partes
        Model3DGroup hand;

        //caja 3d para tener un punto de referencia
        BoxVisual3D mybox = new BoxVisual3D();

        //partes de la mano
        Model3D mano_sin_dedos;
        Model3D pulgar_proximal;
        Model3D pulgar_distal;
        Model3D indice_proximal;
        Model3D indice_distal;
        Model3D mayor_proximal;
        Model3D mayor_distal;
        Model3D anular_proximal;
        Model3D anular_distal;
        Model3D meñique_proximal;
        Model3D meñique_distal;

        #region Helper_Box_properties
        public double boxheigth
        {
            get { return mybox.Height; }
            set { mybox.Height = value; }
        }

        public double boxwidth
        {
            get { return mybox.Width; }
            set { mybox.Width = value; }
        }

        public double boxlength
        {
            get { return mybox.Length; }
            set { mybox.Length = value; }
        }


        public double boxX
        {
            get { return mybox.Center.X; }
            set { mybox.Center = new Point3D(value, mybox.Center.Y, mybox.Center.Z); }
        }


        public double boxY
        {
            get { return mybox.Center.Y; }
            set { mybox.Center = new Point3D(mybox.Center.X, value, mybox.Center.Z); }
        }

        public double boxZ
        {
            get { return mybox.Center.Z; }
            set { mybox.Center = new Point3D(mybox.Center.X, mybox.Center.Y, value); }
        }

        #endregion

        #region function
        //property for the pulgar proximal movement
        int m_pulgar_proximal_angle;
        public int pulgar_proximal_angle
        {
            get { return m_pulgar_proximal_angle; }
            set
            {
                //move_proximal(value, "pulgar", new Vector3D(1, 0.4, 0), new Point3D(-0.2, 0, 1.5));
                move_proximal(value, "pulgar", new Vector3D(-1, 0, 0), new Point3D(13 , 71, 4));
                //move_distal(value, "pulgar", new Point3D(0.7, 0.5, 1.9), new Vector3D(1, 0.4, 0));
                m_pulgar_proximal_angle = value;
            }
        }

        ////property for the pulgar distal movement
        //int m_pulgar_distal_angle;
        //public int pulgar_distal_angle
        //{
        //    get { return m_pulgar_distal_angle; }
        //    set
        //    {
        //        move_distal(value, "pulgar", new Point3D(0.7, 0.5, 1.9), new Vector3D(1, 0.4, 0));
        //        m_pulgar_distal_angle = value;
        //    }
        //}

        int m_indice_proximal_angle;
        public int indice_proximal_angle
        {
            get { return m_indice_proximal_angle; }
            set
            {
                //move_proximal(value, "indice", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //move_distal(value, "indice", new Point3D(0.5, 0, 2.5), new Vector3D(0, 1, 0));
                move_proximal(value, "indice", new Vector3D(0, 1, 0), new Point3D(84, 74, 9));
                move_distal(value, "indice", new Point3D(123.5, 74, 8.5), new Vector3D(0, 1, 0));
                m_indice_proximal_angle = value;
            }
        }

        ////property for the indice distal movement
        //int m_indice_distal_angle;
        //public int indice_distal_angle
        //{
        //    get { return m_indice_distal_angle; }
        //    set
        //    {
        //        move_distal(value, "indice", new Point3D(0.5, 0, 2.5), new Vector3D(0, 1, 0));
        //        m_indice_distal_angle = value;
        //    }
        //}

        int m_mayor_proximal_angle;
        public int mayor_proximal_angle
        {
            get { return m_mayor_proximal_angle; }
            set
            {
                //move_proximal(value, "mayor", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //move_distal(value, "mayor", new Point3D(0.6, -0.13, 2.545), new Vector3D(0, 1, 0));
                move_proximal(value, "mayor", new Vector3D(0, 1, 0), new Point3D(84, 54, 9 ));
                move_distal(value, "mayor", new Point3D(122.5, 54, 8), new Vector3D(0, 1, 0));
                m_mayor_proximal_angle = value;
            }
        }

        ////property for the mayor distal movement
        //int m_mayor_distal_angle;
        //public int mayor_distal_angle
        //{
        //    get { return m_mayor_distal_angle; }
        //    set
        //    {
        //        move_distal(value, "mayor", new Point3D(0.6, -0.13, 2.545), new Vector3D(0, 1, 0));
        //        m_mayor_distal_angle = value;
        //    }
        //}

        int m_anular_proximal_angle;
        public int anular_proximal_angle
        {
            get { return m_anular_proximal_angle; }
            set
            {
                //move_proximal(value, "anular", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //move_distal(value, "anular", new Point3D(0.66, -0.388, 2.47), new Vector3D(0, 1, 0));
                move_proximal(value, "anular", new Vector3D(0, 1, 0), new Point3D(84, 34, 9));
                move_distal(value, "anular", new Point3D(123.5, 34, 8), new Vector3D(0, 1, 0));
                m_anular_proximal_angle = value;
            }
        }

        ////property for the anular distal movement
        //int m_anular_distal_angle;
        //public int anular_distal_angle
        //{
        //    get { return m_anular_distal_angle; }
        //    set
        //    {
        //        move_distal(value, "anular", new Point3D(0.66, -0.388, 2.47), new Vector3D(0, 1, 0));
        //        m_anular_distal_angle = value;
        //    }
        //}

        int m_meñique_proximal_angle;
        public int meñique_proximal_angle
        {
            get { return m_meñique_proximal_angle; }
            set
            {
                //move_proximal(value, "meñique", new Vector3D(0, 1, 0), new Point3D(0.66, -0.54, 2.08));
                //move_distal(value, "meñique", new Point3D(0.674, -0.61, 2.27), new Vector3D(0, 1, 0));
                move_proximal(value, "meñique", new Vector3D(0, 1, 0), new Point3D(84, 14, 9));
                move_distal(value, "meñique", new Point3D(113.5, 14, 9), new Vector3D(0, 1, 0));
                m_meñique_proximal_angle = value;
            }
        }

        ////property for the meñique distal movement
        //int m_meñique_distal_angle;
        //public int meñique_distal_angle
        //{
        //    get { return m_meñique_distal_angle; }
        //    set
        //    {
        //        move_distal(value, "meñique", new Point3D(0.674, -0.61, 2.27), new Vector3D(0, 1, 0));
        //        m_meñique_distal_angle = value;
        //    }
        //}
        #endregion



        //Property for the binding with the hand
        public Model3D our_Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //The Importer to load .obj files
            ModelImporter importer = new ModelImporter();

            //The Material (Color) that is applyed to the importet objects
            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Orange));
            importer.DefaultMaterial = material;

            //instanciate a new group of 3D Models
            hand = new Model3DGroup();

            #region load_files
            CargarRecursos(importer);

            //add them to the group
            AgregarRecursos();
            #endregion

            //hand is complete assign it to the global variable
            this.our_Model = hand;

            //establecer punto a la caja
            mybox.Height = 0.01;
            mybox.Width = 0.3;
            mybox.Length = 0.3;
            boxX = 13;
            boxY = 71;
            boxZ = 4d;
            m_helix_viewport.Children.Add(mybox);
            //boxcontrol.DataContext = this;

            //aplicar transformaciones al modelo 3D
            //var transforms = new Transform3DGroup();
            //// resize model 3D
            ////ScaleTransform3D myScale = new ScaleTransform3D(3,2.5,2.5);
            ////rotate whole skeleton to have it upright
            //RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90));
            //myRotateTransform.CenterX = 0;
            //myRotateTransform.CenterY = 0;
            //myRotateTransform.CenterZ = 0;
            //transforms.Children.Add(myRotateTransform);
            ////transforms.Children.Add(myScale);
            //hand.Transform = transforms;

            //set datacontext for the sliders and helper
            overall_grid.DataContext = this;

        }

        private void AgregarRecursos()
        {
            hand.Children.Add(mano_sin_dedos);
            hand.Children.Add(pulgar_proximal);
            //hand.Children.Add(pulgar_distal);
            hand.Children.Add(indice_proximal);
            hand.Children.Add(indice_distal);
            hand.Children.Add(mayor_proximal);
            hand.Children.Add(mayor_distal);
            hand.Children.Add(anular_proximal);
            hand.Children.Add(anular_distal);
            hand.Children.Add(meñique_proximal);
            hand.Children.Add(meñique_distal);
        }

        private void CargarRecursos(ModelImporter importer)
        {
            //load the files
            mano_sin_dedos = importer.Load("../../Protesis/mano_sin_dedos.3ds");
            pulgar_proximal = importer.Load("../../Protesis/pulgar_proximal.3ds");
            //pulgar_distal = importer.Load("../../Recursos/Protesis/pulgar_distal.3ds");
            indice_proximal = importer.Load("../../Protesis/indice_proximal.3ds");
            indice_distal = importer.Load("../../Protesis/indice_distal.3ds");
            mayor_proximal = importer.Load("../../Protesis/mayor_proximal.3ds");
            mayor_distal = importer.Load("../../Protesis/mayor_distal.3ds");
            anular_proximal = importer.Load("../../Protesis/anular_proximal.3ds");
            anular_distal = importer.Load("../../Protesis/anular_distal.3ds");
            meñique_proximal = importer.Load("../../Protesis/meñique_proximal.3ds");
            meñique_distal = importer.Load("../../Protesis/meñique_distal.3ds");
        }

        //moves generic proximal
        void move_proximal(int angle, string dedo, Vector3D vec, Point3D punto)
        {
            //rotate the object by "angle", the vector describes the axis
            RotateTransform3D proximal_transform = new RotateTransform3D(new AxisAngleRotation3D(vec, angle));

            //tells where the point of rotation is OK
            proximal_transform.CenterX = punto.X;
            proximal_transform.CenterY = punto.Y;
            proximal_transform.CenterZ = punto.Z;

            //apply transformation, also move the distal
            switch (dedo)
            {
                case "pulgar":
                    pulgar_proximal.Transform = proximal_transform;
                    //move_distal(pulgar_distal_angle, dedo, new Point3D(), new Vector3D());
                    break;
                case "indice":
                    indice_proximal.Transform = proximal_transform;
                    //move_distal(indice_distal_angle, dedo, new Point3D(), new Vector3D());
                    break;
                case "mayor":
                    mayor_proximal.Transform = proximal_transform;
                    //move_distal(mayor_distal_angle, dedo, new Point3D(), new Vector3D());
                    break;
                case "anular":
                    anular_proximal.Transform = proximal_transform;
                    //move_distal(anular_distal_angle, dedo, new Point3D(), new Vector3D());
                    break;
                case "meñique":
                    meñique_proximal.Transform = proximal_transform;
                    //move_distal(meñique_distal_angle, dedo, new Point3D(), new Vector3D());
                    break;
            }

        }

        //moves generic distal
        void move_distal(int angle, string dedo, Point3D punto, Vector3D vec)
        {
            //new group of transformations, the group will "add" movements
            var Group_3D = new Transform3DGroup();

            switch (dedo)
            {
                case "pulgar":
                    Group_3D.Children.Add(pulgar_proximal.Transform);
                    break;
                case "indice":
                    Group_3D.Children.Add(indice_proximal.Transform);
                    break;
                case "mayor":
                    Group_3D.Children.Add(mayor_proximal.Transform);
                    break;
                case "anular":
                    Group_3D.Children.Add(anular_proximal.Transform);
                    break;
                case "meñique":
                    Group_3D.Children.Add(meñique_proximal.Transform);
                    break;
            }

            //we need to find out where our old point is now
            Point3D origin = Group_3D.Transform(punto);

            //create new transformation
            RotateTransform3D distal_transform = new RotateTransform3D(new AxisAngleRotation3D(vec, angle));
            distal_transform.CenterX = origin.X;
            distal_transform.CenterY = origin.Y;
            distal_transform.CenterZ = origin.Z;

            //add it to the transformation group (and therefore to the femores movement
            Group_3D.Children.Add(distal_transform);

            //Apply the transform
            switch (dedo)
            {
                case "pulgar":
                    pulgar_distal.Transform = Group_3D;
                    break;
                case "indice":
                    indice_distal.Transform = Group_3D;
                    break;
                case "mayor":
                    mayor_distal.Transform = Group_3D;
                    break;
                case "anular":
                    anular_distal.Transform = Group_3D;
                    break;
                case "meñique":
                    meñique_distal.Transform = Group_3D;
                    break;
            }

        }

        private void GuardarGesto(object sender, RoutedEventArgs e)
        {
            //string[] lines = {
            //    "D1" + pulgar_proximal_angle.ToString("X2"),
            //    "D2" + indice_proximal_angle.ToString("X2"),
            //    "D3" + mayor_proximal_angle.ToString("X2"),
            //    "D4" + anular_proximal_angle.ToString("X2"),
            //    "D5" + meñique_proximal_angle.ToString("X2")
            //};

            // Configure save file dialog box
            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.FileName = "Secuencia"; //Default file name
            //dlg.DefaultExt = ".txt"; //Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; //Filter files by extension

            //// Show save file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            //if (result == true)
            //{
            //    // Save document
            //    string filename = dlg.FileName;
            //    System.IO.File.WriteAllLines(filename, lines);
            //}

            //GestoModel gesto = new GestoModel()
            //{
            //    PosPulgar = pulgar_proximal_angle,
            //    PosMeñique = meñique_proximal_angle,
            //    PosMayor = mayor_proximal_angle,
            //    Posindice = indice_proximal_angle,
            //    PosAnular = anular_proximal_angle,
            //    Descripcion = "Prueba Post",
            //    FechaCreacion = DateTime.Today,
            //    Nombre = "Pruebaa Post",
            //    UsuarioID = 1
            //};


            //GestoHandler.GuardarGesto(gesto);


        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "¿Estás seguro/a que quieres salir del programa?";
            string caption = "Mensaje";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    System.Windows.Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }

        //private async void CargarGesto(object sender, RoutedEventArgs e)
        //{
         
        //    GestoModel gesto =  await GestoHandler.ObtenerGestoPorId(2);
        //    ActualizarGesto(gesto);
        //}

        //private void ActualizarGesto(GestoModel gesto)
        //{
        //    this.pulgar_proximal_angle = gesto.PosPulgar;
        //    this.indice_proximal_angle = gesto.Posindice;
        //    this.mayor_proximal_angle = gesto.PosMayor;
        //    this.anular_proximal_angle = gesto.PosAnular;
        //    this.meñique_proximal_angle = gesto.PosMeñique;

        //}
    }
}
