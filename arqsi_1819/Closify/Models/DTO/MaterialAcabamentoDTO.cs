

using Newtonsoft.Json;

namespace Closify.Models.DTO
{

    public class MaterialAcabamentoDTO
    {
        public int? id;
        [JsonProperty("Material")]
        public MaterialDTO Material;
        [JsonProperty("Acabamento")]
        public AcabamentoDTO Acabamento;
    }
}