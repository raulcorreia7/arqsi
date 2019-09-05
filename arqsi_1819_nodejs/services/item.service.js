const CatalogoService = require('./catalogo.service');
const ItemDeProduto = require('../models/itemdeproduto.model');
const ItemRepository = require('../repository/itemdeproduto.repository');
const ItemProdutoValidacaoDTO = require('../dto/ItemProdutoValidacaoDTO');
const FromItemDTO = require('../dto/FromItemDTO');
class ItemService {

    constructor() {}

    async criarItemDeProduto(itemDeProdutoJSON) {
        return new Promise(async (resolve, reject) => {
            const servicoCatalogo = new CatalogoService();
            const repo = new ItemRepository();
            var itemvalidacaodto = await construirDTOValidacao(itemDeProdutoJSON);
            if (itemvalidacaodto == null) reject("Formato inválido.");
            else {
                var valido = false;
                try {
                    valido = await servicoCatalogo.ValidarItemDeProduto(itemvalidacaodto);
                    if (!valido) reject("Produto Inválido");
                    var produto = await servicoCatalogo.getProduto(itemDeProdutoJSON.Produto_id);
                    var itemdeproduto = await ConstruirItemComNomes(itemvalidacaodto, itemDeProdutoJSON, produto);
                    repo.save(itemdeproduto, (err, savedItem) => {
                        if (err) reject(err);
                        else
                            resolve(savedItem)
                    });
                } catch (e) {
                    reject(e);
                }
            }
        });
    }
    async atualizarItemDeProduto(itemID, itemDeProdutoJSON, itemdeproduto) {
        const repo = new ItemRepository();
        return new Promise(async (resolve, reject) => {
            if (itemdeproduto == null) reject("404");
            if (itemdeproduto.em_construcao === 0) reject("O item já não se encontra em construção.");
            else {
                repo.findParent(itemID, async (err, produtoPai) => {
                    if (err) reject(err);
                    else {
                        const servicoCatalogo = new CatalogoService();
                        itemdeproduto.fromJSON(itemDeProdutoJSON);
                        var oQueMandar = itemdeproduto;
                        if (produtoPai != null) {
                            oQueMandar = produtoPai;
                            for (let i = 0; i < produtoPai.partes.length; i++) {
                                if (produtoPai.partes[i].produto_id == itemdeproduto.produto_id) {
                                    produtoPai.partes[i] = itemdeproduto;
                                    break;
                                }
                            }
                        }
                        if (oQueMandar == null) reject("Formato inválido.");
                        else {
                            var valido = false;
                            try {
                                var dtoValidar = new FromItemDTO(oQueMandar);
                                valido = await servicoCatalogo.ValidarItemDeProduto(dtoValidar);
                                if (!valido) reject("Produto Inválido");
                                repo.update(itemdeproduto, (err, savedItem) => {
                                    if (err) reject(err);
                                    else
                                        resolve(savedItem)
                                });
                            } catch (e) {
                                reject(e);
                            }
                        }
                    }
                })
            }
        });
    }

    async adicionarParteItemDeProduto(itemdeproduto, itemparte) {
        const repo = new ItemRepository();
        return new Promise(async (resolve, reject) => {
            if (itemdeproduto == null || itemparte == null) reject("404");
            if (itemdeproduto.em_construcao === 0) reject("O item já não se encontra em construção.");

            var itemFound = itemdeproduto.partes.find(function (element) {
                return element.id == itemparte.id;
            })
            if (itemFound != null) {
                reject("400");
            }

            itemdeproduto.partes.push(itemparte);
            try {
                const servicoCatalogo = new CatalogoService();
                var dtoValidar = new FromItemDTO(itemdeproduto);
                var valido = await servicoCatalogo.ValidarItemDeProduto(dtoValidar);
                if (!valido) reject("Produto Inválido");
                repo.update(itemdeproduto, (err, savedItem) => {
                    if (err) reject(err);
                    else
                        resolve(savedItem)
                });
            } catch (e) {
                reject(e);
            }

        });
    }

