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
    public class ListadoGestosViewModel : Conductor<object> , IHandle<EditarGestoEvent>
    {

        public int Id { get; set; }

        private ILoggedInUser _user;

        private IEventAggregator _events;

        private SimpleContainer _container;
        public ListadoGestosViewModel(IEventAggregator events , SimpleContainer container)
        {
            _events = events;
            _events.Subscribe(this);
            _container = container;
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
            evente.Gesto = await GestoHandler.ObtenerGestoPorId(SelectedGesto.Id);
            conductor.LoadEditor(evente);
           
            //_events.PublishOnUIThread(evente);
           
            //conductor.LoadEditor();
 
           

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

        public void Handle(EditarGestoEvent message)
        {
            
        }
    }
}
