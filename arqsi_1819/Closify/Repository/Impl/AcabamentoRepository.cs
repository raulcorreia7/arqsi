
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

    public class AcabamentoRepository : GenericRepository<Acabamento>, IAcabamentoRepository

    {
        public AcabamentoRepository(ClosifyContext context) : base(context)
        {
        }

        public Task<bool> Exists(string nome)
        {
            return _context.Acabamentos.AnyAsync(m => m.Nome == nome);
        }

        public override Task<bool> Exists(int id)
        {
            return _context.Acabamentos.AnyAsync(m => m.AcabamentoID == id);
        }

        public Task<Acabamento> GetByName(string nome)
        {
            return _context.Acabamentos.FirstOrDefaultAsync(m => m.Nome == nome);
        }
    }
}