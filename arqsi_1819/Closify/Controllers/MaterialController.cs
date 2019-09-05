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
    [Route("api/Material")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _repository;

        private readonly IMapper _mapper;

        public MaterialController(IMaterialRepository materialRepository, IMapper mapper)
        {
            _repository = materialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string nome = "")
        {
            if (string.IsNullOrEmpty(nome))
            {
                var mat = await _repository.GetAll();
                var dtos = _mapper.Map<List<Material>, List<MaterialDTO>>(mat);
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
                    var dto = _mapper.Map<Material, MaterialDTO>(entity);
                    return Ok(dto);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var mat = await _repository.GetById(id);
            if (mat != null)
            {
                var matdto = _mapper.Map<Material, MaterialDTO>(mat);
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
        public async Task<IActionResult> CreateMaterial([FromBody] MaterialDTO materialdto)
        {
            if (string.IsNullOrEmpty(materialdto.Nome)) return BadRequest();
            if (await _repository.Exists(materialdto.Nome)) return BadRequest();

            Material material = new Material(materialdto);
            await _repository.Create(material);
            MaterialDTO dto = _mapper.Map<Material, MaterialDTO>(material);
            return Created("Created Material",dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialAcabamento(int id, [FromBody] MaterialDTO materialdto)
        {
            if (string.IsNullOrEmpty(materialdto.Nome)) return BadRequest();
            if (!await _repository.Exists(id)) return NotFound();

            var entity = await _repository.GetById(id);
            entity.Nome = materialdto.Nome;
            await _repository.Update(id, entity);
            var returndto = _mapper.Map<Material, MaterialDTO>(entity);
            return Ok(returndto);

        }

    }
}
