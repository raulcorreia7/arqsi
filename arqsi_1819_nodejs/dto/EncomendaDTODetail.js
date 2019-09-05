const ItemDeProdutoDTO = require('./ItemDeProdutoDTO');
class EncomendaDTODetail {

    /**
     * Construtor que constrói uma encomenda com detalhe
     * @param {Encomenda} encomenda 
     */
    constructor(encomenda) {
        this.id = Number(encomenda.id);
        this.Cliente = encomenda.cliente;
        this.em_construcao = encomenda.em_construcao;
        this.items = [];
        if (encomenda.items != null) {
            encomenda.items.forEach(element => {
                this.items.push(new ItemDeProdutoDTO(element));
            });
        }

    }
}

module.exports = EncomendaDTODetail;