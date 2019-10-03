using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoHand.EventModels;
using System.ComponentModel;
using EcoHand.Models;
using EcoHand.Handlers;
using EcohandBussinessLogic.Handlers;


namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen
    {

        private IEventAggregator _events;

        public EditorSecuenciasViewModel(IEventAggregator events)
        {
            _events = events;
            Secuencia = new BindingList<ISecuenciable>();
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await CargarListaDeGestosAsync();

        }

        private async Task CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindingList<GestoModel>();
            foreach (var item in resp)
            {
                Gestos.Add(new GestoModel()
                {
                    Id = item.ID,
                    Nombre = item.Nombre
                });
            }
        }

        //deberia ser de gestos y eventos
        private BindingList<ISecuenciable> _secuencia;

        public BindingList<ISecuenciable> Secuencia
        {
            get { return _secuencia; }
            set
            {
                _secuencia = value;
                NotifyOfPropertyChange(() => Secuencia);

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

            }
        }


        public void AgregarASecuencia()
        {
            this.Secuencia.Add(SelectedGesto);
        }

        public void EliminarDeSecuencia()
        {
            this.Secuencia.RemoveAt(Secuencia.Count - 1);
            NotifyOfPropertyChange(() => Secuencia);
        }

        public void GuardarSecuencia()
        {
            SecuenciaHandler.Crear();
        }



    }
}
