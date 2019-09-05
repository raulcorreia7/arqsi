using Closify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Repository.Interfaces
{
    public interface IAcabamentoRepository : IGenericRepository<Acabamento>
    {
        Task<bool> Exists(string nome);
        Task<Acabamento> GetByName(string nome);
    }
}
