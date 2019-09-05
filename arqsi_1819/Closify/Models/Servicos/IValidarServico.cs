
using Closify.Models.DTO;
using Closify.Models.Restricoes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closify.Models.Servico
{
    public enum Validar
    {
        Valido,
        Invalido,
        NaoEncontrado
    };

    public interface IValidarServico
    {

        Task<Validar> ValidarItemDeProduto(ItemDTO itemDTO);
        Task<Validar> ValidarPartesObrigatorias(ItemDTO itemDTO);
    }
}