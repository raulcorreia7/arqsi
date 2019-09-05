using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Closify.Models;
using Closify.Models.DTO;
using System.Collections.Generic;
using Closify.Models.Restricoes;

namespace Closify.Data
{

    public class Bootstrap
    {

        public static void Initialize(ClosifyContext context)
        {

            if (context.Categorias.Any())
            {
                return;
            }

            Categoria[] categorias = {
                new Categoria{
                    Nome = "Armário"
                },
                new Categoria{
                    Nome = "Módulo Gavetas"
                },
                new Categoria{
                    Nome = "Gavetas"
                },
                new Categoria{
                    Nome = "Portas"
                },
                new Categoria{
                    Nome = "Cabide"
                },
                new Categoria
                {
                    Nome = "Espelhos"
                }
            };

            context.Categorias.AddRange(categorias);
            context.SaveChanges();
            Categoria[] subCategorias = {
                new Categoria { Nome ="Armário de Sala",
                SuperCategoria = categorias[0],
                SuperCatID = categorias[0].CategoriaID},
                new Categoria { Nome ="Armário de Jardim",
                SuperCategoria= categorias[0],
                SuperCatID = categorias[0].CategoriaID}
        };
            categorias[0].AdicionarSubCategoria(subCategorias[0]);
            categorias[0].AdicionarSubCategoria(subCategorias[1]);
            //context.Categorias.AddRange(subCategorias);
            context.SaveChanges();


            Acabamento[] acabamentos = {
                new Acabamento("Natural")
                ,
                new Acabamento("Polido")
                ,
                new Acabamento("Envernizado")
                ,
                new Acabamento("Anodizado")
            };

            Material[] materiais = {
                new Material("Madeira"),
                new Material("Aluminio"),
                new Material("MDF"),
            };

            context.Materiais.AddRange(materiais);

            context.SaveChanges();

            context.Acabamentos.AddRange(acabamentos);

            context.SaveChanges();


            MaterialAcabamento[] madeiraAcabamentos = {
                new MaterialAcabamento(materiais[0],acabamentos[0]),//Madeira Natural
                new MaterialAcabamento(materiais[0],acabamentos[1]),//Madeira Polido
                new MaterialAcabamento(materiais[0],acabamentos[2])//Madeira Envernizado
            };

            MaterialAcabamento[] aluminioAcabamentos = {
                new MaterialAcabamento(materiais[1],acabamentos[3])//Aluminio Anodizado
            };

            MaterialAcabamento[] mdfAcabamentos = {
                new MaterialAcabamento(materiais[2],acabamentos[0]),//MDF Natural
                new MaterialAcabamento(materiais[2],acabamentos[1]),//MDF Polido
                new MaterialAcabamento(materiais[2],acabamentos[2])//MDF Envernizado
            };

            DimensaoDTO dimensaodto = new DimensaoDTO
            {

                TipoAltura = "discreto",
                TipoComprimento = "discreto",
                TipoLargura = "continuo",
                Altura = new List<double> { 100, 200 },
                Comprimento = new List<double> { 20, 30, 40 },
                Largura = new List<double> { 10, 20 }

            };
            Dimensao d = new Dimensao(dimensaodto);

            //Armario
            Produto armario1 = new Produto
            {
                Nome = "Armário Xpto",
                Categoria = subCategorias[0],
                Dimensao = d,
            };
            foreach (MaterialAcabamento ma in madeiraAcabamentos) armario1.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in aluminioAcabamentos) armario1.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) armario1.AddMaterialAcabamento(ma);
            context.Produtos.Add(armario1);
            context.SaveChanges();

            //Criar Porta
            DimensaoDTO dporta = new DimensaoDTO
            {
                TipoAltura = "continuo",
                TipoComprimento = "continuo",
                TipoLargura = "continuo",
                Altura = new List<double> { 1, 200 },
                Comprimento = new List<double> { 1, 400 },
                Largura = new List<double> { 1, 100 }
            };

