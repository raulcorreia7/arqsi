
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{
    public class AgregacaoDTO
    {

        public int? id { get; set; }
        [JsonProperty("ProdutoBase")]
        public ProdutoDTO ProdutoBase { get; set; }
        [JsonProperty("ProdutoParte")]

        public ProdutoDTO ProdutoParte { get; set; }
        [JsonProperty("Restricoes")]
        public List<RestricaoDTO> Restricoes { get; set; }

    }
}