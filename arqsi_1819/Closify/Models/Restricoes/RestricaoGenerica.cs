
using Closify.Models;

namespace Closify.Models.Restricoes
{

    public class RestricaoGenerica : Restricao
    {
        private static readonly string NOME_R = "Restricao Generica";
        public RestricaoGenerica() { }
        public RestricaoGenerica(string nome) : base(NOME_R,TipoRestricao.Generica) { }

        public override bool Validar(Produto pBase, Produto pParte)
        {
            return true;
        }
    }
}