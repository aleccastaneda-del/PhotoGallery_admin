const ImageBL = require('./ImageBL');


module.exports = function() {
    var dataAccessObjects = require('../database/DataAccessLayerInitializer')();
    return {
        imageBL: new ImageBL(dataAccessObjects)
    }
}