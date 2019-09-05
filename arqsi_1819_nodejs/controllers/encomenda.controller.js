const sendJsonResponse = require('../custom_modules/sendJsonResponse');
const EncomendaRepository = require('../repository/encomenda.repository');
const ItemdeProdutoRepository = require('../repository/itemdeproduto.repository');
const EncomendaService = require('../services/encomenda.service');
const EncomendaDTO = require('../dto/EncomendaDTO');
const ItemService = require('../services/item.service');
const EncomendaDTODetail = require('../dto/EncomendaDTODetail');
class EncomendaController {

    constructor() { }

    /**
     * Lista todas as encomendas com um DTO simples,
     * mostrando apenas o id, produto e o id das partes
     * @param {*} req 
     * @param {*} res 
     */
    listAll(req, res) {
        let repository = new EncomendaRepository();
        repository.findAll().then(encomendas => {
            var dtos = [];
            encomendas.forEach((elemento) => {
                let dto = new EncomendaDTO(elemento);
                dtos.push(dto);
            })
            sendJsonResponse(res, 200, dtos)
        });
    }

    listEncomendasNaoFinalizadas(req, res) {
        let repository = new EncomendaRepository();
        repository.findAll().then(encomendas => {
            var dtos = [];
            encomendas.forEach((elemento) => {
                if (elemento.em_construcao == 1) {
                    var dto = new EncomendaDTO(elemento);
                    dtos.push(dto);
                }
            })
            sendJsonResponse(res, 200, dtos);
        })
    }
    /**
     * Criar uma encomenda apenas
     * @param {*} req 
     * @param {*} res 
     * @param {*} next 
     */
    async create(req, res, next) {
        const _service = new EncomendaService();
        const encomendaData = req.body;
        try {
            var encomenda = await _service.criarEncomenda(encomendaData);
            let dto = new EncomendaDTODetail(encomenda);
            sendJsonResponse(res, 201, dto);
        } catch (e) {
            sendJsonResponse(res, 400, null);
        }
    }
    /**
     * Mostra com muito detalhe uma encomenda,
     * Inclui mostrar os Produtos Recursivamente
     * @param {*} req 
     * @param {*} res 
     */
    details(req, res) {
        let repository = new EncomendaRepository();
        repository.findById(req.params.id, (err, encomenda) => {
            if (err) sendJsonResponse(res, 400, null);
            else {
                let dto = new EncomendaDTODetail(encomenda);
                sendJsonResponse(res, 200, dto);
            }
        });
    }
    /**
     * Apaga uma encomenda
     * @param {*} req 
     * @param {*} res 
     */
    delete(req, res) {
        const repository = new EncomendaRepository();
        repository.delete(req.params.id, (err, encomenda) => {
            if (err) sendJsonResponse(res, 400, null);
            else if (encomenda == null) sendJsonResponse(res, 404, null);
            else {
                sendJsonResponse(res, 200, "Encomenda apagada com sucesso.");
            }
        });
    }
    /**
     * Adiciona um item a uma encomenda
     * @param {*} req 
     * @param {*} res 
     */
    addItem(req, res) {
        const _encomendarepository = new EncomendaRepository();
        const _itemrepository = new ItemdeProdutoRepository();
        const _service = new EncomendaService();

        var encomenda = null;
        var item = null;
        var promises = [];
        //Buscar encomenda
        promises.push(new Promise((resolve, reject) => {
            _encomendarepository.findById(req.params.id,
                (err, enc) => {
                    if (err || enc == null) reject(err);
                    else {
                        encomenda = enc;
                        resolve(enc);
                    }
                });
        }))
        //Parte
        promises.push(new Promise((resolve, reject) => {
            _itemrepository.findById(req.params.item,
                (err, itemdeproduto) => {
                    if (err || itemdeproduto == null) reject(err);
                    else {
                        item = itemdeproduto;
                        resolve(itemdeproduto);
                    }
                });
        }));
        Promise.all(promises)
            .then((values) => {
                _service.adicionarItem(encomenda, item).then((enc) => {
                    const dto = new EncomendaDTO(enc);
                    sendJsonResponse(res, 200, dto);
                }).catch((reason) => {
                    if (reason == "404")
                        sendJsonResponse(res, 404, null);
                    else
                        sendJsonResponse(res, 400, reason);
                });
            })
            .catch((err) => {
                sendJsonResponse(res, 400, null);
            });
    }
    /**
     * Remove um item de uma encomenda
     * @param {*} req 
     * @param {*} res 
     */
    remItem(req, res) {
        const _encomendarepository = new EncomendaRepository();
        const _itemrepository = new ItemdeProdutoRepository();
        const _service = new EncomendaService();

        var encomenda = null;
        var item = null;
        var promises = [];
        //Buscar encomenda
        promises.push(new Promise((resolve, reject) => {
            _encomendarepository.findById(req.params.id,
                (err, enc) => {
                    if (err || enc == null) reject(err);
                    else {
                        encomenda = enc;
                        resolve(enc);
                    }
                });
        }))
        //Parte
        promises.push(new Promise((resolve, reject) => {
            _itemrepository.findById(req.params.item,
                (err, itemdeproduto) => {
                    if (err || itemdeproduto == null) reject(err);
                    else {
                        item = itemdeproduto;
                        resolve(itemdeproduto);
                    }
                });
        }));
        Promise.all(promises)
            .then((values) => {
                _service.removerItem(encomenda, item).then((enc) => {
                    const dto = new EncomendaDTO(enc);
                    sendJsonResponse(res, 200, dto);
                }).catch((reason) => {
                    if (reason == "404")
                        sendJsonResponse(res, 404, null);
                    else
                        sendJsonResponse(res, 400, reason);
                });
            })
            .catch((err) => {
                sendJsonResponse(res, 400, null);
            });
    }
    /**
     * Finaliza uma encomenda, fazendo com que esta não possa ser mais alterada
     * @param {*} req 
     * @param {*} res 
     */
    async finalizar(req, res) {
        const _repository = new EncomendaRepository();
        const _servico = new EncomendaService();
        _repository.findById(req.params.id,
            (err, encomenda) => {
                if (err) sendJsonResponse(res, 400, null);
                else if (encomenda == null) sendJsonResponse(res, 404, null);
                else {
                    if (encomenda.em_construcao == 0) sendJsonResponse(res, 400, null);
                    else {
                        _servico.validarEncomenda(encomenda)
                            .then((value) => {
                                encomenda.finalizar();
                                _repository.update(encomenda, (err) => {
                                    if (err)
                                        sendJsonResponse(res, 400, null);
                                    else {
                                        let dto = new EncomendaDTODetail(encomenda);
                                        sendJsonResponse(res, 200, dto);
                                    }
                                })
                            })
                            .catch((err) => {
                                sendJsonResponse(res, 400, err);
                            });

                    }
                }
            });
    }

}


module.exports = EncomendaController;