using Caliburn.Micro;
using EcoHand.EventModels;
using EcoHand.Handlers;
using EcoHand.Models;
using EcohandBussinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class ListadoSecuenciasViewModel : Conductor<object>
    {

        public int Id { get; set; }

        private ILoggedInUser _user;

        private IEventAggregator _events;

        private SimpleContainer _container;
        public ListadoSecuenciasViewModel(IEventAggregator events, SimpleContainer container, ILoggedInUser user)
        {
            _events = events;
            _events.Subscribe(this);
            _container = container;
            _user = user;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            MisGestosSelected = true;

            if (Secuencias?.Count > 0)
            {
                SelectedSecuencia = Secuencias.First();
            }
        }



        public bool CanLoadEditarById
        {
            get
            {
                return SelectedSecuencia?.UsuarioId == _user.Id;
            }
        }

        public bool CanEliminarSecuencia
        {
            get
            {
                return SelectedSecuencia?.UsuarioId == _user.Id;
            }
        }


        private SecuenciaModel _selectedSecuencia;

        public SecuenciaModel SelectedSecuencia
        {
            get
            {
                return _selectedSecuencia;
            }
           set
            {
                _selectedSecuencia = value;
                NotifyOfPropertyChange(() => SelectedSecuencia);
                NotifyOfPropertyChange(() => CanEliminarSecuencia);
                NotifyOfPropertyChange(() => CanLoadEditarById);
                LoadSecuenciaDetail();
            }
        }

        private bool _misSecuenciasSelected;

        public bool MisGestosSelected
        {
            get { return _misSecuenciasSelected; }
            set
            {
                _misSecuenciasSelected = value;
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
            await CargarSecuencias();
            NotifyOfPropertyChange(() => Secuencias);
            if (Secuencias.Count > 0)
            {
                SelectedSecuencia = Secuencias.First();
            }
        }


        private BindingList<SecuenciaModel> _secuencias;

        public BindingList<SecuenciaModel> Secuencias
        {
            get { return _secuencias; }
            set
            {
                _secuencias = value;
                NotifyOfPropertyChange(() => Secuencias);

            }
        }


        private async Task CargarSecuencias()
        {
            var resp = await SecuenciaHandler.GetSecuenciasAsync();

            if (MisGestosSelected)
            {
                resp = resp.Where(x => x.UsuarioID == _user.Id).ToList();
            }

            Secuencias = new BindingList<SecuenciaModel>();

            foreach (var item in resp)
            {

                Secuencias.Add(new SecuenciaModel()
                {
                    Descripcion = item.Descripcion,
                    ID = item.ID,
                    FechaActualizacion = item.FechaModificacion,
                    FechaCreacion = item.FechaCreacion,
                    Nombre = item.Nombre,
                    XmlCode = item.CodigoEstructura,
                    UsuarioId = item.UsuarioID
                });
            }
        }

        public void LoadEditorSecuencias()
        {
            var conductor = this.Parent as ShellViewModel;
            conductor.LoadEditorSecuencias();

        }

        public void LoadEditarById(int Id)
        {

            var conductor = this.Parent as ShellViewModel;
            var evento = new EditarSecuenciaEvent();
            evento.Secuencia = SelectedSecuencia;

            conductor.LoadEditorSecuencias(evento);

        }

        public async void EliminarSecuencia()
        {
            await SecuenciaHandler.EliminarAsync(SelectedSecuencia.ID);
            this.Secuencias.Remove(SelectedSecuencia);

            NotifyOfPropertyChange(() => Secuencias);

        }

        public void LoadSecuenciaDetail()
        {
            var evento = new VerSecuenciaEvent();
            evento.Secuencia = SelectedSecuencia;
            
            ActivateItem(_container.GetInstance<SecuenciaDetailsViewModel>());
            _events.PublishOnUIThread(evento);

        }


    }
}
