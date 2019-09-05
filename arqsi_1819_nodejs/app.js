const express = require('./config/express.js');
const cors = require('cors');

// Set up mongoose connection
const mongoose = require('mongoose');
let dev_db_url = 'mongodb://administrador:Closify18@ds046037.mlab.com:46037/closify';
let mongoDB = process.env.MONGODB_URI || dev_db_url;
mongoose.connect(mongoDB, {
	useNewUrlParser: true
});
mongoose.Promise = global.Promise;
let db = mongoose.connection;
db.on('error', console.error.bind(console, 'MongoDB connection error:'));

// Require Routes
const encomenda = require('./routes/encomenda.route');
const itemdeproduto = require('./routes/itemdeproduto.route');
const debug = require('./routes/debug.route');
// Express
const app = express();
app.use(cors());
//Config
const config = require('./config/config');

// Use Routes
app.use('/api/Encomenda', encomenda);
app.use('/api/ItemDeProduto', itemdeproduto);
app.use('/api/Debug', debug);

var port = process.env.port || 1337;


app.listen(port, () => {
	console.log('Server is up and running on port ' + port);
});