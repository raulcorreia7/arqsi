using Closify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Repository.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task<Produto> GetProdutoByName(string nome);

        //Task<List<Produto>> GetAllInclude();

    }
}