    async removerParteItemDeProduto(itemdeproduto, itemparte) {
        const repo = new ItemRepository();

        return new Promise(async (resolve, reject) => {
            if (itemdeproduto == null || itemparte == null) reject("404");
            if (itemdeproduto.em_construcao === 0) reject("O item já não se encontra em construção.");

            //if (!itemdeproduto.partes.includes(itemparte)) reject("400");
            var itemFound = itemdeproduto.partes.find(function (element) {
                return element.id == itemparte.id;
            })
            if (itemFound == null) {
                reject("400");
            }

            var Partes = itemdeproduto.partes.filter(
                element => element.id != itemparte.id
            );
            itemdeproduto.partes = Partes;
            //itemdeproduto.partes.pull(itemparte);

            repo.update(itemdeproduto, (err, savedItem) => {
                if (err) reject(err);
                else
                    resolve(savedItem);
            });
        });
    }



    /**
     * Método para Obter Todos os Items
     * @param {*} items 
     */
    async ObterTodosOsItems(items) {
        return new Promise(async (resolve, reject) => {
            if (items == null || items.length == 0)
                reject("Items Inválido.");
            var promises = [];
            items.forEach(element => {
                promises.push(new Promise((resolve, reject) => {
                    const _repo = ItemRepository();
                    _repo.findById(element, (err, res) => {
                        if (err) reject(err);
                        else resolve(res);
                    })
                }))
            });
            Promise.all(promises).then((values) => {
                resolve(values);
            }).catch((reason) => reject(reason));
        });
    }

    async FindAllItemsDeProdutoAssociadosComAgregacoes(id) {
        const _repository = new ItemRepository();
        const _servico = new CatalogoService();
        return new Promise(async (resolve, reject) => {
            //Ir buscar o item com o id associado
            _repository.findById(id, (err, item) => {
                if (err) reject(err)
                else {
                    if (!item) reject(null)
                    else {
                        _servico.GetAllProdutosParteDeProduto(item.produto_id)
                            .then((values) => {
                                _repository.findAllByProdutoId(values,
                                    (error, items) => {
                                        if (error) reject(error);
                                        else {
                                            resolve(items);
                                        }
                                    })
                            })
                            .catch((reason) => {
                                reject(reason);
                            })
                    }
                }
            })

        });
    }
}
/**
    Método para construir o dto de validação a partir de um json
*/
async function construirDTOValidacao(itemDeProdutoJSON) {
    return new Promise(async (resolve, reject) => {
        itemvalidacaodto = new ItemProdutoValidacaoDTO(itemDeProdutoJSON);
        const repo = new ItemRepository();
        var promises = [];
        /*
            Código para ir buscar as partes
        */
        if (itemDeProdutoJSON.Partes != null) {
            itemDeProdutoJSON.Partes.forEach(element => {
                promises.push(new Promise((resolve, reject) => {
                    repo.findById(element, (err, item) => {
                        if (err) reject(err);
                        else
                            resolve(item);
                    });
                }));
            });
            Promise.all(promises)
                .then(values => {
                    values.forEach(element => {
                        itemvalidacaodto.Partes.push(new FromItemDTO(element));
                    });
                    resolve(itemvalidacaodto);
                }).catch((error) =>
                    reject(error)
                )
        } else {
            resolve(itemvalidacaodto);
        }
    })
}

async function ConstruirItemComNomes(itemvalidacaodto, itemDeProdutoJSON, produto) {
    itemvalidacaodto.Nome = produto.Nome;
    return ConstruirItem(itemvalidacaodto, itemDeProdutoJSON);

}
/**
 * Construir um item concretização para ser guardado
 * @param {*} itemvalidacaodto 
 * @param {*} itemDeProdutoJSON 
 */
async function ConstruirItem(itemvalidacaodto, itemDeProdutoJSON) {
    return new Promise((resolve, reject) => {
        var itemdeproduto = ItemDeProduto.construirItemDeDTO(itemvalidacaodto);
        var promises = [];
        const repo = new ItemRepository();
        if (itemDeProdutoJSON.Partes == null)
            resolve(itemdeproduto);
        itemDeProdutoJSON.Partes.forEach(element => {
            promises.push(new Promise((resolve, reject) => {
                repo.findById(element, (err, item) => {
                    if (err) reject(err);
                    else
                        resolve(item);
                });
            }));
        });
        Promise.all(promises).then((values) => {
            values.forEach(element => {
                itemdeproduto.partes.push(element);
            });
            resolve(itemdeproduto);
        }).catch((err) => {
            reject(err);
        });

    });
}

module.exports = ItemService;