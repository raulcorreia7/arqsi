const FromItemDTO = require('./FromItemDTO');
class EncomendaValidarDTO {
    constructor(encomenda) {
        this.items = [];
        encomenda.items.forEach(element => {
            this.items.push(new FromItemDTO(element));
        });
    }
}

module.exports = EncomendaValidarDTO;