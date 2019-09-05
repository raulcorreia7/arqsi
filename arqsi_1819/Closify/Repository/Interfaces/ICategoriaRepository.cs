
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Closify.Models;
using Closify.Repository.Interfaces;

namespace Closify.Repository.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        //Task<List<Categoria>> GetAllIncludes();

        Task<bool> Exists(string nome);

        Task<Categoria> GetByName(string nome);
    }
}