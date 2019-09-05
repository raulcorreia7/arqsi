
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class AcabamentoDTO
    {
        [JsonProperty("id")]
        public int? AcabamentoID { get; set;}

        [JsonProperty("Nome")]
        public string Nome { get; set; }
    }

}