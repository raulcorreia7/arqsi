const mongoose = require('mongoose');
const autoIncrement = require('mongoose-plugin-autoinc')
const Schema = mongoose.Schema;

let EncomendaSchema = new Schema({
    cliente: {
        type: String
    },
    items: [{
        type: Schema.Types.ObjectId,
        ref: 'ItemDeProduto'
    }],
    em_construcao: {
        type: Number,
        default: 1
    }
});

EncomendaSchema.methods.finalizar = function finalizar() {
    this.em_construcao = 0;
    if (this.items != null) {
        for (let index = 0; index < this.items.length; index++) {
            this.items[index].finalizar();

        }
    }
}
EncomendaSchema.plugin(autoIncrement.autoIncrement, {
    model: 'Encomenda',
    startAt: 1
});
module.exports = mongoose.model('Encomenda', EncomendaSchema);