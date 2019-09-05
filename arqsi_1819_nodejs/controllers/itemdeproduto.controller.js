const sendJsonResponse = require('../custom_modules/sendJsonResponse');
const ItemdeProdutoRepository = require('../repository/itemdeproduto.repository');
const ItemService = require('../services/item.service');
const ItemDeProdutoDTO = require('../dto/ItemDeProdutoDTO');
const ItemDeProdutoDTOSimples = require('../dto/ItemDeProdutoSimplesDTO');
//  const to = require('await-to-js').default;
class ItemDeProdutoController {
  // constructor () {}

  listAll(req, res) {
    let repository = new ItemdeProdutoRepository();
    repository.findAll().then(items => {
      var dtos = [];
      items.forEach((element) => {
        let dto = new ItemDeProdutoDTO(element);
        dtos.push(dto);
      });
      sendJsonResponse(res, 200, dtos)
    });
  }

  create(req, res, next) {
    const _service = new ItemService();
    _service.criarItemDeProduto(req.body).then((produto) => {
      const dto = new ItemDeProdutoDTO(produto);
      sendJsonResponse(res, 201, dto);
    }).catch((reason) => {
      if (reason == "404")
        sendJsonResponse(res, 404, null);
      else
        sendJsonResponse(res, 400, reason);
    });
  }

  details(req, res) {
    const repository = new ItemdeProdutoRepository();
    repository.findById(req.params.id, (err, itemdeproduto) => {
      if (itemdeproduto == null || err) sendJsonResponse(res, 404, null);
      else {
        const dto = new ItemDeProdutoDTO(itemdeproduto);
        sendJsonResponse(res, 200, dto);
      }
    });
  }

  delete(req, res) {
    const repository = new ItemdeProdutoRepository();
    repository.delete(req.params.id, (err, itemdeproduto) => {
      if (err || itemdeproduto == null) {
        sendJsonResponse(res, 404, null);
      } else {
        sendJsonResponse(res, 200, null);
      }
    });
  }

  update(req, res) {
    const _service = new ItemService();
    const _repository = new ItemdeProdutoRepository();
    _repository.findById(req.params.id, (err, itemdeproduto) => {
      if (itemdeproduto == null || err) {
        sendJsonResponse(res, 404, null);
      } else {
        _service.atualizarItemDeProduto(req.params.id, req.body, itemdeproduto).then((produto) => {
            const dto = new ItemDeProdutoDTO(produto);
            sendJsonResponse(res, 204, dto);
          })
          .catch((reason) => {
            if (reason == "404")
              sendJsonResponse(res, 404, null);
            else
              sendJsonResponse(res, 400, reason);
          });
      }
    });
  }

  adicionarParte(req, res) {
    const _service = new ItemService();
    var [base, parte] = [null, null];
    ObterItemBaseEParte(req)
      .then((values) => {
        [base, parte] = values;
        _service.adicionarParteItemDeProduto(base, parte)
          .then((produto) => {
            const dto = new ItemDeProdutoDTO(produto);
            sendJsonResponse(res, 201, dto);
          })
          .catch((reason) => {
            if (reason == "404")
              sendJsonResponse(res, 404, null);
            else
              sendJsonResponse(res, 400, reason);
          });
      })
      .catch((err) => {
        sendJsonResponse(res, 404, null);
      });
  }

  removerParte(req, res) {
    const _service = new ItemService();
    var [base, parte] = [null, null];
    ObterItemBaseEParte(req)
      .then((values) => {
        [base, parte] = values;
        _service.removerParteItemDeProduto(base, parte)
          .then((produto) => {
            const dto = new ItemDeProdutoDTO(produto);
            sendJsonResponse(res, 200, dto);
          }).catch((reason) => {
            if (reason == "404")
              sendJsonResponse(res, 404, null);
            else
              sendJsonResponse(res, 400, reason);
          });
      })
      .catch((err) => {
        sendJsonResponse(res, 404, null);
      });

  }

  findAllNonPartes(req, res) {
    const _repository = new ItemdeProdutoRepository();
    const _servico = new ItemService();
    _repository.findAllNaoPartes(req.params.id,
      (err, allitems) => {
        if (err) sendJsonResponse(res, 400, null);
        else {
          const alldtos = [];
          allitems.forEach(e => {
            alldtos.push(new ItemDeProdutoDTO(e));
          })
          sendJsonResponse(res, 200, alldtos);
        }
      });

  }

  findAllItemsAgregados(req, res) {
    const _servico = new ItemService();
    _servico.FindAllItemsDeProdutoAssociadosComAgregacoes(req.params.id)
      .then((values) => {
        const alldtos = [];
        values.forEach(e => {
          if (e != null)
            alldtos.push(new ItemDeProdutoDTO(e));
        })
        sendJsonResponse(res, 200, alldtos);
      })
      .catch((reason) => {
        sendJsonResponse(res, 400, reason);
      })
  }
}
async function ObterItemBaseEParte(req) {
  return new Promise((resolve, reject) => {
    const _repository = new ItemdeProdutoRepository();
    var promises = [];
    var base = null;
    var parte = null;
    promises.push(new Promise((resolve, reject) => {
      _repository.findById(req.params.id,
        (err, itemdeproduto) => {
          if (err || itemdeproduto == null) reject(err);
          else {
            base = itemdeproduto;
            resolve(itemdeproduto);
          }
        });
    }));
    //Parte
    promises.push(new Promise((resolve, reject) => {
      _repository.findById(req.params.parte,
        (err, itemdeproduto) => {
          if (err || itemdeproduto == null) reject(err);
          else {
            parte = itemdeproduto;
            resolve(itemdeproduto);
          }
        });
    }));
    Promise.all(promises)
      .then((values) => {
        resolve([base, parte])
      })
      .catch((err) => {
        reject([base, parte]);
      })
  });
}

module.exports = ItemDeProdutoController;