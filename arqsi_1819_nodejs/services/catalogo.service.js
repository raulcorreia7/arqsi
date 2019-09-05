const request = require('request').defaults({
    rejectUnauthorized: false
});
const config = require('../config/config');

//const URL_PRODUTO = config.URL + 'Produto/';
const URL_VALIDAR = config.URL + 'Validar/';
class CatalogoService {
    constructor() {}

    async getProduto(id) {
        const curr_url = config.URL + 'Produto/' + id;
        return new Promise((resolve, reject) => {
            request.get({
                url: curr_url
            }, (error, response, body) => {
                if (error) reject(error);
                else {
                    if (response.statusCode == 200) {
                        var produto = JSON.parse(body);
                        resolve(produto);
                    } else {
                        reject(response.statusCode);
                    }
                }
            });
        });
    }

    async ValidarItemDeProduto(itemDTO) {
        const curr_url = URL_VALIDAR + 'ItemDeProduto';
        return new Promise((resolve, reject) => {
            request.post({
                    url: curr_url,
                    headers: {
                        "Content-Type": "application/json"
                    },
                    json: itemDTO
                },
                function (error, response, body) {
                    if (error) reject(error)
                    else {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            resolve(true);
                        } else {
                            reject(response.statusCode);
                        }
                    }
                }
            );
        });
    }

    async ValidarEncomenda(EncomendaDTO) {
        const curr_url = URL_VALIDAR + 'Encomenda';
        return new Promise((resolve, reject) => {
            console.log("Validar Encomenda" + JSON.stringify(EncomendaDTO));
            request.post({
                    url: curr_url,
                    headers: {
                        "Content-Type": "application/json"
                    },
                    json: EncomendaDTO
                },
                function (error, response, body) {
                    if (error) reject(error)
                    else {
                        if (response.statusCode >= 200 && response.statusCode < 300) {
                            resolve(true);
                        } else {
                            reject(response.statusCode);
                        }
                    }
                }
            );
        });
    }

    async GetAllProdutosParteDeProduto(id) {
        const curr_url = config.URL + 'Produto/' + id + '/PartesSimples';
        return new Promise((resolve, reject) => {
            request.get({
                url: curr_url
            }, (error, response, body) => {
                if (error) reject(error);
                else {
                    if (response.statusCode == 200) {
                        var partes = JSON.parse(body);
                        if (partes.Partes != null)
                            resolve(partes.Partes);
                        else
                            reject(400);
                    } else {
                        reject(response.statusCode);
                    }
                }
            })
        })
    }


}

module.exports = CatalogoService;