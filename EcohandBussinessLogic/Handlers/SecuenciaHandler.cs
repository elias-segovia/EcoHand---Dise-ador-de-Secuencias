using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APIController;

namespace EcohandBussinessLogic.Handlers
{
    public  class SecuenciaHandler
    {

        
        public static void Editar() { }

        public static void Eliminar(int id) { }
        
        

        public static async Task Crear(Secuencia s)
        {

            await SecuenciaController.PostAsync(s);

        }

        public static List<Secuencia> GetSecuencias()
        {
            return new List<Secuencia>();
        }
    }
}
