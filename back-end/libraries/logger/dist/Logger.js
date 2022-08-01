"use strict";
const DataStructures_1 = require("./DataStructures");
function getLocation(logFullStack = false) {
    try {
        throw new Error("STACK TRACE");
    }
    catch (error) {
        try {
            const err = error;
            if (err.stack) {
                if (!logFullStack) {
                    const callStackLines = err.stack.split('\n');
                    for (let i = 0; i < callStackLines.length; i++) {
                        if (callStackLines[i].trim().startsWith('at')) {
                            return `\n${callStackLines[i + 2]}`;
                        }
                    }
                    return '*** NO CALL STACK ***';
                }
                return err.stack;
            }
            return '';
        }
        catch (e) {
            return '';
        }
    }
}
const transports = { console: DataStructures_1.ConsoleTransport, file: DataStructures_1.FileTransport };
const defaultConfig = {
    transports: [new transports.console({ level: DataStructures_1.Level.debug })],
    level: DataStructures_1.Level.debug
};
function createLogger(passedConfig) {
    const config = Object.assign(Object.assign({}, defaultConfig), passedConfig);
    const isLevelAllowed = (level) => {
        return level <= config.level;
    };
    const log = (level) => {
        if (isLevelAllowed(DataStructures_1.Level[level])) {
            return (logData) => {
                for (const nextTransport of config.transports) {
                    const message = nextTransport.getInfo({ date: new Date(), message: logData, level: DataStructures_1.Level[level], location: getLocation(nextTransport.config.logFullErrorStack) });
                    nextTransport.log(message, level);
                }
            };
        }
        return () => { };
    };
    return {
        error: log('error'),
        info: log('info'),
        debug: log('debug')
    };
}
;
module.exports = {
    createLogger,
    transports,
    Level: DataStructures_1.Level
};
