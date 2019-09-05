using Closify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Repository.Interfaces
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        Task<bool> Exists(string nome);
        Task<Material> GetByName(string nome);
    }
}
