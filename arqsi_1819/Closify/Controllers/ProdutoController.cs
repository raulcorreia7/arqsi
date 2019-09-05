using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Closify.Data;
using Closify.Models;
using Closify.Models.DTO;
using Closify.Models.Restricoes;
using Closify.Models.Servico;
using Closify.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Closify.Controllers
{
    [Route("api/Produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProdutoServico _servico;

        public ProdutoController(IProdutoRepository repository, IMapper mapper, IProdutoServico servico)
        {
            _repository = repository;
            _mapper = mapper;
            _servico = servico;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllProdutos(string nome = "")
        {
            if (string.IsNullOrEmpty(nome))
            {
                var produtos = await _repository.GetAll();
                List<ProdutoDTO> produtoDTOs = converterProdutosEmDTO(produtos);
                return Ok(produtoDTOs);
            }
            else
            {
                var produto = await _repository.GetProdutoByName(nome);
                if (produto == null) return NotFound();

                var produtodto = converterProdutoEmDTO(produto);
                return Ok(produtodto);

            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _repository.GetById(id);
            if (produto == null) return NotFound();

            var produtodto = converterProdutoEmDTO(produto);
            return Ok(produtodto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDTO produtodto)
        {
            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            await _servico.Update(id, produtodto);
            return Ok(produtodto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] ProdutoDTO produto)
        {
            //FIXME:, verificar se o produto foi realmente criado
            Produto p = await _servico.Create(produto);
            var dto = converterProdutoEmDTO(p);
            return Created("Produto criado", dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            //Raúl:Fixme, serviço deve apagar o produto por completo
            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            await _servico.DeleteAgregacoes(id);
            //Não apaga dimensoes
            await _repository.Delete(id);
            return Ok();
        }


        [HttpGet("{id:int}/Partes")]
        public async Task<IActionResult> PartesDoProduto(int id)
        {
            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            var produtos = await _servico.ProdutoPartes(id);
            var partesproduto = _mapper.Map<List<Produto>, List<ProdutoDTO>>(produtos);
            return Ok(partesproduto);
        }

        [HttpGet("{id:int}/PartesSimples")]
        public async Task<IActionResult> PartesDoProdutoSimples(int id)
        {

            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            var produtos = await _servico.ProdutoPartes(id);
            PartesDTO dto = new PartesDTO();
            produtos.ForEach(p => dto.Partes.Add(p.ProdutoID));
            return Ok(dto);
        }


        [HttpPost("{id:int}/Partes")]
        public async Task<IActionResult> AcrescentarPartes(int id, [FromBody] PartesDTO partesdto)
        {
            var produto = await _repository.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }

            var produtos = await _servico.ProdutoPartes(id);
            List<int> idProdutos = new List<int>();
            foreach (Produto p in produtos) idProdutos.Add(p.ProdutoID);

            List<int> novasPartes = new List<int>();
            foreach (int idpartes in partesdto.Partes)
            {
                if (!idProdutos.Contains(idpartes))
                    novasPartes.Add(idpartes);
            }
            await _servico.AgregarProdutos(produto, novasPartes);
            await _repository.SaveChangesAsync();

            var produtodto = converterProdutoEmDTO(produto);
            return Ok(produtodto);
        }

        [HttpGet("{id:int}/ParteEm")]
        public async Task<IActionResult> ParteEmProdutos(int id)
        {
            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            var produtos = await _servico.ProdutoPartesEm(id);
            var partesproduto = _mapper.Map<List<Produto>, List<ProdutoDTO>>(produtos);
            return Ok(partesproduto);
        }

        [HttpGet("{id:int}/NaoParte")]

        public async Task<IActionResult> ProdutosSemLigacao(int id)
        {
            bool exists = await _repository.Exists(id);
            if (!exists) return NotFound();

            var produtos = await _servico.ProdutosSemLigacao(id);
            var produtosemligacao = _mapper.Map<List<Produto>, List<ProdutoDTO>>(produtos);
            return Ok(produtosemligacao);
        }

        [HttpGet("{id:int}/Restricoes")]
        public async Task<IActionResult> Restricoes(int id)
        {
            bool exist = await _repository.Exists(id);
            if (!exist) return NotFound();

            var restricoes = await _servico.RestricaoProduto(id);
            var restricoesdto = _mapper.Map<List<Restricao>, List<RestricaoDTO>>(restricoes);
            return Ok(restricoesdto);
        }

        /*
            Raúl
            Métodos para converter produto em DTO,
            Acrescenta as devidas partes no dto
         */
        private ProdutoDTO converterProdutoEmDTO(Produto p)
        {
            var pdto = _mapper.Map<Produto, ProdutoDTO>(p);
            pdto.Partes = _servico.ListaPartesProduto(p.ProdutoID);
            return pdto;
        }
        private List<ProdutoDTO> converterProdutosEmDTO(List<Produto> produtos)
        {
            List<ProdutoDTO> listaProdutosDTO = new List<ProdutoDTO>();
            foreach (Produto p in produtos)
            {
                var pdto = _mapper.Map<Produto, ProdutoDTO>(p);
                pdto.Partes = _servico.ListaPartesProduto(p.ProdutoID);
                listaProdutosDTO.Add(pdto);
            }
            return listaProdutosDTO;
        }

        [HttpPut("{produto:int}/MaterialAcabamento/{maid:int}")]
        public async Task<IActionResult> AdicionarMaterialAcabamento(int produto, int maid)
        {
            bool added = false;
            if (!await _repository.Exists(produto)) return NotFound();
            added = await _servico.AdicionarMaterialAcabamento(produto, maid);
            if (added) return Ok();
            return BadRequest();
        }
    }
}