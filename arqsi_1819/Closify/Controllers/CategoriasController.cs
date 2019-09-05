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
namespace Closify.Controllers
{
    [Route("api/Categoria")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorias(string nome = "")
        {
            if (string.IsNullOrEmpty(nome))
            {
                var categorias = await _repository.GetAll();
                var categoriasdto = _mapper.Map<List<Categoria>, List<CategoriaDTO>>(categorias);
                return Ok(categoriasdto);
            }
            else
            {
                if (!await _repository.Exists(nome))
                {
                    return NotFound();
                }
                else
                {
                    var cat = await _repository.GetByName(nome);
                    var dto = _mapper.Map<Categoria, CategoriaDTO>(cat);
                    return Ok(dto);
                }
            }
        }

        // GET: Categoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var catdto = _mapper.Map<Categoria, CategoriaDTO>(categoria);

            return Ok(catdto);
        }

        // POST: Categorias/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaDTO categoriadto)
        {
            Categoria cat;
            if (categoriadto.SuperCategoria == null)
            {
                cat = new Categoria { Nome = categoriadto.Nome, SuperCatID = null };
                await _repository.Create(cat);
            }
            else
            {
                cat = new Categoria { Nome = categoriadto.Nome, SuperCatID = categoriadto.SuperCategoria.Value };

                int SuperCategoriaID = categoriadto.SuperCategoria.Value;
                var SuperCategoria = await _repository.GetById(SuperCategoriaID);

                SuperCategoria.AdicionarSubCategoria(cat);
                await _repository.SaveChangesAsync();
            }
            var dto = _mapper.Map<Categoria, CategoriaDTO>(cat);
            return Created("Categoria criada", dto);
        }

        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategoria(int id, [FromBody] CategoriaDTO categoriadto)
        {
            if (String.IsNullOrEmpty(categoriadto.Nome)) return BadRequest();

            Categoria categoria = await _repository.GetById(id);
            categoria.Nome = categoriadto.Nome;

            await _repository.Update(id, categoria);
            CategoriaDTO dto = _mapper.Map<Categoria, CategoriaDTO>(categoria);
            return Ok(dto);
        }

        // POST: Categorias/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (!_repository.Exists(id).Result) return NotFound();
            await _repository.Delete(id);
            return RedirectToAction(nameof(GetAllCategorias));
        }
    }
}
