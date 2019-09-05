
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class RestricaoOcupacaoDTO : RestricaoDTO
    {
        [JsonProperty("LarguraMin")]
        public float? LarguraMin { get; set; }
        [JsonProperty("LarguraMax")]
        public float? LarguraMax { get; set; }
        [JsonProperty("AlturaMin")]
        public float? AlturaMin { get; set; }
        [JsonProperty("AlturaMax")]
        public float? AlturaMax { get; set; }
        [JsonProperty("ComprimentoMax")]
        public float? ComprimentoMax { get; set; }
        [JsonProperty("ComprimentoMin")]
        public float? ComprimentoMin { get; set; }

    }

}