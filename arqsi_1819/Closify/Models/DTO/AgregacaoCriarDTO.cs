
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class AgregacaoCriarDTO
    {

        [JsonProperty("Base")]
        public int Base { get; set; }

        [JsonProperty("Parte")]

        public int Parte { get; set; }
    }
}