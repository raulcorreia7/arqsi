const ItemRepository = require('../repository/itemdeproduto.repository');
const EncomendaRepository = require('../repository/encomenda.repository');
const Encomenda = require('../models/encomenda.model');
const CatalogoService = require('../services/catalogo.service');
const EncomendaValidarDTO = require('../dto/EncomendaValidarDTO');
class EncomendaService {

    /**
     * Criar uma encomenda,
     * Considera-se que a encomenda é final e não alterável
     */
    async criarEncomenda(encomenda) {
        return new Promise(
                function (resolve, reject) {
                    
                    /*if (encomenda.Cliente == null || encomenda.Items == null ||
                        encomenda.Items.length == 0)
                        reject("400");
                    */
                   if(encomenda.Cliente == null) {
                       reject("400");
                   }
                    var encomenda_items = (encomenda.Items != null) ? encomenda.Items : [];
                    var promises = [];
                    //Ir buscar todas as partes
                    encomenda_items.forEach(element => {
                        promises.push(new Promise((resolve, reject) => {
                            const _repo_item = new ItemRepository();
                            _repo_item.findById(element, function (err, item) {
                                if (err) reject(err);
                                else {
                                    resolve(item);
                                }

                            })
                        }));
                    });
                    //Depois de ter ido buscar todas as partes à BD, fazer lógica
                    Promise.all(promises)
                        .then(function (values) {
                            const _repo_encomenda = new EncomendaRepository();
                            const encomenda_to_save = new Encomenda({
                                cliente: encomenda.Cliente,
                                items: values
                            });
                            _repo_encomenda.save(encomenda_to_save, mayHaveError => {
                                if (mayHaveError) reject(mayHaveError)
                                else
                                    resolve(encomenda_to_save);
                            });
                        })
                        .catch((reason) => {
                            reject(reason)
                        });
                })
            .catch((reason) => reject(reason));
    }

    async validarEncomenda(encomenda) {
        return new Promise((resolve, reject) => {
            const _servico = new CatalogoService();
            const dtovalidar = new EncomendaValidarDTO(encomenda);
            _servico.ValidarEncomenda(dtovalidar)
                .then((value) => {
                    resolve(encomenda);
                })
                .catch((err) => {
                    reject(err);
                })
        })
    }

    async adicionarItem(encomenda, item) {
        const repo = new EncomendaRepository();

        return new Promise(async (resolve, reject) => {
            if (encomenda == null || item == null) reject("404");
            if (encomenda.em_construcao == 0) reject("400");

            var itemFound = encomenda.items.find(function (element) {
                return element.id == item.id;
            })
            if (!itemFound == null) {
                reject("400");
            }

            encomenda.items.push(item);

            repo.update(encomenda, (err, saved) => {
                if (err) reject(err);
                else
                    resolve(saved);
            });
        });
    }

    async removerItem(encomenda, item) {
        const repo = new EncomendaRepository();
        return new Promise(async (resolve, reject) => {
            if (encomenda == null || item == null) reject("404");
            if (encomenda.em_construcao == 0) reject("400");

            var itemFound = encomenda.items.find(function (element) {
                return element.id == item.id;
            });
            if (itemFound == null) {
                reject("400");
            }

            var Items = encomenda.items.filter(
                element => element.id != item.id
            );

            encomenda.items = Items;

            repo.update(encomenda, (err, savedEncomenda) => {
                if (err) reject(err);
                else
                    resolve(savedEncomenda);
            });
        });
    }
}
module.exports = EncomendaService;