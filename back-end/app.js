require('dotenv').config();
const port = process.env.PORT;
const host = process.env.HOST;
const express = require('express');
const app = express();
const cors = require('cors');

// ---------------------- CONFIGURATIONS ----------------------
app.use(express.json({limit: '50mb'}));
app.use(cors());

// ---------------------- ROUTING ----------------------
app.use('/images',require('./routes/images'));
app.use('/folders',require('./routes/folders'));

// ---------------------- BUSSINESS/ETL PROCEDURES ----------------------
app.bl = require('./business/BusinessLayerInitializer')();
console.log('*********** Models Created ***********');

// ---------------------- SERVER BOOT ----------------------

var server = app.listen(port,host,() => {
    console.log(`*********** Server listening on port ${port} ***********`);
});
process.on('exit', async () => {
    await server.close();
});
process.on('uncaughtException', async () => {
    await server.close();
});
process.on('SIGTERM', async () => {
    await server.close();
});
process.on('SIGINT', async () => {
    await server.close();
});
module.exports = app;