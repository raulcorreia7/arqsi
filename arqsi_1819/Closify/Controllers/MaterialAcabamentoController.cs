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
    [Route("api/MaterialAcabamento")]
    [ApiController]
    public class MaterialAcabamentoController : ControllerBase
    {
        private readonly IMaterialAcabamentoRepository _repo_mat_acab;

        private readonly IMaterialRepository _repo_material;

        private readonly IAcabamentoRepository _repo_acabamento;

        private readonly IMapper _mapper;

        public MaterialAcabamentoController(IMaterialAcabamentoRepository materialAcabamentoRepository, IAcabamentoRepository acabamentoRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _repo_mat_acab = materialAcabamentoRepository;
            _repo_material = materialRepository;
            _repo_acabamento = acabamentoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matacab = await _repo_mat_acab.GetAll();
            var dtos = _mapper.Map<List<MaterialAcabamento>, List<MaterialAcabamentoDTO>>(matacab);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var mat = await _repo_mat_acab.GetById(id);
            if (mat != null)
            {
                var matdto = _mapper.Map<MaterialAcabamento, MaterialAcabamentoDTO>(mat);
                return Ok(matdto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            if (await _repo_mat_acab.Exists(id))
            {
                await _repo_mat_acab.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterialAcabamento([FromBody] MaterialAcabamentoDTO matacabdto)
        {
            if (!matacabdto.Material.MaterialID.HasValue || !matacabdto.Acabamento.AcabamentoID.HasValue) return BadRequest();
            if (matacabdto.id.HasValue && await _repo_mat_acab.Exists(matacabdto.id.Value)) return BadRequest();
            if (!await _repo_material.Exists(matacabdto.Material.MaterialID.Value))
                return NotFound();
            if (!await _repo_acabamento.Exists(matacabdto.Acabamento.AcabamentoID.Value))
                return NotFound();
            if (await _repo_mat_acab.Exists(matacabdto.Material.MaterialID.Value, matacabdto.Acabamento.AcabamentoID.Value)) return BadRequest();
            
            Material material = await _repo_material.GetById(matacabdto.Material.MaterialID.Value);
            Acabamento acabamento = await _repo_acabamento.GetById(matacabdto.Acabamento.AcabamentoID.Value);

            MaterialAcabamento mat_acab = new MaterialAcabamento(material, acabamento);
            await _repo_mat_acab.Create(mat_acab);
            MaterialAcabamentoDTO dto = _mapper.Map<MaterialAcabamento, MaterialAcabamentoDTO>(mat_acab);
            return Created("Created", dto);
        }

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialAcabamento(int id, [FromBody] MaterialAcabamentoDTO materialdto)
        {
            if(!await _repo_mat_acab.Exists(id)) return NotFound();

            var entity = await _repo_mat_acab.GetById(id);
            entity.Material.Nome=materialdto.Material.Nome;
            entity.Acabamento.Nome=materialdto.Acabamento.Nome;

            await _repo_mat_acab.Update(id,entity);
            var returndto = _mapper.Map<MaterialAcabamento, MaterialAcabamentoDTO>(entity);

            return Ok(returndto);

        }
        */

    }
}
