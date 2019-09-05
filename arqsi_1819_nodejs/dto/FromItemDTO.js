class FromItemDTO {

    constructor(item) {
        this.Produto_id = item.produto_id;
        //this.Nome = item.Nome;
        this.Material = item.material;
        this.Acabamento = item.acabamento;
        this.Dimensao = {
            TipoComprimento: "discreto",
            TipoLargura: "discreto",
            TipoAltura: "discreto",
            Comprimento: [item.dimensao.comprimento],
            Largura: [item.dimensao.largura],
            Altura: [item.dimensao.altura]
        }
        this.Partes = [];
        if (item.partes != null) {
            item.partes.forEach(element => {
                this.Partes.push(new FromItemDTO(element));
            })
        }
    }
}
module.exports = FromItemDTO;