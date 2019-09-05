using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{
    public class CategoriaDTOID
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
