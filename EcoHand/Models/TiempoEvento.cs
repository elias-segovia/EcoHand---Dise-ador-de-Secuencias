using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("TiempoEvento")]
    public class TiempoEvento : EventoModel
    {
        public override EventoModel Clone()
        {
            return new TiempoEvento()
            {
                Descripcion = this.Descripcion,
                Nombre = this.Nombre,
                Posicion = this.Posicion,
                Tipo = TipoEvento.Tiempo,
                ValorEntrada = ValorEntrada
            };
        }

        protected override string ObtenerCodigo()
        {
            return "TT";
        }

        protected override string ObtenerValorEnHexa()
        {
            return Int64.Parse((Math.Round(ValorEntrada * 25.5)).ToString()).ToString("X2");
        }
    }
}
