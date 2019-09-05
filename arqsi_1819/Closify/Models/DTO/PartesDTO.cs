
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class PartesDTO
    {
        [JsonProperty("Partes")]
        public List<int> Partes = new List<int>();
        
    }
    
}