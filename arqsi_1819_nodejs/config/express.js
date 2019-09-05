const express = require('express');

const app = express();

const bodyParser = require('body-parser');

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

/*
app.use(function (req, res, next) {
    console.log('Route invalido.');
    var err = new Error('Recurso nao encontrado');
    err.status = 404;
    next(err);
});*/

app.use(function (e, req, res, next) {
    let status = e.status || 500;
    let error = { message: e.message };

    console.log(error);
    res.status(status).json(error);
});

module.exports = function () {
    return app;
};