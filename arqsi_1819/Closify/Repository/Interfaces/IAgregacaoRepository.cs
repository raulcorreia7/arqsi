using Closify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Closify.Models.Restricoes;

namespace Closify.Repository.Interfaces
{
    public interface IAgregacaoRepository : IGenericRepository<Agregacao>
    {
        Task<List<Agregacao>> GetAllAgregacoesByProdutoBaseID(int id);
        Task<List<Agregacao>> GetAllAgregacoesByProdutoParteID(int id);
        Task<List<Produto>> GetAllPartes(int id);
        Task<List<Produto>> GetAllPartesEm(int id);
        Task<List<Restricao>> GetAllRestricoesByProdutoBaseID(int id);
        Task<Agregacao> GetByProducts(int pBase, int pParte);
    }
}
