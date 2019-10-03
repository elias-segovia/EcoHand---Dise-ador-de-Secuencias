﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHand.Models
{
    public class GestoModel : ISecuenciable
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        
        public int SecuenciaID { get; set; }
        public int Posicion { get; set; }

    }
}
