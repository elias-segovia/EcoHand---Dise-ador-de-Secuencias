using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("MoverDedoEvento")]
    public class MoverDedoEvento : EventoModel
    {
        public override EventoModel Clone()
        {
            return new MoverDedoEvento()
            {
                Descripcion = this.Descripcion,
                Nombre = this.Nombre,
                Posicion = this.Posicion,
                Tipo = this.Tipo,
                ValorEntrada = ValorEntrada
            };
        }

        protected override string ObtenerCodigo()
        {
            switch (Tipo)
            {
                case TipoEvento.MoverPulgar:
                    return "D1";
                case TipoEvento.MoverIndice:
                    return "D2";
                case TipoEvento.MoverMayor:
                    return "D3";
                case TipoEvento.MoverAnular:
                    return "D4";
                case TipoEvento.MoverMeñique:
                    return "D5";
                default: return "";

            }
        }

        protected override string ObtenerValorEnHexa()
        {
            return (ValorEntrada).ToString("X2");
        }
    }
}
