using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public class SecuenciaModel
    {
        public int ID { get; set; }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string ArduinoCode { get; set; }

        public string XmlCode { get; set; }



    }
}
