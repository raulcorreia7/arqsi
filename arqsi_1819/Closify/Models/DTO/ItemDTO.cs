using System.Collections.Generic;

namespace Closify.Models.DTO
{
    public class ItemDTO
    {
        public int Produto_id { get; set; }
        public string Nome { get; set; }
        public string Material { get; set; }
        public string Acabamento { get; set; }
        public DimensaoDTO Dimensao { get; set; }
        public List<ItemDTO> Partes { get; set; }
    }
}