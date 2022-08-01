
function checkParams(req,paramKeys) {
    let reqParams = {};
    switch (req.method) {
        case "POST":
        case "PUT":
            reqParams = Object.keys(req.body);
            break;
        case "DELETE":
        case "GET":
            reqParams = Object.keys(req.query);
            break;
        default:
            return false;
    }
    for (const key of paramKeys) {
        if (reqParams.indexOf(key) === -1) {
            return false;
        }
    }
    return true;
}

function base64ToBlob(base64Str) {
    const byteChars = Buffer.from(base64Str,'base64').toString('binary');
    const byteNums = new Array(byteChars.length);
    for (let i = 0; i < byteChars.length; i++) {
        byteNums[i] = byteChars.charCodeAt(i);
    }
    return new Blob([new Uint8Array(byteNums)]);
}

module.exports = {
    checkParams: checkParams,
    base64ToBlob: base64ToBlob
}