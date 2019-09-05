
using System.ComponentModel.DataAnnotations.Schema;

namespace Closify.Models.Restricoes
{

    public abstract class Restricao
    {
        public int RestricaoID { get; set; }
        public string NomeRestricao { get; set; }

        public TipoRestricao TipoRestricao { get; set; }
        protected Restricao() { }

        protected Restricao(string nomeRestricao)
        {
            NomeRestricao = nomeRestricao;
        }

        protected Restricao(string nomeRestricao, TipoRestricao tipoRestricao)
        {
            NomeRestricao = nomeRestricao;
            TipoRestricao = tipoRestricao;
        }
        public abstract bool Validar(Produto pBase, Produto pParte);

        public bool EdoMesmoTipo(TipoRestricao tipo){
            return TipoRestricao == tipo;
        }
    }
}