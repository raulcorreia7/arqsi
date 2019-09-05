using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Closify.Data;
using Closify.Models;
using AutoMapper;
using Closify.Models.DTO;
using Closify.Repository;
using Closify.Repository.Interfaces;
using Closify.Models.Servico;

namespace Closify.Controllers
{
    [Route("api/Validar")]
    [ApiController]
    public class ValidarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IValidarServico _servico;
        public ValidarController(IValidarServico servico, IMapper mapper)
        {
            _mapper = mapper;
            _servico = servico;
        }
        // POST: Categorias/Create
        [HttpPost("ItemDeProduto")]
        public async Task<IActionResult> ValidarItemDeProduto([FromBody] ItemDTO itemDTO)
        {
            Validar v = await _servico.ValidarItemDeProduto(itemDTO);
            switch (v)
            {
                case Validar.Invalido:
                    return BadRequest();
                case Validar.Valido:
                    return Ok("Ok");
                case Validar.NaoEncontrado:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }

        [HttpPost("Encomenda")]
        public async Task<IActionResult> ValidarEncomenda([FromBody] EncomendaDTO encomendaDTO)
        {

            foreach (ItemDTO item in encomendaDTO.Items)
            {
                Validar v = await _servico.ValidarItemDeProduto(item);
                if (v == Validar.Invalido)
                    return BadRequest();
                v = await _servico.ValidarPartesObrigatorias(item);
                if (v == Validar.Invalido)
                    return BadRequest();
            }
            return Ok("Ok");
        }
    }
}
