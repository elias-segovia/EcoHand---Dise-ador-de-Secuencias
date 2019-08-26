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
    public class ShellViewModel : Conductor<object>
    {



        public ShellViewModel()
        {
            LoadMain();
        }
        public void LoadMain()
        {
            LoadListaGesto();
        }

        public BindableCollection<GestoModel> Gestos { get; set; }
        private async Task<BindableCollection<GestoModel>> CargarListaDeGestosAsync()
        {
            var resp = await GestoHandler.ObtenerListaDeGestosAsync();
            Gestos = new BindableCollection<GestoModel>();
            foreach (var item in resp)
            {
                Gestos.Add(new GestoModel() { Id = item.ID, Nombre = item.Nombre });
            }

            return Gestos;
        }
        public async void LoadListaGesto()
        {
            
            try
            {
                var resp = await CargarListaDeGestosAsync();
                ActivateItem(new ListadoGestosViewModel(resp));
            }
            catch (Exception e)
            {
                ActivateItem(new ListadoGestosViewModel());
                //mensaje no se pudo cargar los gestos
            }
          

        }
    }
}
