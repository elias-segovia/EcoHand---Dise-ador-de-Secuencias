using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIController.Responses
{
    public class GetSecuenciaPorNombreResponse
    {

        public bool Exitoso { get; set; }

        public Secuencia Secuencia { get; set; }

    }
}
