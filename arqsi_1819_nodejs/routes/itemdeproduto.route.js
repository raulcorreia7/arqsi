const express = require('express');
const router = express.Router();

const ItemdeprodutoController = require('../controllers/itemdeproduto.controller');

var ctrl = new ItemdeprodutoController();

router.post('/', ctrl.create);
router.get('/', ctrl.listAll);
router.get('/:id', ctrl.details);
router.put('/:id', ctrl.update);
router.put('/:id/AdicionarParte/:parte', ctrl.adicionarParte);
router.put('/:id/RemoverParte/:parte', ctrl.removerParte);
router.delete('/:id', ctrl.delete);
router.get('/:id/NaoParte',ctrl.findAllNonPartes);
router.get('/:id/ItemsAgregados',ctrl.findAllItemsAgregados);

module.exports = router;