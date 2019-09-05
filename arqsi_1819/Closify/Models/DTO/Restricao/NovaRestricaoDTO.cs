
using Closify.Models.Restricoes;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class NovaRestricaoDTO
    {
        [JsonProperty("Restricao")]
        public TipoRestricao Restricao { get; set; } = TipoRestricao.Generica;

        //public RestricaoCaberDTO Caber { get; set; }

        //public RestricaoObrigatoria Obrigatoria { get; set; }
        [JsonProperty("Ocupacao")]
        
        public RestricaoOcupacaoDTO Ocupacao { get; set; }

        

    }

}