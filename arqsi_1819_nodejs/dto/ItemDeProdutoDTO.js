class ItemDeProdutoDTO {

    constructor(itemDeProduto) {
        if (itemDeProduto.id != null)
            this.id = parseInt(itemDeProduto.id);
        if (itemDeProduto.nome != null)
            this.Nome = itemDeProduto.nome;
        if (itemDeProduto.produto_id != null)
            this.Produto_id = itemDeProduto.produto_id;
        if (itemDeProduto.em_construcao != null)
            this.Em_construcao = itemDeProduto.em_construcao;
        if (itemDeProduto.material != null)
            this.Material = itemDeProduto.material;
        if (itemDeProduto.acabamento != null)
            this.Acabamento = itemDeProduto.acabamento;
        if (itemDeProduto.dimensao != null) {
            this.Dimensao = {
                Altura: itemDeProduto.dimensao.altura,
                Largura: itemDeProduto.dimensao.largura,
                Comprimento: itemDeProduto.dimensao.comprimento,
            };
        }
        if (itemDeProduto.partes != null) {
            //FIXME:Só buscar IDS
            this.Partes = [];
            itemDeProduto.partes.forEach(element => {
                this.Partes.push(new ItemDeProdutoDTO(element));
            });
        }
    }
}
module.exports = ItemDeProdutoDTO;