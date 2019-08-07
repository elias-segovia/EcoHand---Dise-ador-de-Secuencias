using System;
using System.Collections.Generic;
using System.Text;

namespace APIController.Model
{
    public class GestoModel
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int PosPulgar { get; set; }

        public int Posindice { get; set; }

        public int PosMayor { get; set; }

        public int PosAnular { get; set; }

        public int PosMeñique { get; set; }

        public int UsuarioID { get; set; }
    }
}
