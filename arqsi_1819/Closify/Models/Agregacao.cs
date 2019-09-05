
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Closify.Models;
using Closify.Models.DTO;
using Closify.Models.Restricoes;

namespace Closify.Models
{
    public class Agregacao
    {
        public int AgregacaoID { get; set; }

        public int BaseID { get; set; }

        public int ParteID { get; set; }

        [ForeignKey("BaseID")]
        public virtual Produto Base { get; set; }
        [ForeignKey("ParteID")]
        public virtual Produto Parte { get; set; }
        public virtual List<Restricao> Restricoes { get; set; } = new List<Restricao>();
        public Agregacao() { }

        public Agregacao(Produto pBase, Produto pParte)
        {
            Base = pBase;
            Parte = pParte;
            BaseID = pBase.ProdutoID;
            ParteID = pParte.ProdutoID;

            Restricoes.Add(RestricaoFactory.RestricaoCaber());
        }

        public bool adicionarRestricao(NovaRestricaoDTO novaRestricaoDTO)
        {
            TipoRestricao tipo = novaRestricaoDTO.Restricao;
            Restricao rest = null;
            foreach (Restricao r in Restricoes)
            {
                if (r.EdoMesmoTipo(tipo))
                {
                    rest = r;
                    break;
                }
                if ((tipo == TipoRestricao.Opcional && r.TipoRestricao == TipoRestricao.Obrigatoria) || (tipo == TipoRestricao.Obrigatoria && r.TipoRestricao == TipoRestricao.Opcional))
                {
                    rest = r;
                    break;
                }
            }
            if (rest != null)
            {
                Restricoes.Remove(rest);
            }
            Restricao nova = RestricaoFactory.Create(tipo, novaRestricaoDTO);
            bool valido = false;
            if (valido = nova.Validar(Base, Parte))
            {
                Restricoes.Add(nova);
            }
            return valido;
        }
        public bool removerRestricao(int id)
        {
            Restricao restricao = null;
            foreach (Restricao r in Restricoes)
            {
                if (r.RestricaoID == id)
                    restricao = r;
            }
            if (restricao == null) return false;
            return Restricoes.Remove(restricao);
        }
        public bool Validar()
        {
            bool valido = true;
            foreach (Restricao r in Restricoes)
            {
                valido &= r.Validar(Base, Parte);
            }
            return valido;
        }
    }
}