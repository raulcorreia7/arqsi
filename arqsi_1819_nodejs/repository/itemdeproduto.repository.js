const mongoose = require('mongoose');

class ItemDeProdutoRepository {
    constructor() {}

    /**
     * Retorna uma promesa com todos os ItemsDeProduto
     */
    findAll() {
        return mongoose.model('ItemDeProduto')
            .find()
            .populate('partes')
            .exec();
    }
    /**
     * Encontra um ItemDeProduto por ID
     * @param {Number} id 
     * @param {(err,item)} cb 
     */
    findById(id, cb) {
        console.log('Encontrar Item de Produto id: ' + id);
        mongoose.model('ItemDeProduto')
            .findOne({
                _id: id
            })
            .populate({
                path: 'partes',
                populate: {
                    path: 'partes'
                }
            })
            .exec(cb);
    }
    /**
     * Encontra um Item Pai de um determinado Item
     * @param {Number} id 
     * @param {(err,item)} cb 
     */
    findParent(id, cb) {
        console.log('Encontrar Item de Produto a cima de id: ' + id);
        mongoose.model('ItemDeProduto')
            .findOne({
                partes: id
            }).exec(cb);
    }
    /**
     * Guarda um item de produto
     * @param {ItemDeProduto} itemproduto 
     * @param {(err,itemsaved)} cb 
     */
    save(itemproduto, cb) {
        console.log('A guardar concretizacao de produto ' + itemproduto);
        itemproduto.save(function (err, oi) {
            if (err) cb(new Error("Invalid Save"));
            else
                cb(err, oi);
        });
    }
    /**
     * Atualiza um ItemDeProduto
     * @param {ItemDeProduto} itemproduto 
     * @param {(err,item)} cb 
     */
    update(itemproduto, cb) {
        console.log('A guardar alteracoes de produto id: ' + itemproduto.id);
        mongoose.model('ItemDeProduto').findById(itemproduto.id, function (err, oldItemProduto) {
            if (err) {
                cb(err);
            } else {
                oldItemProduto = itemproduto;
                oldItemProduto.save(cb);
            }
        })
    }

    delete(id, cb) {
        console.log('Apagar Item de Produto id: ' + id);

        mongoose.model('ItemDeProduto').findByIdAndDelete(id, cb);
    }
    /**
     * 
     * @param {*} id 
     * @param {(err,allitems)} cb 
     */
    findAllNaoPartes(id, cb) {
        var itembase = null;
        var produtos = null;
        var diferenca = [];
        this.findById(id, (err, item) => {
            if (err) cb(err);
            else {
                itembase = item;
                this.findAll().then((allitems) => {
                    produtos = allitems.filter(i => !i.isParte(itembase));
                    cb(err, produtos);
                });
            }
        })
    }
    /**
     * 
     * @param {[]} multipleids 
     */
    findAllByProdutoId(multipleids, cb) {
        var items_arr = [];
        var promises = [];
        multipleids.forEach(id => {
            var p = (mongoose.model('ItemDeProduto')
                .findOne({
                    produto_id: id
                }).exec());
            promises.push(p);
        })

        Promise.all(promises)
            .then((values) => {
                items_arr = values;
                cb(null, items_arr);
            })
            .catch((err) => {
                cb(err);
            });
    }
}

module.exports = ItemDeProdutoRepository;