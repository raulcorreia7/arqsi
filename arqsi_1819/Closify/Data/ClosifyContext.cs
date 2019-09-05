using Microsoft.EntityFrameworkCore;
using Closify.Models;
using Closify.Models.Restricoes;
namespace Closify.Data
{
    public class ClosifyContext : DbContext
    {
        public ClosifyContext(DbContextOptions<ClosifyContext> options)
        : base(options) { }

        // public DbSet<Test> Tests { get; set; }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Acabamento> Acabamentos { get; set; }

        public DbSet<Dimensao> Dimensoes { get; set; }

        public DbSet<Agregacao> Agregacoes { get; set; }
        public DbSet<Restricao> Restricoes { get; set; }

        public DbSet<MaterialAcabamento> MaterialAcabamentos { get; set; }
        public DbSet<ProdutoMaterialAcabamento> ProdutoMaterialAcabamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<ItemDeProduto>()
                .HasOne(pb => pb.ProdutoBase)
                .WithMany(p => p.ItemsDeProdutos);
            */
            modelBuilder.Entity<Produto>()
                .HasOne(d => d.Dimensao)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Produto>()
            .HasMany(m => m.ProdutoMaterialAcabamentos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProdutoMaterialAcabamento>()
            .HasKey(pma => new { pma.ProdutoID, pma.MaterialAcabamentoID });

            modelBuilder.Entity<ProdutoMaterialAcabamento>()
            .HasOne(m => m.MaterialAcabamento)
            .WithMany()
            .HasForeignKey(m => m.MaterialAcabamentoID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProdutoMaterialAcabamento>()
            .HasOne(m => m.Produto)
            .WithMany()
            .HasForeignKey(m => m.ProdutoID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaterialAcabamento>()
            .HasOne(m => m.Material)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaterialAcabamento>()
            .HasOne(m => m.Acabamento)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RestricaoGenerica>()
                .HasBaseType<Restricao>();
            modelBuilder.Entity<RestricaoCaber>()
            .HasBaseType<Restricao>();
            modelBuilder.Entity<RestricaoMaterial>()
            .HasBaseType<Restricao>();
            modelBuilder.Entity<RestricaoObrigatoria>()
            .HasBaseType<Restricao>();
            modelBuilder.Entity<RestricaoOcupacao>()
            .HasBaseType<Restricao>();
            modelBuilder.Entity<RestricaoOpcional>()
            .HasBaseType<Restricao>();

            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.SuperCategoria)
                .WithMany()
                .HasForeignKey(c => c.SuperCatID);

            modelBuilder.Entity<Agregacao>()
            .HasOne(p => p.Parte)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Agregacao>()
            .HasOne(p => p.Base)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agregacao>()
            .HasMany(r => r.Restricoes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Restricao>()
            .HasDiscriminator<string>("res_type")
            .HasValue<RestricaoGenerica>("reg_gen")
            .HasValue<RestricaoCaber>("res_caber");
        }
    }
}