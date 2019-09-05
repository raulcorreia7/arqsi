using System.Collections.Generic;
using Closify.Models;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class ProdutoDTO
    {
        [JsonProperty("id")]
        public int? ProdutoID { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }
        [JsonProperty("Categoria")]
        public CategoriaDTOID Categoria { get; set; }
        [JsonProperty("Dimensao")]
        public DimensaoDTO DimensaoDTO { get; set; }

        [JsonProperty("MaterialAcabamento")]
        public List<MaterialAcabamentoDTO> MaterialAcabamentoDTO { get; set; }
        [JsonProperty("Partes")]
        public List<int> Partes { get; set; }
    }
}