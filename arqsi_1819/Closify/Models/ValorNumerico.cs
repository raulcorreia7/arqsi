
using System;

namespace Closify.Models
{

    public class ValorNumerico
    {

        public int ValorNumericoID { get; set; }

        public double Valor { get; set; }

        public ValorNumerico(double valor)
        {
            Valor = valor;
        }

        public ValorNumerico() { }

        public override bool Equals(object obj)
        {
            var item = obj as ValorNumerico;
            if (item == null) return false;
            double epsilon = 0.001;
            return Math.Abs(Valor - item.Valor) < epsilon;
        }
    }
}