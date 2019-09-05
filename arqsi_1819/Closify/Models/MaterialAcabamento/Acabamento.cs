

using System.ComponentModel.DataAnnotations;
using Closify.Models.DTO;

namespace Closify.Models
{

    public class Acabamento
    {
        public int AcabamentoID { get; set; }
        public string Nome { get; set; }
        public Acabamento() { }

        public Acabamento(string nome)
        {
            Nome = nome;
        }

        public Acabamento(AcabamentoDTO dto)
        {
            Nome = dto.Nome;
        }
    }
}