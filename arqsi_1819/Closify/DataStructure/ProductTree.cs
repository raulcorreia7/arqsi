
using System.Collections.Generic;
using Closify.Models;
using Closify.Models.Servico;

namespace Closify.DataStructure
{
    public class Node
    {
        public Produto node = null;

        public List<Node> children = new List<Node>();

        public Node(Produto p)
        {
            this.node = p;
        }

        public async void addChildren(Produto produto, IProdutoServico servico)
        {
            //FIXME, adicionar paenas os filhos,
            //EStá mal
            if (node.ProdutoID == produto.ProdutoID)
            {

                var produtos = await servico
                    .ProdutoPartes(produto.ProdutoID);

                produtos
                    .ForEach(e => children.Add(new Node(e)));
            }
            else
            {
                foreach (Node n in children)
                {
                    n.addChildren(produto, servico);
                }
            }
        }

        public bool Contains(Produto produto)
        {
            bool contains = false;
            if (this.node.ProdutoID == produto.ProdutoID) return true;
            else
            {
                foreach (Node n in children)
                {
                    if (n.Contains(produto))
                    {
                        return true;
                    }
                }
            }
            return contains;
        }
    }
    public class ProductTree
    {

        public Node root;
        public IProdutoServico _servico;
        ProductTree(Produto root, IProdutoServico servico)
        {
            this.root = new Node(root);
            this._servico = servico;
            this.addChild(root);
        }

        void addChild(Produto child)
        {
            this.root.addChildren(child, _servico);
        }

        bool Contains(Produto produto)
        {
            return this.root.Contains(produto);
        }
    }
}