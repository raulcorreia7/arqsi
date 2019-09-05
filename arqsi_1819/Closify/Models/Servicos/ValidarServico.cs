using Closify.Models.DTO;
using Closify.Models.Restricoes;
using Closify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Closify.Models.Servico
{

    public class ValidarServico : IValidarServico
    {
        readonly IProdutoRepository _produto_repo;
        readonly IAgregacaoRepository _agregacao_repo;

        public ValidarServico(IProdutoRepository produtorepo, IAgregacaoRepository agregacaorepo)
        {
            _produto_repo = produtorepo;
            _agregacao_repo = agregacaorepo;
        }

        public async Task<Validar> ValidarItemDeProduto(ItemDTO itemDTO)
        {
            bool valid = true;

            if (!await _produto_repo.Exists(itemDTO.Produto_id))
            {
                return Validar.NaoEncontrado;
            }

            Produto pOriginal = await _produto_repo.GetById(itemDTO.Produto_id);
            valid &= pOriginal.validarMaterialAcabamento(itemDTO.Material, itemDTO.Acabamento);

            Dimensao d = new Dimensao(itemDTO.Dimensao);

            valid &= pOriginal.Dimensao.Validar(d);
            if (!valid) return Validar.Invalido;
            Produto concretizacao = new Produto(itemDTO);
            return await ValidarPartes(concretizacao, itemDTO.Partes);
        }

        private async Task<Validar> ValidarPartes(Produto pBase, List<ItemDTO> partes)
        {
            if (partes == null || partes.Count == 0)
            {
                return Validar.Valido;
            }

            foreach (ItemDTO item in partes)
            {
                if (await ValidarItemDeProduto(item) == Validar.Invalido || await ValidarAgregacao(pBase, item) == Validar.Invalido)
                {
                    return Validar.Invalido;
                }
            }
            return Validar.Valido;
        }

        private async Task<Validar> ValidarAgregacao(Produto pBase, ItemDTO item)
        {
            Agregacao a = await _agregacao_repo.GetByProducts(pBase.ProdutoID, item.Produto_id);
            if (a == null)
                return Validar.Invalido;
            Produto pParte = new Produto(item);
            bool valido = true;
            foreach (Restricao r in a.Restricoes)
            {
                switch (r.TipoRestricao)
                {
                    case TipoRestricao.Caber:
                    case TipoRestricao.Material:
                    case TipoRestricao.Ocupacao:
                        valido &= r.Validar(pBase, pParte);
                        break;
                    default:
                        break;
                }
            }
            if (valido)
                return Validar.Valido;
            return Validar.Invalido;
        }

        public async Task<Validar> ValidarPartesObrigatorias(ItemDTO itemDTO)
        {
            List<Agregacao> agregacoes = await _agregacao_repo.GetAllAgregacoesByProdutoBaseID(itemDTO.Produto_id);
            if (agregacoes == null) return Validar.Invalido;

            List<int> idProdutosParte = new List<int>();
            List<int> idProdutosObrigatorios = new List<int>();

            foreach (Agregacao a in agregacoes)
            {
                foreach (Restricao r in a.Restricoes)
                {
                    if (r.EdoMesmoTipo(TipoRestricao.Obrigatoria))
                    {
                        idProdutosObrigatorios.Add(a.ParteID);
                    }
                }
            }

            foreach (ItemDTO item in itemDTO.Partes)
            {
                idProdutosParte.Add(item.Produto_id);
            }

            // Verifica se existem elementos na lista de produtos obrigatorios que nao estejam na lista de produtos parte
            if (!idProdutosObrigatorios.Except(idProdutosParte).Any())
            {
                Validar v = Validar.Valido;
                foreach (ItemDTO item in itemDTO.Partes)
                {
                    v &= await ValidarPartesObrigatorias(item);
                }
                return v;

            }

            return Validar.Invalido;
        }
    }
}