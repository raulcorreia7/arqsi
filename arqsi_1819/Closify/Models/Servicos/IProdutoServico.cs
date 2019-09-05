
using Closify.Models.DTO;
using Closify.Models.Restricoes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Closify.Models.Servico
{

    public interface IProdutoServico { 

        Produto ConstruirProdutoDeDTO(ProdutoDTO produtodto);
        List<int> ListaPartesProduto(int id);
        Task DeleteAgregacoes(int id);
        Task<Produto> Create(ProdutoDTO p);
        Task<Produto> Update(int id, ProdutoDTO p);
        Task<List<Produto>> ProdutoPartes(int id);
        Task<List<Produto>> ProdutoPartesEm(int id);
        Task<List<Restricao>> RestricaoProduto(int id);

        Task<List<Produto>> ProdutosSemLigacao(int id);
        Task AgregarProdutos(Produto p, List<int> IDPartes);

        Task<bool> AdicionarMaterialAcabamento(int produtoid, int matAcabid);
    }
}