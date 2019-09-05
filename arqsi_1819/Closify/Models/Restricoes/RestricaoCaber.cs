using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Models.Restricoes
{
    public class RestricaoCaber : Restricao
    {
        private static readonly string NOME_R = "Restricao Caber";
        public RestricaoCaber() : base(NOME_R, TipoRestricao.Caber) { }

        public override bool Validar(Produto pBase, Produto pParte)
        {
            return ValidarDimensoes(pBase.Dimensao.Altura, pParte.Dimensao.Altura)
                && ValidarDimensoes(pBase.Dimensao.Comprimento, pParte.Dimensao.Comprimento)
                && ValidarDimensoes(pBase.Dimensao.Largura, pParte.Dimensao.Largura);
        }

        private bool ValidarDimensoes(List<ValorNumerico> dimPBase, List<ValorNumerico> dimPParte)
        {
            List<Double> Base = new List<Double>();
            List<Double> Parte = new List<Double>();

            foreach (ValorNumerico v in dimPBase.ToArray())
                Base.Add(v.Valor);
            foreach (ValorNumerico v in dimPParte.ToArray())
                Parte.Add(v.Valor);
            Base.Sort();
            Parte.Sort();
            foreach (Double d in Parte)
            {
                if (Base.First() >= d) return true;
                else
                {
                    if (Base.Last() >= d) return true;
                }
            }
            return false;
        }
    }
}
