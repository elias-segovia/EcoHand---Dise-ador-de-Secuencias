using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIController.Model
{
    public class Secuencia
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string CodigoEjecutable { get; set; }

        public string CodigoEstructura { get; set; }

        public int UsuarioID { get; set; }
    }
}
