

using Closify.Models.Restricoes;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class RestricaoDTO
    {

        public int id;

        [JsonProperty("Tipo")]
        public TipoRestricao Tipo;

        [JsonProperty("Nome")]
        public string Nome;


    }
}