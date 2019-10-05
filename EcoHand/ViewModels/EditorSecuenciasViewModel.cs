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
using EcohandBussinessLogic.Data;


namespace EcoHand.ViewModels
{
    public class EditorSecuenciasViewModel : Screen
    {

           

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

        

        private IEventAggregator _events;

        private ILoggedInUser _user;

        public EditorSecuenciasViewModel(IEventAggregator events, ILoggedInUser user)
        {
            _events = events;
            _events.Subscribe(this);
            _user = user;
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

        public void AgregarASecuencia()
        {
            SelectedGesto.Posicion = Secuencia.Count;
            this.Secuencia.Add(SelectedGesto);
        }

        public void EliminarDeSecuencia()
        {
            this.Secuencia.RemoveAt(Secuencia.Count - 1);
            NotifyOfPropertyChange(() => Secuencia);
        }

        public async void GuardarSecuencia()
        {

            Secuencia s = new Secuencia();
            foreach (var item in Secuencia)
            {
                var type = item.GetType();
                if (type.Name.StartsWith("Gesto"))
                {
                    //es un gesto
                    var i = item as GestoModel;
                    var gesto = await GestoHandler.ObtenerGestoPorId(i.Id);



                    //esto esta horrible
                    s.Secuencias.Add(new SecuenciaItem
                    {
                        Posicion = item.Posicion,
                        CodigoHexa = gesto.Hexa,
                        Tipo = 1
                    });
                }
                else
                {
                    //esto deberia ser un evento
                    s.Secuencias.Add(new SecuenciaItem
                    {
                        Posicion = item.Posicion,
                        CodigoHexa = "verComoFormarEsto",
                        Tipo = 0
                    });
                }
            }
            await SecuenciaHandler.Crear(s);
        }



    }
}
