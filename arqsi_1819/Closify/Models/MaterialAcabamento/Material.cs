
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Closify.Models.DTO;

namespace Closify.Models
{
    public class Material
    {
        public int MaterialID { get; set; }

        public string Nome { get; set; }

        public Material() { }
        public Material(string nome)
        {
            Nome = nome;
        }

        public Material(MaterialDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
            {
                throw new ArgumentException("Inválido Nome Material");
            }
            Nome = dto.Nome;
        }
    }
}