

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{
    public class DimensaoDTO
    {

        [JsonProperty("TipoComprimento")]
        public string TipoComprimento { get; set;}
        [JsonProperty("TipoLargura")]
        public string TipoLargura { get;set;}
        [JsonProperty("TipoAltura")]
        public string TipoAltura{get;set;}
        [JsonProperty("Comprimento")]
        public List<double> Comprimento{get;set;}
        [JsonProperty("Largura")]
        public List<double> Largura{get;set;}
        [JsonProperty("Altura")]
        public List<double> Altura{get;set;}

    }
}