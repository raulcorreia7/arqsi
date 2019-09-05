using Closify.Data;
using Closify.Models;
using Closify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Closify.Repository.Impl
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ClosifyContext context) : base(context)
        {
        }
        public async override Task<bool> Exists(int id)
        {
            return await _context.Produtos.AnyAsync(m => m.ProdutoID == id);
        }

        public Task<Produto> GetProdutoByName(string nome)
        {
            return _context.Produtos.Where(p => p.Nome == nome).FirstOrDefaultAsync();
        }
        /*
        Depreciado, usamos lazy loading
                public async Task<List<Produto>> GetAllInclude()
                {
                    return await _context.Produtos
                        .Include(p => p.Categoria)
                        .Include(p => p.Dimensao)
                        .Include(p => p.Dimensao.Altura)
                        .Include(p => p.Dimensao.Comprimento)
                        .Include(p => p.Dimensao.Largura)
                        //.Include(p => p.Material)
                        .ToListAsync();
                }
        */
    }
}
