const express = require('express');
const router = express.Router();
const encomendaController = require('../controllers/encomenda.controller');

const controller = new encomendaController();

router.post('/', controller.create);
router.get('/', controller.listAll);
router.get('/NaoFinalizadas', controller.listEncomendasNaoFinalizadas);
router.get('/:id', controller.details);
router.put('/:id/AdicionarItem/:item', controller.addItem);
router.put('/:id/RemoverItem/:item', controller.remItem);
router.delete('/:id', controller.delete);
router.put('/:id/Finalizar', controller.finalizar);

module.exports = router;