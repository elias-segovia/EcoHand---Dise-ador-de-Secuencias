using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcohandBussinessLogic.Data;
using APIController;

namespace EcohandBussinessLogic.Handlers
{
    public  class SecuenciaHandler
    {

        private static void CrearSecuencia()
        {

        
        }

        public static void Editar() { }

        public static void Eliminar() { }

        public static void Crear() {

            CrearSecuencia();

            CrearSecuenciaArduino();
        }

        private static  void CrearSecuenciaArduino() { }

        public static async Task Crear(Data.Secuencia s)
        {
            APIController.Model.Secuencia secu = new APIController.Model.Secuencia();
            foreach(SecuenciaItem i in s.Secuencias)
            {
                secu.CodigoEjecutable += i.CodigoHexa;
            }
            secu.Descripcion = "Esta es una descri de prueba";
            secu.FechaCreacion = DateTime.Now;
            secu.FechaModificacion = DateTime.Now;
            secu.Nombre = "Secu Prueba";

            await SecuenciaController.PostAsync(secu);
            

        }
    }
}
