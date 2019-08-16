using Caliburn.Micro;
using EcoHand.Handlers;
using EcoHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.ViewModels
{
    public class ListadoGestosViewModel : Screen
    {

        public int Id { get; set; }
        public ListadoGestosViewModel()
        {
            CargarListaDeGestosAsync();
        }

        public ListadoGestosViewModel(BindableCollection<GestoModel> gestos)
        {
            Gestos = gestos;
        }

        private GestoModel _selectedItem;

        public GestoModel SelectedItem { get { return _selectedItem; } set { _selectedItem = value; NotifyOfPropertyChange(); } }

        public BindableCollection<GestoModel> Gestos { get; set; }
        private async void CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindableCollection<GestoModel>();
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

        public async void LoadEditarById()
        {

            var conductor = this.Parent as IConductor;
            var resp = await GestoHandler.ObtenerGestoPorId(SelectedItem.Id);
            conductor.ActivateItem(new EditorDeGestosViewModel(resp));

        }



    }
}
