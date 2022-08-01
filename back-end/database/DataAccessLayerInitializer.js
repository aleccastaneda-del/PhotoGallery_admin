const { Sequelize } = require('sequelize');
const Image = require('./imageModel');
const Folder = require('./folderModel');

module.exports = function(){
    var connection = new Sequelize(process.env.DB_CONNECTION);
    return {
        folderDAO: Folder.connect(connection),
        imageDAO: Image.connect(connection)
    }
}