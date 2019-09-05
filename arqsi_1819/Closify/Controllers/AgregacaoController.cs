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
using Closify.Models.Restricoes;

namespace Closify.Controllers
{
    [Route("api/Agregacao")]
    [ApiController]
    public class AgregacaoController : ControllerBase
    {
        private readonly IAgregacaoRepository _agregacaoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public AgregacaoController(IAgregacaoRepository agregacaoRepository, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _agregacaoRepository = agregacaoRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgregacoes()
        {
            var agregacoes = await _agregacaoRepository.GetAll();
            var dto = _mapper.Map<List<Agregacao>, List<AgregacaoDTO>>(agregacoes);
            return Ok(dto);
        }

        [HttpGet("{id}/PBase")]
        public async Task<IActionResult> GetAgregacoesProdutoBase(int id)
        {
            var agregacoes = await _agregacaoRepository.GetAllAgregacoesByProdutoBaseID(id);
            var dto = _mapper.Map<List<Agregacao>, List<AgregacaoDTO>>(agregacoes);
            return Ok(dto);
        }

        // GET: Agregacao/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var agregacao = await _agregacaoRepository.GetById(id);
            if (agregacao == null) return NotFound();
            var dto = _mapper.Map<Agregacao, AgregacaoDTO>(agregacao);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgregacaoCriarDTO agregacaodto)
        {
            var pBase = await _produtoRepository.GetById(agregacaodto.Base);
            var pParte = await _produtoRepository.GetById(agregacaodto.Parte);

            if (pBase == null || pParte == null)
            {
                return NotFound();
            }

            Agregacao a = new Agregacao(pBase, pParte);
            await _agregacaoRepository.Create(a);

            var dto = _mapper.Map<Agregacao, AgregacaoDTO>(a);
            return Created("Agregacao Criada", dto);
        }

        [HttpPut("{id}/Restricao")]
        public async Task<IActionResult> AdicionarRestricao(int id, [FromBody] NovaRestricaoDTO novaRestricao)
        {
            if (!RestricaoFactory.Valid(novaRestricao.Restricao)) return BadRequest();
            if (!await _agregacaoRepository.Exists(id)) return NotFound();
            Agregacao entity = await _agregacaoRepository.GetById(id);

            if (entity.adicionarRestricao(novaRestricao))
            {
                await _agregacaoRepository.Update(id, entity);
                var dto = _mapper.Map<Agregacao, AgregacaoDTO>(entity);
                return Ok(dto);
            }
            return BadRequest();
        }

        [HttpDelete("{id}/Restricao/{idrestricao}")]
        public async Task<IActionResult> RemoverRestricao(int id, int idrestricao)
        {
            if (!await _agregacaoRepository.Exists(id)) return NotFound();
            Agregacao entity = await _agregacaoRepository.GetById(id);
            if (!entity.removerRestricao(idrestricao)) return BadRequest();
            await _agregacaoRepository.Update(id, entity);
            var dto = _mapper.Map<Agregacao, AgregacaoDTO>(entity);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgregacao(int id)
        {
            var agregacao = await _agregacaoRepository.GetById(id);
            if (agregacao == null)
            {
                return NotFound();
            }
            await _agregacaoRepository.Delete(id);
            return Ok();
        }
        /*
        Não faz sentido alterar Agregações
        Raúl
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAgregacao(int id, [FromBody] Agregacao agregacao)
        {
            if (id != agregacao.AgregacaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(id, agregacao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.Exists(agregacao.AgregacaoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(agregacao);
            }
            return BadRequest();
        }
        */

    }
}
