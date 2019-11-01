using Caliburn.Micro;
using EcoHand.EventModels;
using EcoHand.Handlers;
using EcoHand.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoHand.ViewModels
{
    public class ListadoGestosViewModel : Conductor<object>
    {

        public int Id { get; set; }

        private ILoggedInUser _user;

                
        public ListadoGestosViewModel(ILoggedInUser user)
        {
            _user = user;

        }

        public bool CanLoadEditarById
        {
            get { return _user.Id == SelectedGesto?.UsuarioID; }
        }

        public bool CanEliminarGesto
        {
            get { return _user.Id == SelectedGesto?.UsuarioID; }
        }

        protected override  void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            MisGestosSelected = true;

            if (Gestos?.Count > 0)
            {
                SelectedGesto = Gestos.First();
            }
        }

        private GestoModel _selectedGesto;

        public GestoModel SelectedGesto
        {
            get
            {
                return _selectedGesto;
            }
            set
            {
                _selectedGesto = value;
                NotifyOfPropertyChange(() => SelectedGesto);
                NotifyOfPropertyChange(() => CanLoadEditarById);
                NotifyOfPropertyChange(() => CanEliminarGesto);
                LoadHandAsync();

            }
        }

        private BindingList<GestoModel> _gestos;

        public BindingList<GestoModel> Gestos
        {
            get { return _gestos; }
            set
            {
                _gestos = value;
                NotifyOfPropertyChange(() => Gestos);

            }
        }

        private bool _misGestosSelected;

        public bool MisGestosSelected
        {
            get { return _misGestosSelected; }
            set
            {
                _misGestosSelected = value;
                NotifyOfPropertyChange(() => MisGestosSelected);
                RefrescarGestos();
            }
        }

        private bool _todosSelected;

        public bool TodosSelected
        {
            get { return _todosSelected; }
            set
            {
                _todosSelected = value;
                NotifyOfPropertyChange(() => TodosSelected);

            }
        }

        private async void RefrescarGestos()
        {
            await CargarListaDeGestosAsync();
            NotifyOfPropertyChange(() => Gestos);
            if (Gestos?.Count > 0)
            {
                SelectedGesto = Gestos.First();
            }
        }


        //public BindableCollection<GestoModel> Gestos { get; set; }
        private async Task CargarListaDeGestosAsync()
        {


            try
            {
                var resp = await GestoHandler.ObtenerListaDeGestosAsync();

                if (MisGestosSelected == true)
                {
                    resp = resp.Where(x => x.UsuarioID == _user.Id).ToList();
                }

                Gestos = new BindingList<GestoModel>();
                foreach (var item in resp)
                {
                    Gestos.Add(new GestoModel()
                    {
                        Descripcion = item.Descripcion,
                        FechaCreacion = item.FechaCreacion,
                        FechaModificacion = item.FechaModificacion,
                        ID = item.ID,
                        Nombre = item.Nombre,
                        PosAnular = item.PosAnular,
                        Posindice = item.Posindice,
                        PosMayor = item.PosMayor,
                        PosMeñique = item.PosMeñique,
                        PosPulgar = item.PosPulgar,
                        UsuarioID = item.UsuarioID
                    });
                }

            }
            catch (Exception)
            {

               MessageBox.Show("Error al cargar los gestos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void LoadEditorDeGestos()
        {
            var conductor = this.Parent as ShellViewModel;
            conductor.LoadEditor();
       
        }

        public  void LoadEditarById(int Id)
        {

           
            var conductor = this.Parent as ShellViewModel;
            var evente = new EditarGestoEvent();
            evente.Gesto = SelectedGesto;
           
            conductor.LoadEditor(evente);

       



        }

        public async void EliminarGesto()
        {
            try
            {
                await GestoHandler.EliminarGesto(SelectedGesto.ID);
                this.Gestos.Remove(SelectedGesto);

                NotifyOfPropertyChange(() => Gestos);
            }
            catch
            {
                MessageBox.Show("Error al Eliminar el gesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public async Task LoadHandAsync()
        {

            ActivateItem(new HandDetailsViewModel(SelectedGesto));
        }


    }
}
