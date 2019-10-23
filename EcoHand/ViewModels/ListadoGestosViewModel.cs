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

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            
            await CargarListaDeGestosAsync();
            
            if (Gestos.Count > 0)
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
            if (Gestos.Count > 0)
            {
                SelectedGesto = Gestos.First();
            }
        }


        //public BindableCollection<GestoModel> Gestos { get; set; }
        private async Task CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();

            if(MisGestosSelected == true)
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

        public void LoadEditorDeGestos()
        {
            var conductor = this.Parent as ShellViewModel;
            conductor.LoadEditor();
            //conductor.ActivateItem(new EditorDeGestosViewModel());
            //ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());

        }

        public async void LoadEditarById(int Id)
        {

            //ActivateItem(_container.GetInstance<EditorDeGestosViewModel>());
            var conductor = this.Parent as ShellViewModel;
            var evente = new EditarGestoEvent();
            evente.Gesto = SelectedGesto;
            //evente.Gesto = await GestoHandler.ObtenerGestoPorId(SelectedGesto.ID);
            conductor.LoadEditor(evente);

            //_events.PublishOnUIThread(evente);

            //conductor.LoadEditor();



        }

        public async void EliminarGesto()
        {
            await GestoHandler.EliminarGesto(SelectedGesto.ID);
            this.Gestos.Remove(SelectedGesto);

            NotifyOfPropertyChange(() => Gestos);

        }

        public async Task LoadHandAsync()
        {

            ActivateItem(new HandDetailsViewModel(SelectedGesto));
        }


    }
}
