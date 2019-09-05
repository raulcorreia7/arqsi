
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Closify.Data;
using Closify.Models;
using Closify.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Closify.Repository.Impl
{

    public class MaterialAcabamentoRepository : GenericRepository<MaterialAcabamento>, IMaterialAcabamentoRepository

    {
        public MaterialAcabamentoRepository(ClosifyContext context) : base(context)
        {
        }

        public async override Task<bool> Exists(int id)
        {
            return await _context.MaterialAcabamentos.AnyAsync(m => m.MaterialAcabamentoID == id);
        }

        public async Task<bool> Exists(string nomeMaterial, string nomeAcabamento)
        {
            return await _context.MaterialAcabamentos.AnyAsync(m => m.Material.Nome == nomeMaterial && m.Acabamento.Nome == nomeAcabamento);
        }
        public async Task<bool> Exists(int materialID, int acabamentoID)
        {
            return await _context.MaterialAcabamentos.AnyAsync(m => m.Material.MaterialID == materialID && m.Acabamento.AcabamentoID == acabamentoID);
        }
    }
}