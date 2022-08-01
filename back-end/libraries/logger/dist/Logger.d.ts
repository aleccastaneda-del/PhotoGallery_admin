import { ILoggerConfig, ConsoleTransport, FileTransport, Level } from './DataStructures';
declare function createLogger(passedConfig: ILoggerConfig): {
    error: (logData: string) => void;
    info: (logData: string) => void;
    debug: (logData: string) => void;
};
declare const _default: {
    createLogger: typeof createLogger;
    transports: {
        console: typeof ConsoleTransport;
        file: typeof FileTransport;
    };
    Level: typeof Level;
};
export = _default;
