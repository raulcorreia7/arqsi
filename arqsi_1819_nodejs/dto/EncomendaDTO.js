class EncomendaDTO {
    /**
    Constroi um EncomendaDTO de uma Encomenda
    @constructor
    @params {Encomenda} encomenda
    **/
    constructor(encomenda) {
        this.id = Number(encomenda.id);
        this.cliente = encomenda.cliente;
        this.items = [];
        encomenda.items.forEach(element => {
            this.items.push(element);
        });
    }
}
module.exports = EncomendaDTO;