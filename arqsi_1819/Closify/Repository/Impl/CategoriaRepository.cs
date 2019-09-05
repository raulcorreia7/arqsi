using Closify.Data;
using Closify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Closify.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Closify.Repository.Impl
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ClosifyContext context) : base(context)
        {
        }

        public async override Task<bool> Exists(int id)
        {
            return await _context.Categorias.AnyAsync(c => c.CategoriaID == id);
        }

        public async Task<bool> Exists(string nome)
        {
            return await _context.Categorias.AnyAsync(c => c.Nome == nome);
        }

        public async Task<Categoria> GetByName(string nome)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == nome);
        }



        /* 
            Depreciado Raúl
            Usamos lazy loadinng
                public async Task<List<Categoria>> GetAllIncludes()
                {
                    return await _context.Categorias.Include(s => s.SubCategorias).ToListAsync();
                }
        */
        /*
                public override async Task<Categoria> GetById(int? id){

                    return await _context.Categorias.Include(s => s.SuperCategoria)
                        .Include(sub => sub.SubCategorias)
                        .FirstOrDefaultAsync();
                }
        */
    }
}
