using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Closify.Data;
using Closify.Models;
using Closify.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Closify.Repository.Interfaces;
namespace Closify.Controllers
{
    [Route("api/Acabamento")]
    [ApiController]
    public class AcabamentoController : ControllerBase
    {
        private readonly IAcabamentoRepository _repository;

        private readonly IMapper _mapper;

        public AcabamentoController(IAcabamentoRepository acabamentoRepository, IMapper mapper)
        {
            _repository = acabamentoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string nome ="")
        {

            if (string.IsNullOrEmpty(nome))
            {
            var acab = await _repository.GetAll();
            var dtos = _mapper.Map<List<Acabamento>, List<AcabamentoDTO>>(acab);
            return Ok(dtos);
            }
            else
            {
                if (!await _repository.Exists(nome))
                {
                    return NotFound();
                }
                else
                {
                    var entity = await _repository.GetByName(nome);
                    var dto = _mapper.Map<Acabamento, AcabamentoDTO>(entity);
                    return Ok(dto);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var entity = await _repository.GetById(id);
            if (entity != null)
            {
                var matdto = _mapper.Map<Acabamento, AcabamentoDTO>(entity);
                return Ok(matdto);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            if (await _repository.Exists(id))
            {
                await _repository.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAcabamento([FromBody] AcabamentoDTO acabamentodto)
        {
            if (string.IsNullOrEmpty(acabamentodto.Nome)) return BadRequest();
            if (await _repository.Exists(acabamentodto.Nome)) return BadRequest();

            Acabamento entity = new Acabamento(acabamentodto);
            await _repository.Create(entity);
            AcabamentoDTO dto = _mapper.Map<Acabamento, AcabamentoDTO>(entity);
            return Created("Created Acabamento",dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialAcabamento(int id, [FromBody] AcabamentoDTO acabamentodto)
        {
            if (string.IsNullOrEmpty(acabamentodto.Nome)) return BadRequest();
            if (!await _repository.Exists(id)) return NotFound();
            var entity = await _repository.GetById(id);
            entity.Nome = acabamentodto.Nome;
            await _repository.Update(id, entity);
            var returndto = _mapper.Map<Acabamento, AcabamentoDTO>(entity);
            return Ok(returndto);

        }

    }
}
