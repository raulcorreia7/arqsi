namespace Closify.Models.Servico
{
    using Closify.Models.DTO;
    using Closify.Models.Restricoes;
    using Closify.Repository.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepository _produtorepo;

        private readonly IMaterialAcabamentoRepository _materialacabamentorepo;

        private readonly ICategoriaRepository _categoriarepo;

        private readonly IAgregacaoRepository _agregacaorepo;
        private readonly IMaterialRepository _repomaterial;
        private readonly IAcabamentoRepository _repoacabamento;
        public ProdutoServico(ICategoriaRepository categoriarepo, IProdutoRepository produtrepo,
         IMaterialAcabamentoRepository materialcabamentorepo, IAgregacaoRepository agregacaorepo,
         IMaterialRepository materialrepo, IAcabamentoRepository acabamentorepo)
        {
            _categoriarepo = categoriarepo;
            _produtorepo = produtrepo;
            _materialacabamentorepo = materialcabamentorepo;
            _agregacaorepo = agregacaorepo;
            _repomaterial = materialrepo;
            _repoacabamento = acabamentorepo;
        }

        public Produto ConstruirProdutoDeDTO(ProdutoDTO produtodto)
        {
            Dimensao dimensao = new Dimensao(produtodto.DimensaoDTO);
            Categoria categoria = _categoriarepo.GetById(produtodto.Categoria.ID).Result;
            if (categoria == null) throw new ArgumentException("Categoria inválida.");

            //Material mat = _materialrepo.GetById(produtodto.MaterialDTO.MaterialID).Result
            //if (mat == null) throw new ArgumentException("Material inválido.");

            var materiaisacabamento = ConstruirMaterialAcabamentoDeDTO(produtodto).Result;
            Produto p = new Produto(
            produtodto.Nome, dimensao, categoria, materiaisacabamento);


            //Depreciado, Raúl
            //AgregarProdutos(p, produtodto.Partes);
            return p;
        }

        private async Task<List<MaterialAcabamento>> ConstruirMaterialAcabamentoDeDTO(ProdutoDTO produto)
        {
            List<MaterialAcabamento> lista = new List<MaterialAcabamento>();
            List<MaterialAcabamentoDTO> listamacdto = produto.MaterialAcabamentoDTO;
            foreach (MaterialAcabamentoDTO madto in listamacdto)
            {
                //if (!madto.Material.MaterialID.HasValue) continue;
                //if (!madto.Acabamento.AcabamentoID.HasValue) continue;
                //Material mat = await _repomaterial.GetById(madto.Material.MaterialID.Value);
                //Acabamento acab = await _repoacabamento.GetById(madto.Acabamento.AcabamentoID.Value);
                //if (mat == null || acab == null) continue;
                //lista.Add(new MaterialAcabamento(mat, acab));
                if (madto.id.HasValue)
                {
                    MaterialAcabamento matacab = await _materialacabamentorepo.GetById(madto.id.Value);
                    if (matacab != null) lista.Add(matacab);
                }


            }

            return lista;
        }

        public async Task<Produto> Create(ProdutoDTO p)
        {
            Produto produto = ConstruirProdutoDeDTO(p);
            await _produtorepo.Create(produto);
            /*
                Raul
                Depreciado,
                Agregacoes vão ser criadas individualmente
             */

            /*             if (p.Partes != null)
                        {
                            await AgregarProdutos(produto, p.Partes);
                        } */

            return produto;
        }

        public async Task<Produto> Update(int id, ProdutoDTO produtodto)
        {
            Produto produto = _produtorepo.GetById(id).Result;

            Dimensao dimensao = new Dimensao(produtodto.DimensaoDTO);
            var lista_matacab = await ConstruirMaterialAcabamentoDeDTO(produtodto);
            Categoria categoria = _categoriarepo.GetById(produtodto.Categoria.ID).Result;

            if (lista_matacab.Count == 0) throw new ArgumentException("Tem que existir materiais");
            if (categoria == null) throw new ArgumentException("Categoria inválida.");

            produto.Update(produtodto.Nome, categoria, dimensao, lista_matacab);
            //await DeleteAgregacoes(id); Depreciado
            await _produtorepo.Update(id, produto);
            //wait AgregarProdutos(produto, produtodto.Partes); Depreciado

            return produto;
        }

        public async Task AgregarProdutos(Produto p, List<int> IDPartes)
        {
            foreach (int i in IDPartes)
            {
                Produto pParte = _produtorepo.GetById(i).Result;
                if (pParte != null)
                {

                    Agregacao a = new Agregacao(p, pParte);
                    if (a.Validar())
                        await _agregacaorepo.Create(a);
                }
            }
        }

        public async Task DeleteAgregacoes(int id)
        {
            foreach (Agregacao a in _agregacaorepo.GetAllAgregacoesByProdutoBaseID(id).Result)
            {
                await _agregacaorepo.Delete(a.AgregacaoID);
            }

            foreach (Agregacao a in _agregacaorepo.GetAllAgregacoesByProdutoParteID(id).Result)
            {
                await _agregacaorepo.Delete(a.AgregacaoID);
            }
        }

        public List<int> ListaPartesProduto(int id)
        {
            List<int> partesProduto = new List<int>();

            foreach (Produto p in _agregacaorepo.GetAllPartes(id).Result)
            {
                partesProduto.Add(p.ProdutoID);
            }
            return partesProduto;
        }

        public async Task<List<Produto>> ProdutoPartes(int id)
        {
            return await _agregacaorepo.GetAllPartes(id);
        }

        public async Task<List<Produto>> ProdutoPartesEm(int id)
        {
            return await _agregacaorepo.GetAllPartesEm(id);
        }

        public async Task<List<Restricao>> RestricaoProduto(int id)
        {
            return await _agregacaorepo.GetAllRestricoesByProdutoBaseID(id);
        }
        public async Task<bool> AdicionarMaterialAcabamento(int produtoid, int matAcabid)
        {
            bool added = true;

            var produto = await _produtorepo.GetById(produtoid);
            var materialAcabamento = await _materialacabamentorepo.GetById(matAcabid);

            if (produto == null && materialAcabamento == null) return false;

            produto.AddMaterialAcabamento(materialAcabamento);
            await _produtorepo.SaveChangesAsync();
            return added;

        }

        public async Task<List<Produto>> ProdutosSemLigacao(int id)
        {
            var taskproduto = _produtorepo.GetById(id);
            var taskallproducts = _produtorepo.GetAll();
            var taskallpartes = _agregacaorepo.GetAllPartes(id);

            await Task.WhenAll(taskproduto,taskallproducts,taskallpartes);

            var produto = taskproduto.Result;
            var allproducts = taskallproducts.Result;
            var allpartes = taskallpartes.Result;
            allproducts.Remove(produto);
            var produtosNonIntersect = allproducts.Except(allpartes);
            return produtosNonIntersect.ToList();
            //Verificar recursivamente
        }
    }
}
