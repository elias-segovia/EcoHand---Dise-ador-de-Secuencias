using Caliburn.Micro;
using EcoHand.EventModels;
using EcoHand.Handlers;
using EcoHand.Models;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EcoHand.ViewModels
{
    public class EditorDeGestosViewModel : Screen, IHandle<EditarGestoEvent>
    {



        #region Modelo3D Variables
        //modelo de la mano agrupando todas las partes
        Model3DGroup hand;

        //caja 3d para tener un punto de referencia
        BoxVisual3D mybox = new BoxVisual3D();

        //partes de la mano
        Model3D mano_sin_dedos;
        Model3D pulgar_proximal;
        //Model3D pulgar_distal;
        Model3D indice_proximal;
        Model3D indice_distal;
        Model3D mayor_proximal;
        Model3D mayor_distal;
        Model3D anular_proximal;
        Model3D anular_distal;
        Model3D meñique_proximal;
        Model3D meñique_distal;
        #endregion

        #region Modelo Mano Variables
        int m_pulgar_proximal_angle;


        public int Pulgar_proximal_angle
        {
            get { return m_pulgar_proximal_angle; }
            set
            {
                //Move_proximal(value, "pulgar", new Vector3D(1, -0.4, 0), new Point3D(-0.2, 0, 1.5));
                //Move_distal(value, "pulgar", new Point3D(0.7, 0.5, 1.9), new Vector3D(1, 0.4, 0));
                Move_proximal(value, "pulgar", new Vector3D(-1, 0, 0), new Point3D(13, 71, 4));
                m_pulgar_proximal_angle = value;



            }
        }


        string nombre;

        string descripcion;

        int m_indice_proximal_angle;
        public int Indice_proximal_angle
        {
            get { return m_indice_proximal_angle; }
            set
            {
                //Move_proximal(value, "indice", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //Move_distal(value, "indice", new Point3D(0.5, 0, 2.5), new Vector3D(0, 1, 0));
                Move_proximal(value, "indice", new Vector3D(0, 1, 0), new Point3D(84, 74, 9));
                Move_distal(value, "indice", new Point3D(123.5, 74, 8.5), new Vector3D(0, 1, 0));
                m_indice_proximal_angle = value;
            }
        }

        int m_mayor_proximal_angle;
        public int Mayor_proximal_angle
        {
            get { return m_mayor_proximal_angle; }
            set
            {
                //Move_proximal(value, "mayor", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //Move_distal(value, "mayor", new Point3D(0.6, -0.13, 2.545), new Vector3D(0, 1, 0));
                Move_proximal(value, "mayor", new Vector3D(0, 1, 0), new Point3D(84, 54, 9));
                Move_distal(value, "mayor", new Point3D(122.5, 54, 8), new Vector3D(0, 1, 0));
                m_mayor_proximal_angle = value;
                NotifyOfPropertyChange();
            }
        }


        int m_anular_proximal_angle;
        public int Anular_proximal_angle
        {
            get { return m_anular_proximal_angle; }
            set
            {
                //Move_proximal(value, "anular", new Vector3D(0, 1, 0), new Point3D(0.5, 0, 2.2));
                //Move_distal(value, "anular", new Point3D(0.66, -0.388, 2.47), new Vector3D(0, 1, 0));
                Move_proximal(value, "anular", new Vector3D(0, 1, 0), new Point3D(84, 34, 9));
                Move_distal(value, "anular", new Point3D(123.5, 34, 8), new Vector3D(0, 1, 0));
                m_anular_proximal_angle = value;
            }
        }


        int m_meñique_proximal_angle;

        public int Meñique_proximal_angle
        {
            get { return m_meñique_proximal_angle; }
            set
            {
                //Move_proximal(value, "meñique", new Vector3D(0, 1, 0), new Point3D(0.66, -0.54, 2.08));
                //Move_distal(value, "meñique", new Point3D(0.674, -0.61, 2.27), new Vector3D(0, 1, 0));
                Move_proximal(value, "meñique", new Vector3D(0, 1, 0), new Point3D(84, 14, 9));
                Move_distal(value, "meñique", new Point3D(113.5, 14, 9), new Vector3D(0, 1, 0));
                m_meñique_proximal_angle = value;
            }
        }



        public string NombreGesto { get => nombre; set { nombre = value; NotifyOfPropertyChange(() => NombreGesto); NotifyOfPropertyChange(() => CanGuardarGesto); } }
        public string Descripcion { get => descripcion; set { descripcion = value; NotifyOfPropertyChange(); } }

        public bool Editando = false;

        public GestoModel MiGesto;
        #endregion



        //Property for the binding with the hand
        public Model3D Our_Model { get; set; }

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
            mano_sin_dedos = importer.Load("../../Recursos/Protesis/mano_sin_dedos.3ds");
            pulgar_proximal = importer.Load("../../Recursos/Protesis/pulgar_proximal.3ds");
            //pulgar_distal = importer.Load("../../Recursos/Protesis/pulgar_distal.3ds");
            indice_proximal = importer.Load("../../Recursos/Protesis/indice_proximal.3ds");
            indice_distal = importer.Load("../../Recursos/Protesis/indice_distal.3ds");
            mayor_proximal = importer.Load("../../Recursos/Protesis/mayor_proximal.3ds");
            mayor_distal = importer.Load("../../Recursos/Protesis/mayor_distal.3ds");
            anular_proximal = importer.Load("../../Recursos/Protesis/anular_proximal.3ds");
            anular_distal = importer.Load("../../Recursos/Protesis/anular_distal.3ds");
            meñique_proximal = importer.Load("../../Recursos/Protesis/meñique_proximal.3ds");
            meñique_distal = importer.Load("../../Recursos/Protesis/meñique_distal.3ds");
        }

        private IEventAggregator _events;

        EcoHand.Models.ILoggedInUser _user;
        public EditorDeGestosViewModel(IEventAggregator events, EcoHand.Models.ILoggedInUser user)
        {
            //The Importer to load .obj files
            _events = events;
            _events.Subscribe(this);
            _user = user;
            CargarMano();

        }

        private void CargarMano()
        {
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
            this.Our_Model = hand;
            this.
            //establecer punto a la caja
            mybox.Height = 0.01;
            mybox.Width = 0.3;
            mybox.Length = 0.3;

        }


        void Move_proximal(int angle, string dedo, Vector3D vec, Point3D punto)
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
        void Move_distal(int angle, string dedo, Point3D punto, Vector3D vec)
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
                    //pulgar_distal.Transform = Group_3D;
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

        public void LoadListaDeGestos()
        {
            var shell = this.Parent as ShellViewModel;
            shell.LoadListaGesto();

        }


        public async void GuardarGesto()
        {
            if (Editando)
            {
                await EditarGestoAsync();
            }
            else
            {
                await CrearGesto();
            }


        }

        public bool CanGuardarGesto
        {
            get
            {
               return NombreGesto?.Length > 0 && NombreGesto.Trim().Length > 0;
            }
        }

        public void Cancelar()
        {
            var shell = this.Parent as ShellViewModel;

            shell.LoadListaGesto();
        }

        private async Task EditarGestoAsync()
        {
            if (await GestoHandler.EsNombreRepetido(NombreGesto, MiGesto.ID))
            {
                MessageBox.Show("El nombre ingresado ya se encuentra registrado, por favor ingreso otro", "Nombre Repetido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            APIController.Model.GestoModel gesto = new APIController.Model.GestoModel()
            {
                UsuarioID = _user.Id,
                Descripcion = this.Descripcion,
                FechaCreacion = MiGesto.FechaCreacion,
                FechaModificacion = DateTime.Now,
                Nombre = NombreGesto,
                ID = MiGesto.ID,
                PosAnular = Anular_proximal_angle,
                Posindice = Indice_proximal_angle,
                PosMayor = Mayor_proximal_angle,
                PosMeñique = Meñique_proximal_angle,
                PosPulgar = Pulgar_proximal_angle

            };



            await GestoHandler.EditarGestoAsync(gesto);


            LoadListaDeGestos();


        }

        private async Task CrearGesto()
        {

            if (await GestoHandler.EsNombreRepetido(NombreGesto))
            {
                MessageBox.Show("El nombre ingresado ya se encuentra registrado, por favor ingreso otro", "Nombre Repetido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            APIController.Model.GestoModel gesto = new APIController.Model.GestoModel()
            {
                PosPulgar = Pulgar_proximal_angle,
                PosMeñique = Meñique_proximal_angle,
                PosMayor = Mayor_proximal_angle,
                Posindice = Indice_proximal_angle,
                PosAnular = Anular_proximal_angle,
                Descripcion = this.Descripcion,
                FechaCreacion = DateTime.Today,
                Nombre = this.NombreGesto,
                UsuarioID = _user.Id
            };
            await GestoHandler.GuardarGesto(gesto);
            LoadListaDeGestos();
        }




        private void ActualizarGesto(GestoModel gesto)
        {
            //Actualizo los valores de la mano.
            this.Pulgar_proximal_angle = gesto.PosPulgar;
            this.Indice_proximal_angle = gesto.Posindice;
            this.Mayor_proximal_angle = gesto.PosMayor;
            this.Anular_proximal_angle = gesto.PosAnular;
            this.Meñique_proximal_angle = gesto.PosMeñique;



        }

        public void Handle(EditarGestoEvent message)
        {
            MiGesto = message.Gesto;
            Editando = true;
            //lo ideal seria bindear por gesto pero eso lo dejo para otro refinamiento
            this.Anular_proximal_angle = MiGesto.PosAnular;
            this.Mayor_proximal_angle = MiGesto.PosMayor;
            this.Meñique_proximal_angle = MiGesto.PosMeñique;
            this.Pulgar_proximal_angle = MiGesto.PosPulgar;
            this.Indice_proximal_angle = MiGesto.Posindice;

            this.NombreGesto = MiGesto.Nombre;
            this.Descripcion = MiGesto.Descripcion;
        }
    }
}