
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class RestricaoListaDTO
    {
        [JsonProperty("Lista")]
        public List<int> lista { get; set; } = new List<int>();
    }
}