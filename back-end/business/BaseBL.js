const { configureLogger } = require("../logging/loggerWrapper");

module.exports = class BaseBL {
    constructor() {
        this.logger = configureLogger();
    }
}