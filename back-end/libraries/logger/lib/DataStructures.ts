import fs = require('fs');

export interface IInfo {
    date: Date;
    message: string;
    level: Level;
    location: string;
}

export enum Level {
    error = 0,
    info = 1,
    debug = 2
}

export interface IConsoleConfig extends ITransportConfig {
    color?: boolean;
}

export interface IFileConfig extends ITransportConfig {
    path: string;
}

export interface ITransportConfig {
    format?: (value: any) => string;
    level?: Level;
    template?: (value: IInfo) => string;
    logFullErrorStack?: boolean;
}

export interface ILoggerConfig {
    transports: Transport<ITransportConfig>[];
    level: Level;
}

const defaultConfig: ITransportConfig = {
    format: JSON.stringify,
    level: Level.info,
    template: (info: IInfo): string => {
        return `${JSON.stringify(info.date)} >> ${info.message} >> File: ${info.location}`;
    },
    logFullErrorStack: true
}

export abstract class Transport<T extends ITransportConfig>{
    public config: T;
    constructor(config: T){
        this.config = { ...defaultConfig, ...config };
    }
    public format(value: any): string {
        return this.config.format ? this.config.format(value) : '';
    }

    public getInfo(value: IInfo): string {
        return this.config.template ? this.config.template(value) : '';
    }

    abstract log(message: string, level: string): boolean;
}

export class ConsoleTransport extends Transport<IConsoleConfig> {
    constructor(config: IConsoleConfig) {
        super({...defaultConfig,...config});
    }

    public log(message: string, level: string): boolean {
        try {
            if (this.config.level !== undefined && Level[level] <= this.config.level) {
                const method = console[level] || console.log;

                method(`${level.toUpperCase()} >> ${message}`);
                return true;
            }
            return false;
        } catch (error) {
            console.error(error);
            return false;
        }
    }
}

export class FileTransport extends Transport<IFileConfig> {
    private fileStream: fs.WriteStream;
    constructor(config: IFileConfig) {
        super({...defaultConfig,...config});
        this.fileStream = fs.createWriteStream(config.path,{flags: fs.existsSync(this.config.path) ? 'a' : 'w'})
    }

    public log(message: string, level: string): boolean {
        try {
            if (this.config.level !== undefined && Level[level] <= this.config.level) {
                return this.fileStream.write(`${level.toUpperCase()} >> ${message}\n`);
            }
            return false;
        } catch (error) {
            console.error(error);
            return false;
        }
    }
}