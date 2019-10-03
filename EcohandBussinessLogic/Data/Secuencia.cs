using APIController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcohandBussinessLogic.Data
{
    public class Secuencia
    {
        //solo para mapear y probar
        public List<SecuenciaItem> Secuencias { get; set; }
        public Secuencia()
        {
            Secuencias = new List<SecuenciaItem>();
        }

        
    }

    public class SecuenciaItem
    {
        public int Posicion { get; set; }

        //evento o gesto
        public int Tipo { get; set; }

        public string CodigoHexa { get; set; }
    }
}
