const mongoose = require('mongoose');

class EncomendaRepository {
    constructor() {}

    save(encomenda, cb) {
        console.log('A guardar Encomenda ' + encomenda);
        encomenda.save(function (mayHaveError) {
            cb(mayHaveError);
        });
    }

    findAll() {
        return mongoose.model('Encomenda').find()
            .populate('items')
            .exec();
    }

    findById(id, cb) {
        //console.log('Encontrar Encomenda id: ' + id);
        //mongoose.model('Encomenda').findById(id, cb);
        mongoose.model('Encomenda')
            .findOne({
                _id: id
            })
            .populate('items')
            .exec(cb);
    }

    delete(id, cb) {
        console.log('Apagar Encomenda id: ' + id);
        mongoose.model('Encomenda').findByIdAndDelete(id, cb).exec();
    }

    update(encomenda, cb) {
        console.log('A guardar alteracoes de encomenda id: ' + encomenda.id);
        mongoose.model('Encomenda').findById(encomenda.id, function (err, oldEncomenda) {
            if (err) {
                cb(err);
            } else {
                oldEncomenda = encomenda;
                oldEncomenda.save(cb);
            }
        })
    }
}

module.exports = EncomendaRepository;