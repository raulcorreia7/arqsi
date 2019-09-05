﻿// <auto-generated />
using System;
using Closify.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Closify.Migrations
{
    [DbContext(typeof(ClosifyContext))]
    partial class ClosifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Closify.Models.Acabamento", b =>
                {
                    b.Property<int>("AcabamentoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("AcabamentoID");

                    b.ToTable("Acabamentos");
                });

            modelBuilder.Entity("Closify.Models.Agregacao", b =>
                {
                    b.Property<int>("AgregacaoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseID");

                    b.Property<int>("ParteID");

                    b.HasKey("AgregacaoID");

                    b.HasIndex("BaseID");

                    b.HasIndex("ParteID");

                    b.ToTable("Agregacoes");
                });

            modelBuilder.Entity("Closify.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaID1");

                    b.Property<string>("Nome");

                    b.Property<int?>("SuperCatID");

                    b.HasKey("CategoriaID");

                    b.HasIndex("CategoriaID1");

                    b.HasIndex("SuperCatID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Closify.Models.Dimensao", b =>
                {
                    b.Property<int>("DimensaoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TipoValorAltura");

                    b.Property<int>("TipoValorComprimento");

                    b.Property<int>("TipoValorLargura");

                    b.HasKey("DimensaoID");

                    b.ToTable("Dimensoes");
                });

            modelBuilder.Entity("Closify.Models.Material", b =>
                {
                    b.Property<int>("MaterialID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("MaterialID");

                    b.ToTable("Materiais");
                });

            modelBuilder.Entity("Closify.Models.MaterialAcabamento", b =>
                {
                    b.Property<int>("MaterialAcabamentoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AcabamentoID");

                    b.Property<int>("MaterialID");

                    b.HasKey("MaterialAcabamentoID");

                    b.HasIndex("AcabamentoID");

                    b.HasIndex("MaterialID");

                    b.ToTable("MaterialAcabamentos");
                });

            modelBuilder.Entity("Closify.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaID");

                    b.Property<int>("DimensaoID");

                    b.Property<string>("Nome");

                    b.HasKey("ProdutoID");

                    b.HasIndex("CategoriaID");

                    b.HasIndex("DimensaoID")
                        .IsUnique();

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Closify.Models.ProdutoMaterialAcabamento", b =>
                {
                    b.Property<int>("ProdutoID");

                    b.Property<int>("MaterialAcabamentoID");

                    b.Property<int?>("ProdutoID1");

                    b.HasKey("ProdutoID", "MaterialAcabamentoID");

                    b.HasIndex("MaterialAcabamentoID");

                    b.HasIndex("ProdutoID1");

                    b.ToTable("ProdutoMaterialAcabamentos");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.Restricao", b =>
                {
                    b.Property<int>("RestricaoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgregacaoID");

                    b.Property<string>("NomeRestricao");

                    b.Property<int>("TipoRestricao");

                    b.Property<string>("res_type")
                        .IsRequired();

                    b.HasKey("RestricaoID");

                    b.HasIndex("AgregacaoID");

                    b.ToTable("Restricoes");

                    b.HasDiscriminator<string>("res_type").HasValue("Restricao");
                });

            modelBuilder.Entity("Closify.Models.ValorNumerico", b =>
                {
                    b.Property<int>("ValorNumericoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DimensaoID");

                    b.Property<int?>("DimensaoID1");

                    b.Property<int?>("DimensaoID2");

                    b.Property<double>("Valor");

                    b.HasKey("ValorNumericoID");

                    b.HasIndex("DimensaoID");

                    b.HasIndex("DimensaoID1");

                    b.HasIndex("DimensaoID2");

                    b.ToTable("ValorNumerico");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoCaber", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");


                    b.ToTable("RestricaoCaber");

                    b.HasDiscriminator().HasValue("res_caber");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoGenerica", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");


                    b.ToTable("RestricaoGenerica");

                    b.HasDiscriminator().HasValue("reg_gen");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoMaterial", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");


                    b.ToTable("RestricaoMaterial");

                    b.HasDiscriminator().HasValue("RestricaoMaterial");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoObrigatoria", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");


                    b.ToTable("RestricaoObrigatoria");

                    b.HasDiscriminator().HasValue("RestricaoObrigatoria");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoOcupacao", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");

                    b.Property<float>("AlturaMax");

                    b.Property<float>("AlturaMin");

                    b.Property<float>("ComprimentoMax");

                    b.Property<float>("ComprimentoMin");

                    b.Property<float>("LarguraMax");

                    b.Property<float>("LarguraMin");

                    b.ToTable("RestricaoOcupacao");

                    b.HasDiscriminator().HasValue("RestricaoOcupacao");
                });

            modelBuilder.Entity("Closify.Models.Restricoes.RestricaoOpcional", b =>
                {
                    b.HasBaseType("Closify.Models.Restricoes.Restricao");


                    b.ToTable("RestricaoOpcional");

                    b.HasDiscriminator().HasValue("RestricaoOpcional");
                });

            modelBuilder.Entity("Closify.Models.Agregacao", b =>
                {
                    b.HasOne("Closify.Models.Produto", "Base")
                        .WithMany()
                        .HasForeignKey("BaseID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Closify.Models.Produto", "Parte")
                        .WithMany()
                        .HasForeignKey("ParteID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Closify.Models.Categoria", b =>
                {
                    b.HasOne("Closify.Models.Categoria")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaID1");

                    b.HasOne("Closify.Models.Categoria", "SuperCategoria")
                        .WithMany()
                        .HasForeignKey("SuperCatID");
                });

            modelBuilder.Entity("Closify.Models.MaterialAcabamento", b =>
                {
                    b.HasOne("Closify.Models.Acabamento", "Acabamento")
                        .WithMany()
                        .HasForeignKey("AcabamentoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Closify.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Closify.Models.Produto", b =>
                {
                    b.HasOne("Closify.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Closify.Models.Dimensao", "Dimensao")
                        .WithOne()
                        .HasForeignKey("Closify.Models.Produto", "DimensaoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Closify.Models.ProdutoMaterialAcabamento", b =>
                {
                    b.HasOne("Closify.Models.MaterialAcabamento", "MaterialAcabamento")
                        .WithMany()
                        .HasForeignKey("MaterialAcabamentoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Closify.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Closify.Models.Produto")
                        .WithMany("ProdutoMaterialAcabamentos")
                        .HasForeignKey("ProdutoID1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Closify.Models.Restricoes.Restricao", b =>
                {
                    b.HasOne("Closify.Models.Agregacao")
                        .WithMany("Restricoes")
                        .HasForeignKey("AgregacaoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Closify.Models.ValorNumerico", b =>
                {
                    b.HasOne("Closify.Models.Dimensao")
                        .WithMany("Altura")
                        .HasForeignKey("DimensaoID");

                    b.HasOne("Closify.Models.Dimensao")
                        .WithMany("Comprimento")
                        .HasForeignKey("DimensaoID1");

                    b.HasOne("Closify.Models.Dimensao")
                        .WithMany("Largura")
                        .HasForeignKey("DimensaoID2");
                });
#pragma warning restore 612, 618
        }
    }
}
