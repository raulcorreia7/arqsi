const mongoose = require('mongoose');
const autoIncrement = require('mongoose-plugin-autoinc')
const Schema = mongoose.Schema;
const ItemRepository = require('../repository/itemdeproduto.repository');
/*

*/

var ItemDeProdutoSchema = new Schema({
    nome: String,
    material: String,
    acabamento: String,
    dimensao: {
        altura: Number,
        comprimento: Number,
        largura: Number,
    },
    partes: [{
        type: Number,
        ref: 'ItemDeProduto'
    }],
    produto_id: Number,
    em_construcao: {
        type: Number,
        default: 1
    }
});

/**
 * Atualizar o Item a partir de um dto
 */
ItemDeProdutoSchema.methods.FromDTO = function FromDTO(dto) {
    if (this.em_construcao == 1) {
        this.produto_id = dto.Produto_id;
        this.nome = dto.Nome;
        this.material = dto.Material;
        this.acabamento = dto.Acabamento;
        this.dimensao.altura = Number(dto.Dimensao.Altura);
        this.dimensao.comprimento = Number(dto.Dimensao.Comprimento);
        this.dimensao.largura = Number(dto.Dimensao.Largura);
    } else {
        throw Error("O item já não se encontra em construção.");
    }
}
/**
 * Atualizar o Item a partir de um JSON
 */
ItemDeProdutoSchema.methods.fromJSON = function FromJSON(itemDeProdutoJSON) {
    if (itemDeProdutoJSON.Material == null ||
        itemDeProdutoJSON.Acabamento == null ||
        itemDeProdutoJSON.Dimensao == null
    ) throw Error("json inválido");
    if (this.em_construcao == 0) throw Error("O item já não se encontra em construção.");
    //this.produto_id = itemDeProdutoJSON.Produto_id;
    //this.nome = itemDeProdutoJSON.Nome;
    this.material = itemDeProdutoJSON.Material;
    this.acabamento = itemDeProdutoJSON.Acabamento;
    this.dimensao = {
        altura: itemDeProdutoJSON.Dimensao.Altura,
        comprimento: itemDeProdutoJSON.Dimensao.Comprimento,
        largura: itemDeProdutoJSON.Dimensao.Largura,
    };
}

ItemDeProdutoSchema.methods.finalizar = function () {
    finalizarR(this);
}
/**
 * 
 * @param {ItemDeProduto} ItemDeProduto 
 */
function finalizarR(item) {
    const _repo = new ItemRepository();
    item.em_construcao = 0;
    _repo.update(item, (err) => {});
    if (item.partes != null) {
        item.partes.forEach(element => {
            finalizarR(element);
        });
    }

}
ItemDeProdutoSchema.methods.isParte = function (itemToCheck) {
    var valid = false;
    if (this._id == itemToCheck._id) return true;
    else {
        if (this.partes != null) {
            this.partes.forEach(element => {
                valid &= element.isParte(itemToCheck);
            });
        }
    }
    return valid;
}
ItemDeProdutoSchema.statics.construirItemDeProdutoDeJSON = function create(itemDeProdutoJSON) {
    var produto = null;
    var ItemDeProduto = mongoose.model('ItemDeProduto', ItemDeProdutoSchema);
    try {
        produto = new ItemDeProduto({
            produto_id: itemDeProdutoJSON.Produto_id,
            nome: itemDeProdutoJSON.Nome,
            material: itemDeProdutoJSON.Material,
            acabamento: itemDeProdutoJSON.Acabamento,
            dimensao: {
                altura: itemDeProdutoJSON.Dimensao.Altura,
                comprimento: itemDeProdutoJSON.Dimensao.Comprimento,
                largura: itemDeProdutoJSON.Dimensao.Largura,
            },
            partes: []
        });
    } catch (e) {
        return null;
    }
    return produto;
}
ItemDeProdutoSchema.statics.construirItemDeDTO = function construirItem(validacaoDTO) {
    var produto = null;
    var ItemDeProduto = mongoose.model('ItemDeProduto', ItemDeProdutoSchema);
    try {
        produto = new ItemDeProduto({
            produto_id: validacaoDTO.Produto_id,
            nome: validacaoDTO.Nome,
            material: validacaoDTO.Material,
            acabamento: validacaoDTO.Acabamento,
            dimensao: {
                altura: validacaoDTO.Dimensao.Altura[0],
                comprimento: validacaoDTO.Dimensao.Comprimento[0],
                largura: validacaoDTO.Dimensao.Largura[0],
            },
            partes: []
        });

    } catch (e) {
        return null;
    }
    return produto;
}

var autoPopulateChildren = function (next) {
    this.populate('partes');
    next();
}
ItemDeProdutoSchema
    .pre('findOne', autoPopulateChildren)
    .pre('find', autoPopulateChildren);

ItemDeProdutoSchema.plugin(autoIncrement.autoIncrement, {
    model: 'ItemDeProduto',
    startAt: 1
});


module.exports = mongoose.model('ItemDeProduto', ItemDeProdutoSchema);