export interface IInfo {
    date: Date;
    message: string;
    level: Level;
    location: string;
}
export declare enum Level {
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
export declare abstract class Transport<T extends ITransportConfig> {
    config: T;
    constructor(config: T);
    format(value: any): string;
    getInfo(value: IInfo): string;
    abstract log(message: string, level: string): boolean;
}
export declare class ConsoleTransport extends Transport<IConsoleConfig> {
    constructor(config: IConsoleConfig);
    log(message: string, level: string): boolean;
}
export declare class FileTransport extends Transport<IFileConfig> {
    private fileStream;
    constructor(config: IFileConfig);
    log(message: string, level: string): boolean;
}
