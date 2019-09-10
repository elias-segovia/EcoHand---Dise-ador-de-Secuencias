using APIController;
using APIController.Model.DTO_IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcohandBussinessLogic.Handlers
{
    public class UsuarioHandler
    {
        public static async Task<bool> Ingresar(DTO_In_Usuario usuario)
        {

            return await UsuarioController.PostAsync(usuario);

        }

        public static async Task<bool> Registrar(DTO_In_Usuario usuario)
        {

            return await UsuarioController.RegitroAsync(usuario);

        }

    }
}
