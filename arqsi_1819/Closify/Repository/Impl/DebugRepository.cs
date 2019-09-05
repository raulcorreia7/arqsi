
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Closify.Data;
using Closify.Models;
using Closify.Models.Restricoes;
using Closify.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Closify.Repository.Impl
{
    public class DebugRepository : IDebugRepository
    {

        public readonly ClosifyContext _context;
        public DebugRepository(ClosifyContext context)
        {
            _context = context;
        }
        public void CleanDatabase()
        {
            CleanOneTable<Categoria>();
            CleanOneTable<Produto>();
            CleanOneTable<Agregacao>();
            CleanOneTable<MaterialAcabamento>();
            CleanOneTable<Material>();
            CleanOneTable<Acabamento>();
            CleanOneTable<Dimensao>();
            CleanOneTable<ValorNumerico>();
            CleanOneTable<Restricao>();
            CleanOneTable<ProdutoMaterialAcabamento>();
            _context.SaveChanges();
            TruncateTables();
            _context.SaveChangesAsync();

        }

        private void CleanOneTable<T>() where T : class
        {
            //string QUERY = "TRUNCATE TABLE [" + typeof(T).Name + "]";
            _context.Set<T>().RemoveRange(_context.Set<T>());
        }
        private void TruncateTables()
        {
            const string TOKEN = "%replace";
            //const string TRUNCATE = "TRUNCATE TABLE [" + TOKEN + "]";
            const string RESEED = "DBCC CHECKIDENT ('%replace', RESEED, 0);";
            //_context.Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? NOCHECK CONSTRAINT all\"");
            List<string> classes = new List<string>();
            classes.Add("Agregacoes");//typeof(Agregacao).Name);
            classes.Add("Restricoes");
            classes.Add("Acabamentos");
            classes.Add("Categorias");
            classes.Add("Dimensoes");
            classes.Add("Materiais");
            classes.Add("MaterialAcabamentos");
            //classes.Add("ProdutoMaterialAcabamentos");
            classes.Add("Produtos");
            classes.Add("ValorNumerico");

            foreach (string classname in classes)
            {
                //string query = TRUNCATE.Replace(TOKEN, classname + "s");
                //_context.Database.ExecuteSqlCommand(query);
                string reseed = RESEED.Replace(TOKEN, classname);
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
                _context.Database.ExecuteSqlCommand(reseed);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
            }
            //_context.Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all\"");
        }
    }
}