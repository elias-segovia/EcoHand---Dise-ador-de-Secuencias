using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APIController;

namespace EcohandBussinessLogic.Handlers
{
    public class SecuenciaHandler
    {


        public static async Task Editar(Secuencia s) {

           await SecuenciaController.Put(s);
            
        }

        public static async Task EliminarAsync(int id)
        {
            await SecuenciaController.Delete(id);

        }



        public static async Task Crear(Secuencia s)
        {

            await SecuenciaController.PostAsync(s);

        }

        public static async Task<List<Secuencia>> GetSecuenciasAsync()
        {
            var resp = new List<Secuencia>();

            resp = await SecuenciaController.Get();

            return resp.Where(x => !String.IsNullOrEmpty(x.CodigoEstructura)).ToList();
            

        }

        public static async Task<bool> EsNombreRepetidoAsync(string nombre, int iD)
        {
            bool resp = false;

            var sec = await SecuenciaController.GetSecuenciaPorNombreAsync(nombre);

            if (sec.Exitoso)
            {
                resp = sec.Secuencia.ID != iD;
            }


            return resp;

        }
    }
}
