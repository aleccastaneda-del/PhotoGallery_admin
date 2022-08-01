import { ILoggerConfig, ConsoleTransport, FileTransport, Level } from './DataStructures';

function getLocation(logFullStack: boolean = false): string {
    try {
        throw new Error("STACK TRACE");
    } catch (error) {
        try {
            const err: Error = error;
            if (err.stack) {
                if (!logFullStack) {
                    const callStackLines = err.stack.split('\n');
                    for (let i = 0; i < callStackLines.length; i++) {
                        if (callStackLines[i].trim().startsWith('at')) {
                            return `\n${callStackLines[i+2]}`;
                        }
                    }
                    return '*** NO CALL STACK ***';
                }
                return err.stack;
            }
            return '';
        } catch (e) {
            return '';
        }
    }
}

const transports = {console: ConsoleTransport, file: FileTransport};

const defaultConfig: ILoggerConfig = {
    transports: [new transports.console({level:Level.debug})],
    level: Level.debug
};

function createLogger(passedConfig: ILoggerConfig) {
    const config = { ...defaultConfig, ...passedConfig };
    const isLevelAllowed = (level: Level) => {
        return level <= config.level;
    }
    const log = (level: string) => {
        if (isLevelAllowed(Level[level])) {
            return (logData: string): void => {
                for(const nextTransport of config.transports) {
                    const message = nextTransport.getInfo({date: new Date(), message: logData,level: Level[level],location: getLocation(nextTransport.config.logFullErrorStack)});
                    nextTransport.log(message,level);
                }
            } 
        }
        return () => {};
    }
    return {
        error: log('error'),
        info: log('info'),
        debug: log('debug')
    }
};

export = {
    createLogger,
    transports,
    Level
}