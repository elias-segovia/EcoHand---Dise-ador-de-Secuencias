using APIController;
using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Handlers
{
    public class GestoHandler
    {
        public static async Task<List<GestoModel>> ObtenerListaDeGestosAsync()
        {
            var gestos = await GestosController.GetGestos();

            return gestos.Result;
        }

        public static async Task<GestoModel> ObtenerGestoPorId(int id)
        {

            var gesto = await GestosController.GetGestosById(id);

            return gesto.Result;

        }



        public static async void GuardarGesto(GestoModel gesto)
        {

            await GestosController.PostAsync(gesto);

        }

        public static async Task EliminarGesto(int id)
        {
            await GestosController.Delete(id);
        }

        public static  async 
        Task
EditarGestoAsync(GestoModel gesto)
        {
            try
            {
                await GestosController.EditarAsync(gesto);
            }
            catch
            {

            }

        }
    }
}
