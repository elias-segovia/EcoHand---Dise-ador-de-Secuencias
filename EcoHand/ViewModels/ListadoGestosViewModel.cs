using Caliburn.Micro;
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
        public ListadoGestosViewModel()
        {

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await CargarListaDeGestosAsync();
            if(Gestos.Count > 0)
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


        //public BindableCollection<GestoModel> Gestos { get; set; }
        private async Task CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindingList<GestoModel>();
            foreach (var item in resp)
            {
                Gestos.Add(new GestoModel() { Id = item.ID, Nombre = item.Nombre });
            }
        }

        public void LoadEditorDeGestos()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new EditorDeGestosViewModel());

        }

        public async void LoadEditarById(int Id)
        {

            var conductor = this.Parent as IConductor;
            var resp = await GestoHandler.ObtenerGestoPorId(SelectedGesto.Id);
            conductor.ActivateItem(new EditorDeGestosViewModel(resp));

        }

        public async void EliminarGesto()
        {
            await GestoHandler.EliminarGesto(SelectedGesto.Id);
            this.Gestos.Remove(SelectedGesto);

            NotifyOfPropertyChange(() => Gestos);

        }

        public async Task LoadHandAsync()
        {
            var resp = await GestoHandler.ObtenerGestoPorId(SelectedGesto.Id);
            ActivateItem(new HandDetailsViewModel(resp));
        }



    }
}
