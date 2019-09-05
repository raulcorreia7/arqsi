using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class MaterialDTO
    {
        [JsonProperty("id")]
        public int? MaterialID { get; set; }
        [JsonProperty("Nome")]
        public string Nome { get; set; }
    }
}