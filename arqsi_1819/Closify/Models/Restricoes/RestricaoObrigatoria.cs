using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Models.Restricoes
{
    public class RestricaoObrigatoria : Restricao
    {
        private static readonly string NOME_R = "Restricao Obrigatoria";
        public RestricaoObrigatoria() : base(NOME_R,TipoRestricao.Obrigatoria) { }

        public override bool Validar(Produto pBase, Produto pParte)
        {
            return true;
        }
    }
}