            Produto porta = new Produto
            {
                Nome = "Porta 1",
                Categoria = categorias[3],
                Dimensao = new Dimensao(dporta)
            };
            context.Produtos.Add(porta);
            context.SaveChanges();
            foreach (MaterialAcabamento ma in madeiraAcabamentos) porta.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) porta.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in aluminioAcabamentos) porta.AddMaterialAcabamento(ma);
            context.SaveChanges();

            Agregacao a2 = new Agregacao(armario1, porta);
            a2.Restricoes.Add(RestricaoFactory.RestricaoObrigatoria());
            a2.Restricoes.Add(RestricaoFactory.RestricaoMaterial());
            if (a2.Validar())
            {
                context.Agregacoes.Add(a2);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Erro ao fazer a agregação");
            }

            DimensaoDTO dmodulogavetas = new DimensaoDTO
            {
                TipoAltura = "continuo",
                TipoComprimento = "continuo",
                TipoLargura = "continuo",
                Altura = new List<double> { 10, 100 },
                Comprimento = new List<double> { 20, 100 },
                Largura = new List<double> { 5, 40 }
            };

            Produto moduloGavetas = new Produto
            {
                Nome = "Modulo Gavetas 1",
                Categoria = categorias[1],
                Dimensao = new Dimensao(dmodulogavetas)
            };
            context.Produtos.Add(moduloGavetas);
            foreach (MaterialAcabamento ma in madeiraAcabamentos) moduloGavetas.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) moduloGavetas.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in aluminioAcabamentos) moduloGavetas.AddMaterialAcabamento(ma);
            context.SaveChanges();

            Agregacao armario1_modulogavetas = new Agregacao(armario1,moduloGavetas);
            context.Agregacoes.Add(armario1_modulogavetas);
            context.SaveChanges();

            DimensaoDTO dto_gaveta = new DimensaoDTO
            {
                TipoAltura = "discreto",
                TipoComprimento = "discreto",
                TipoLargura = "discreto",
                Altura = new List<double> { 10, 20 },
                Comprimento = new List<double> { 20, 30, 40 },
                Largura = new List<double> { 5 }
            };
            Dimensao d_gaveta = new Dimensao(dto_gaveta);

            //Gaveta
            Produto gaveta = new Produto
            {
                Nome = "Gaveta 1",
                Categoria = categorias[1],
                Dimensao = d_gaveta,
            };
            foreach (MaterialAcabamento ma in madeiraAcabamentos) gaveta.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) gaveta.AddMaterialAcabamento(ma);

            context.Produtos.Add(gaveta);
            context.SaveChanges();

            Agregacao a1 = new Agregacao(moduloGavetas, gaveta);
            a1.Restricoes.Add(RestricaoFactory.RestricaoObrigatoria());
            context.Agregacoes.Add(a1);

            DimensaoDTO dtodim_gaveta2 = new DimensaoDTO
            {
                TipoAltura = "discreto",
                TipoComprimento = "discreto",
                TipoLargura = "discreto",
                Altura = new List<double> { 10, 20, 30 },
                Comprimento = new List<double> { 20, 30, 40 },
                Largura = new List<double> { 5, 10, 15, 20 }
            };

            Produto gaveta2 = new Produto
            {
                Nome = "Gaveta 2",
                Categoria = categorias[1],
                Dimensao = new Dimensao(dtodim_gaveta2)
            };
            foreach (MaterialAcabamento ma in madeiraAcabamentos) gaveta2.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) gaveta2.AddMaterialAcabamento(ma);

            context.Produtos.Add(gaveta2);
            context.SaveChanges();

            Agregacao a3 = new Agregacao(moduloGavetas, gaveta2);
            a3.Restricoes.Add(RestricaoFactory.RestricaoOpcional());
            if (a3.Validar())
            {
                context.Agregacoes.Add(a3);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Erro ao fazer a agregação a3");
            }
            context.SaveChanges();

            DimensaoDTO dcabide = new DimensaoDTO
            {
                TipoAltura = "continuo",
                TipoComprimento = "continuo",
                TipoLargura = "continuo",
                Altura = new List<double> { 1, 200 },
                Comprimento = new List<double> { 1, 400 },
                Largura = new List<double> { 1, 100 }

            };
            Produto cabide = new Produto
            {
                Nome = "Cabide 1",
                Categoria = categorias[4],
                Dimensao = new Dimensao(dcabide)
            };

            foreach (MaterialAcabamento ma in madeiraAcabamentos) cabide.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in mdfAcabamentos) cabide.AddMaterialAcabamento(ma);
            foreach (MaterialAcabamento ma in aluminioAcabamentos) cabide.AddMaterialAcabamento(ma);

            context.Produtos.Add(cabide);
            context.SaveChanges();

            DimensaoDTO darmario2 = new DimensaoDTO
            {
                TipoAltura = "continuo",
                TipoComprimento = "discreto",
                TipoLargura = "discreto",
                Altura = new List<double> { 50, 200 },
                Comprimento = new List<double> { 20, 30, 40 },
                Largura = new List<double> { 10, 20, 30, 40 }
            };

            Produto armario2 = new Produto
            {
                Nome = "Armario 2",
                Categoria = categorias[1],
                Dimensao = new Dimensao(darmario2)
            };

            context.Produtos.Add(armario2);
            context.SaveChanges();
            foreach (MaterialAcabamento ma in mdfAcabamentos) armario2.AddMaterialAcabamento(ma);


            Agregacao armario2_modulogavetas = new Agregacao(armario2, moduloGavetas);
            armario2_modulogavetas.Restricoes.Add(RestricaoFactory.RestricaoOpcional());
            if (armario2_modulogavetas.Validar())
            {
                context.Agregacoes.Add(armario2_modulogavetas);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Erro ao fazer a agregação armario2_modulogavetas");
            }
            Agregacao armario2_cabide = new Agregacao(armario2, cabide);
            armario2_cabide.Restricoes.Add(RestricaoFactory.RestricaoObrigatoria());

            if (armario2_cabide.Validar())
            {
                context.Agregacoes.Add(armario2_cabide);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Erro ao fazer a agregação armario2_cabide");
            }

            Agregacao armario2_porta = new Agregacao(armario2, porta);
            armario2_porta.Restricoes.Add(RestricaoFactory.RestricaoMaterial());

            if (armario2_porta.Validar())
            {
                context.Agregacoes.Add(armario2_porta);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Erro ao fazer a agregação armario2_porta");
            }

            DimensaoDTO dimEspelho = new DimensaoDTO
            {
                TipoAltura = "continuo",
                TipoComprimento = "continuo",
                TipoLargura = "discreto",
                Altura = new List<double> { 20, 120 },
                Comprimento = new List<double> { 10, 20 },
                Largura = new List<double> { 8 }
            };

            Produto espelho1 = new Produto
            {
                Nome = "Espelho",
                Categoria = categorias[5],
                Dimensao = new Dimensao(dimEspelho)
            };

            foreach (MaterialAcabamento ma in aluminioAcabamentos) espelho1.AddMaterialAcabamento(ma);
            context.Produtos.Add(espelho1);
            context.SaveChanges();

            Agregacao armario_espelho = new Agregacao(armario1, espelho1);
            RestricaoOcupacaoDTO restOcupacao = new RestricaoOcupacaoDTO
            {
                AlturaMin = 0,
                AlturaMax = 0.50f/*,
                ComprimentoMin = 0,
                ComprimentoMax = 0.50f,
                LarguraMin = 0,
                LarguraMax = 0.50f */               
            };

            armario_espelho.Restricoes.Add(RestricaoFactory.RestricaoOcupacao(restOcupacao));
            if(armario_espelho.Validar())
            {   
                context.Agregacoes.Add(armario_espelho);
                context.SaveChanges();
            } else
            {
                throw new ArgumentException("Erro ao fazer a agregação armario_espelho");
            }
        }
    }
}