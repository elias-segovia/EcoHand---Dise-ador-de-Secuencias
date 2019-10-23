using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoHand.Models
{
    [XmlType("SaltoEvento")]
    public class SaltoEvento : EventoModel
    {
        public override EventoModel Clone()
        {
            return new SaltoEvento()
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
                case TipoEvento.SaltoFSR:
                    return "I1";
                case TipoEvento.SaltoFSRNegativo:
                    return "I0";
                case TipoEvento.SaltoIncondicional:
                    return "XX";
                case TipoEvento.SaltoMusculo:
                    return "S1";
                case TipoEvento.SaltoMusculoRelajado:
                    return "S0";
                default: return "";

            }
        }

        protected override string ObtenerValorEnHexa()
        {
            return ValorEntrada.ToString("X2");
        }
    }
}
