using Closify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Repository.Interfaces
{
    public interface IMaterialAcabamentoRepository : IGenericRepository<MaterialAcabamento>
    {
        Task<bool> Exists(string nomeMaterial,string nomeAcabamento);

        Task<bool> Exists(int materialID, int acabamentoID);
    }
}
