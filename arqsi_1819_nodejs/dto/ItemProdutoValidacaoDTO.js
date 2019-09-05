class ItemProdutoValidacaoDTO {
    constructor(itemJSON) {
        if (itemJSON.Produto_id == null ||
            itemJSON.Material == null ||
            itemJSON.Acabamento == null ||
            itemJSON.Dimensao == null)
            return null;

        else {
            this.Produto_id = itemJSON.Produto_id;
            this.Nome = itemJSON.Nome;
            this.Material = itemJSON.Material;
            this.Acabamento = itemJSON.Acabamento;
            this.Dimensao = {
                TipoComprimento: "discreto",
                TipoLargura: "discreto",
                TipoAltura: "discreto",
                Comprimento: [itemJSON.Dimensao.Comprimento],
                Largura: [itemJSON.Dimensao.Largura],
                Altura: [itemJSON.Dimensao.Altura]
            }
            this.Partes = [];
            /*
            Não construir as Partes Aqui,
            Constrói fora...
            Raúl
            if (itemJSON.Partes != null) {
                itemJSON.Partes.forEach(element => {
                    this.Partes.push(new ItemProdutoValidacaoDTO(element));
                });

            }
            */
        }
    }
}



module.exports = ItemProdutoValidacaoDTO;