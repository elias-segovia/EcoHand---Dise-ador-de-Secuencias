using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIController.Requests
{
    public class EditarGestoRequest
    {
        public string Name { get; set; }

        public string Descripcion { get; set; }

        public int PosPulgar { get; set; }

        public int PosIndice { get; set; }

        public int PosMayor { get; set; }

        public int PosAnular { get; set; }

        public int PosMeñique { get; set; }

    }
}
