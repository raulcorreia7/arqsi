
using System.ComponentModel.DataAnnotations.Schema;

namespace Closify.Models
{
    public class ProdutoMaterialAcabamento
    {
        public int ProdutoID { get; set; }

        public int MaterialAcabamentoID { get; set; }

        [ForeignKey("ProdutoID")]
        public virtual Produto Produto { get; set; }

        [ForeignKey("MaterialAcabamentoID")]
        public virtual MaterialAcabamento MaterialAcabamento { get; set; }

        public ProdutoMaterialAcabamento() { }
        public ProdutoMaterialAcabamento(Produto produto, MaterialAcabamento materialAcabamento)
        {
            Produto = produto;
            MaterialAcabamento = materialAcabamento;
        }

        public bool Igual(MaterialAcabamento materialAcabamento)
        {
            if (MaterialAcabamento.Material.Nome == materialAcabamento.Material.Nome
            && MaterialAcabamento.Acabamento.Nome == materialAcabamento.Acabamento.Nome)
            {
                return true;
            }
            return false;
        }
    }
}