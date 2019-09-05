class ItemDeProdutoSimplesDTO {
    /**
     * ItemDeProdutoSimplesDTO
     * Apenas com IDS para mostrar no findall
     * @param {*} itemDeProduto 
     */
    constructor(itemDeProduto) {
        if (itemDeProduto.id != null)
            this.id = parseInt(itemDeProduto.id);
        if (itemDeProduto.produto_id != null)
            this.Produto_id = itemDeProduto.produto_id;
        if (itemDeProduto.partes != null) {
            this.Partes = [];
            itemDeProduto.partes.forEach(element => {
                this.Partes.push(parseInt(element.id));
            });
        }

    }
}

module.exports = ItemDeProdutoSimplesDTO;