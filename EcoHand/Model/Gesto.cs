using System;
using System.Collections.Generic;
using System.Text;

namespace EcoHandBL.Model
{
    public class Gesto
    {

        private int pulgarProxi;

        private int pulgarDistal;

        private int indiceProxi;

        private int indiceDistal;

        private int anularProxi;

        private int anularDistal;

        private int meñiqueProxi;

        private int meñiqueDistal;

        private int medioProxi;

        private int medioDistal;

        public Gesto()
        {

        }
        public Gesto(int meñique, int anular, int medio, int indice, int pulgar)
        {
            this.PulgarProxi = pulgar;
            this.IndiceProxi = indice;
            this.MedioProxi = medio;
            this.AnularProxi = anular;
            this.MeñiqueProxi = meñique;
        }

        public int PulgarProxi { get => pulgarProxi; set => pulgarProxi = value; }
        public int PulgarDistal { get => pulgarDistal; set => pulgarDistal = value; }
        public int IndiceProxi { get => indiceProxi; set => indiceProxi = value; }
        public int IndiceDistal { get => indiceDistal; set => indiceDistal = value; }
        public int AnularProxi { get => anularProxi; set => anularProxi = value; }
        public int AnularDistal { get => anularDistal; set => anularDistal = value; }
        public int MeñiqueProxi { get => meñiqueProxi; set => meñiqueProxi = value; }
        public int MeñiqueDistal { get => meñiqueDistal; set => meñiqueDistal = value; }
        public int MedioProxi { get => medioProxi; set => medioProxi = value; }
        public int MedioDistal { get => medioDistal; set => medioDistal = value; }
    }
}
