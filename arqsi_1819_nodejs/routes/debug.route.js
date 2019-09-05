const express = require('express');
const router = express.Router();
const mongoose = require('mongoose');
const ItemDeProduto = mongoose.model('ItemDeProduto');

router.get('/Delete', (req, res) => {
    mongoose.model('ItemDeProduto').deleteMany({}, () => {
        console.log("Removed ItemDeProduto");
    });
    mongoose.model('Encomenda').deleteMany({}, () => {
        console.log("Removed Encomenda");
    });
    mongoose.connection.dropCollection('identitycounters',
        (err) => {
            if (err) console.log("Error reseting counters;")
        }
    );
    res.status(200).send();

});
module.exports = router;