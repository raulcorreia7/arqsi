
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Closify.Models.DTO;

namespace Closify.Models
{
    public class MaterialAcabamento
    {
        public int MaterialAcabamentoID { get; set; }

        public int MaterialID { get; set; }

        public int AcabamentoID { get; set; }

        [ForeignKey("MaterialID")]
        public virtual Material Material { get; set; }
        [ForeignKey("AcabamentoID")]
        public virtual Acabamento Acabamento { get; set; }

        public MaterialAcabamento() { }

        public MaterialAcabamento(Material material, Acabamento acabamento)
        {
            Material = material;
            Acabamento = acabamento;
        }
        public MaterialAcabamento(MaterialAcabamentoDTO dto)
        {
            Material = new Material(dto.Material);
            Acabamento = new Acabamento(dto.Acabamento);
        }
    }
}