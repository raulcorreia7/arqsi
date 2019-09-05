using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Models.Restricoes
{
    public class RestricaoOpcional : Restricao
    {
        private static readonly string NOME_R = "Restricao Opcional";
        public RestricaoOpcional() : base(NOME_R,TipoRestricao.Opcional) { }

        public override bool Validar(Produto pBase, Produto pParte)
        {
            return true;
        }
    }
}
