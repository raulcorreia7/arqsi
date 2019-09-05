using Closify.Data;
using Closify.Models;
using Closify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Closify.Models.Restricoes;

namespace Closify.Repository.Impl
{
    public class AgregacaoRepository : GenericRepository<Agregacao>, IAgregacaoRepository
    {
        public AgregacaoRepository(ClosifyContext context) : base(context)
        {
        }


        private IQueryable<Agregacao> QueryWithIncludes()
        {

            return _context.Agregacoes
                            .Include(p => p.Base)
                            .Include(p => p.Parte)
                            .Include(p => p.Restricoes);
        }

        public async Task<List<Agregacao>> GetAllAgregacoesByProdutoBaseID(int id)
        {
            return await QueryWithIncludes().Where(a => a.BaseID == id)
                            .ToListAsync();
        }

        public async Task<Agregacao> GetByProducts(int pBase, int pParte)
        {
            return await QueryWithIncludes().Where(a => a.BaseID == pBase && a.ParteID == pParte).FirstOrDefaultAsync();
        }

        public async Task<List<Agregacao>> GetAllAgregacoesByProdutoParteID(int id)
        {
            return await QueryWithIncludes().Where(a => a.ParteID == id).ToListAsync();
        }
        public async override Task<bool> Exists(int id)
        {
            return await _context.Agregacoes.AnyAsync(a => a.AgregacaoID == id);
        }

        public async Task<List<Produto>> GetAllPartes(int id)
        {
            List<Produto> produtos = new List<Produto>();
            List<Agregacao> agregacoes = await QueryWithIncludes().Where(a => a.BaseID == id)
                            .ToListAsync();

            foreach(Agregacao a in agregacoes)
            {
                produtos.Add(a.Parte);
            }
            return produtos;
        }

        public async Task<List<Produto>> GetAllPartesEm(int id)
        {
            List<Produto> produtos = new List<Produto>();
            List<Agregacao> agregacoes = await QueryWithIncludes().Where(a => a.ParteID == id)
                            .ToListAsync();

            foreach (Agregacao a in agregacoes)
            {
                produtos.Add(a.Base);
            }
            return produtos;
        }

        public async Task<List<Restricao>> GetAllRestricoesByProdutoBaseID(int id)
        {
            List<Restricao> restricoes = new List<Restricao>();
            List<Agregacao> agregacoes = await QueryWithIncludes().Where(a => a.BaseID == id)
                            .ToListAsync();
            foreach (Agregacao a in agregacoes)
            {
                foreach(Restricao r in a.Restricoes)
                {
                    restricoes.Add(r);
                }
            }

            return restricoes;
        }
    }
}
