using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Closify.Models;
using Closify.Models.DTO;

namespace Closify.Models
{

    public class Produto
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public int CategoriaID { get; set; }
        public int DimensaoID { get; set; }

        [ForeignKey("CategoriaID")]
        public virtual Categoria Categoria { get; set; }
        [ForeignKey("DimensaoID")]
        public virtual Dimensao Dimensao { get; set; }

        //public virtual List<MaterialAcabamento> MateriaisAcabamentos { get; set; } = new List<MaterialAcabamento>();

        public virtual List<ProdutoMaterialAcabamento> ProdutoMaterialAcabamentos { get; set; } = new List<ProdutoMaterialAcabamento>();

        public Produto() { }


        public Produto(string nome, Dimensao dimensao, Categoria categoria, List<MaterialAcabamento> lista)
        {
            Nome = nome;
            Dimensao = dimensao;
            Categoria = categoria;
            AdicionarVariosMateriaisAcabamentos(lista);
        }

        public Produto(ItemDTO dto)
        {
            ProdutoID = dto.Produto_id;
            Nome = "";
            Categoria = null;
            Dimensao = new Dimensao(dto.Dimensao);
            AddMaterialAcabamento(new MaterialAcabamento(new Material(dto.Material), new Acabamento(dto.Acabamento)));
        }


        public void AdicionarVariosMateriaisAcabamentos(List<MaterialAcabamento> lista)
        {
            foreach (MaterialAcabamento mac in lista)
                AddMaterialAcabamento(mac);
        }
        public bool AddMaterialAcabamento(MaterialAcabamento matAcab)
        {
            bool naoRepetido = true;
            foreach (ProdutoMaterialAcabamento pm in ProdutoMaterialAcabamentos)
            {
                if (pm.Igual(matAcab))
                    naoRepetido = false;
            }
            if (naoRepetido)
            {
                ProdutoMaterialAcabamentos.Add(new ProdutoMaterialAcabamento(this, matAcab));
            }
            return naoRepetido;
        }

        public void Update(String novoNome, Categoria novaCategoria, Dimensao novaDimensao, List<MaterialAcabamento> lista)
        {
            Nome = novoNome;
            Categoria = novaCategoria;
            Dimensao = novaDimensao;
            foreach (MaterialAcabamento ma in lista)
            {
                ProdutoMaterialAcabamentos.Add(new ProdutoMaterialAcabamento(this, ma));
            }
        }

        public bool validarMaterialAcabamento(string nomeMaterial, string nomeAcabamento)
        {
            var lista = GetMateriaisAcabamentos();
            foreach (MaterialAcabamento ma in lista)
            {
                if (ma.Material.Nome == nomeMaterial && ma.Acabamento.Nome == nomeAcabamento)
                    return true;
            }
            return false;

        }

        public List<MaterialAcabamento> GetMateriaisAcabamentos()
        {
            List<MaterialAcabamento> lista = new List<MaterialAcabamento>();
            foreach (ProdutoMaterialAcabamento pma in ProdutoMaterialAcabamentos)
                lista.Add(pma.MaterialAcabamento);
            return lista;
        }

    }
}