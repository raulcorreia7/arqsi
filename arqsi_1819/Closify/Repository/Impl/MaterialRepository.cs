
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

    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository

    {
        public MaterialRepository(ClosifyContext context) : base(context)
        {
        }

        public Task<bool> Exists(string nome)
        {
            return _context.Materiais.AnyAsync(m => m.Nome == nome);
        }

        public override Task<bool> Exists(int id)
        {
            return _context.Materiais.AnyAsync(m => m.MaterialID == id);
        }

        public Task<Material> GetByName(string nome)
        {
            return _context.Materiais.FirstOrDefaultAsync(m => m.Nome == nome);
        }
    }
}