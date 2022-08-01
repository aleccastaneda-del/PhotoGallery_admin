"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FileTransport = exports.ConsoleTransport = exports.Transport = exports.Level = void 0;
const fs = require("fs");
var Level;
(function (Level) {
    Level[Level["error"] = 0] = "error";
    Level[Level["info"] = 1] = "info";
    Level[Level["debug"] = 2] = "debug";
})(Level = exports.Level || (exports.Level = {}));
const defaultConfig = {
    format: JSON.stringify,
    level: Level.info,
    template: (info) => {
        return `${JSON.stringify(info.date)} >> ${info.message} >> File: ${info.location}`;
    },
    logFullErrorStack: true
};
class Transport {
    constructor(config) {
        this.config = Object.assign(Object.assign({}, defaultConfig), config);
    }
    format(value) {
        return this.config.format ? this.config.format(value) : '';
    }
    getInfo(value) {
        return this.config.template ? this.config.template(value) : '';
    }
}
exports.Transport = Transport;
class ConsoleTransport extends Transport {
    constructor(config) {
        super(Object.assign(Object.assign({}, defaultConfig), config));
    }
    log(message, level) {
        try {
            if (this.config.level !== undefined && Level[level] <= this.config.level) {
                const method = console[level] || console.log;
                method(`${level.toUpperCase()} >> ${message}`);
                return true;
            }
            return false;
        }
        catch (error) {
            console.error(error);
            return false;
        }
    }
}
exports.ConsoleTransport = ConsoleTransport;
class FileTransport extends Transport {
    constructor(config) {
        super(Object.assign(Object.assign({}, defaultConfig), config));
        this.fileStream = fs.createWriteStream(config.path, { flags: fs.existsSync(this.config.path) ? 'a' : 'w' });
    }
    log(message, level) {
        try {
            if (this.config.level !== undefined && Level[level] <= this.config.level) {
                return this.fileStream.write(`${level.toUpperCase()} >> ${message}\n`);
            }
            return false;
        }
        catch (error) {
            console.error(error);
            return false;
        }
    }
}
exports.FileTransport = FileTransport;
