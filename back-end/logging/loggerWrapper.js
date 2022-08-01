const { createLogger, transports, Level } = require('logger');

function configureLogger() {
    return createLogger({
        level:Level.debug,
        transports:[ new transports.file({
            format: JSON.stringify,
            level: Level.error,
            logFullErrorStack: true,
            path: './web.log'
        }),new transports.console({
            format: JSON.stringify,
            level: Level.debug,
            logFullErrorStack: true,
            color: false
        })]
    });
}

module.exports = {
    configureLogger
};