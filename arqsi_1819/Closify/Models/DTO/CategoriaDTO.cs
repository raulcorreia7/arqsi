using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Closify.Models.DTO
{
    public class CategoriaDTO
    {

        [JsonProperty("id")]
        public int CategoriaDTOID { get; set; }
        [JsonProperty("Nome")]
        public string Nome { get; set; }

        public int? SuperCategoria { get; set; }
        [JsonProperty("SubCategorias")]

        public IList<CategoriaDTOID> SubCategorias { get; set; } = new List<CategoriaDTOID>();

        public CategoriaDTO() { }

    }
}
