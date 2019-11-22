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

            return gestos.Result.OrderBy(x => x.Nombre).ToList();
        }

        public static async Task<GestoModel> ObtenerGestoPorId(int id)
        {

            var gesto = await GestosController.GetGestosById(id);

            return gesto.Result;
            

        }

        public static async Task<bool>  EsNombreRepetido(string nombre)
        {

            var gesto = await GestosController.GetGestoPorNombre(nombre);

            return gesto.Exitoso;
        }




        public static async Task GuardarGesto(GestoModel gesto)
        {

            await GestosController.PostAsync(gesto);

        }

        public static async Task EliminarGesto(int id)
        {
            await GestosController.Delete(id);
        }

        public static async Task EditarGestoAsync(GestoModel gesto)
        {
            await GestosController.EditarAsync(gesto);
        }

        public static async Task<bool> EsNombreRepetido(string nombreGesto, int iD)
        {
            bool resp = false;

            var gesto = await GestosController.GetGestoPorNombre(nombreGesto);

            if(gesto.Exitoso)
            {
                resp = gesto.Gesto.ID != iD;
            }


            return resp;
            
        }
    }
}
