var config = {};


config.URL_CATALOGO = "https://closify.azurewebsites.net/api/";
config.URL_DEBUG = "https://localhost:5001/api/";
config.URL = "";

var env = process.argv[2] || 'prod';
switch (env) {
    case 'dev':
        console.log('running in development environment');
        config.URL = config.URL_DEBUG;
        break;
    case 'prod':
        config.URL = config.URL_CATALOGO;
        console.log('running in production environment');
        break;
}
module.exports = config;