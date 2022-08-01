const { checkParams, base64ToBlob } = require('./util/common');
const BaseBL = require('./BaseBL');

module.exports = class ImageBL extends BaseBL {
    constructor(dataAccessObjects){
        super();
        this.imageDAO = dataAccessObjects.imageDAO;
        this.folderDAO = dataAccessObjects.folderDAO;
    }

    async list(req) {
        if (!checkParams(req,['folderId'])) {
            return {status: 400, data: "Required parameters: folderId (string)"};
        }
        try {
            const imageList = await this.imageDAO.findAll({ where: { FOLDERID: req.query.folderId } });
            let resultList = [];
            for (const image of imageList) {
                let jsonImage = image.toJSON();
                jsonImage.IMAGE = Buffer.from(jsonImage.IMAGE,'buffer').toString('base64');
                resultList.push(jsonImage);
            }
            return {status: 200, data: resultList};
        } catch (ex) {
            this.logger.error(`list - Error occurred while retrieving all images in folder:\n ${ex}`);
            return {status: 500, data: "list - Error occurred while retrieving all images in folder"};
        }
    }

    async getImageInfo(req) {
        if (!checkParams(req,['id'])) {
            return {status: 400, data: "Required parameters: id (string)"};
        }
        try {
            let imageData = await this.imageDAO.findAll({ where: { ID: req.query.id } });
            imageData = imageData[0].toJSON()
            imageData.IMAGE = Buffer.from(imageData.IMAGE,'buffer').toString('base64');
            return {status: 200, data: imageData};
        } catch (ex) {
            this.logger.error(`getImageInfo - Error occurred while querying image data:\n ${ex}`);
            return {status: 500, data: "getImageInfo - Error occurred while querying image data"};
        }
    }

    async add(req) {
        // NOTE - 'favorite' boolean param is optional (default is not favorite)
        if (!checkParams(req,['image','title','type','folderId'])) {
            return {status: 400, data: "Required parameters: image (binary/blob), title (string), type (string), folderId (string)"};
        }
        try {
            const checkFolder = await this.folderDAO.findAll({ where: { ID: req.body.folderId } })
            if (!checkFolder || checkFolder.length === 0) {
                return {status: 422, data: "folder specified does not exist"};
            }
            const imageBinary = Buffer.from(req.body.image,'base64');
            const imageData = await this.imageDAO.create({ IMAGE: imageBinary, TITLE: req.body.title, TYPE: req.body.type, FOLDERID: req.body.folderId, FAVORITE: req.body.favorite || false});
            let jsonImage = imageData.toJSON();
            jsonImage.IMAGE = Buffer.from(jsonImage.IMAGE,'buffer').toString('base64');
            return {status: 200, data: jsonImage};
        } catch (ex) {
            this.logger.error(`add - Error occurred while adding a new image to folder:\n ${ex}`);
            return {status: 500, data: "add - Error occurred while adding a new image to folder"};
        }
    }

    async delete(req) {
        if (!checkParams(req,['id'])) {
            return {status: 400, data: "Required parameters: id (string)"};
        }
        try {
            const deletedImageCount = await this.imageDAO.destroy({ where: { ID: req.query.id } });
            return {status: 200, data: deletedImageCount};
        } catch (ex) {
            this.logger.error(`delete - Error occurred while deleting image data:\n ${ex}`);
            return {status: 500, data: "delete - Error occurred while deleting image data"};
        }
    }

    async toggleFavorite(req) {
        if (!checkParams(req,['id'])) {
            return {status: 400, data: "Required parameters: id (string)"};
        }
        try {
            let tmpData = await this.imageDAO.findAll({ where: { ID: req.body.id } });
            tmpData = tmpData[0];
            tmpData.FAVORITE = !tmpData.FAVORITE;
            let imageData = await tmpData.save();
            imageData.IMAGE = Buffer.from(imageData.IMAGE,'buffer').toString('base64');
            imageData = imageData.toJSON();
            return {status: 200, data: imageData};
        } catch (ex) {
            this.logger.error(`toggleFavorite - Error occurred while toggling image favorite status:\n${ex}`);
            return {status: 500, data: `toggleFavorite - Error occurred while toggling image favorite status:\n${ex}`};
        }
    }
    async listFolders() {
        try {
            const folderData = await this.folderDAO.findAll();
            return {status: 200, data: folderData};
        } catch (ex) {
            this.logger.error(`list - Error occurred while querying folder data:\n${ex}`);
            return {status: 500, data: `list - Error occurred while querying folder data:\n${ex}`};
        }
    }
    async deleteFolder(req) {
        if (!checkParams(req,['id'])) {
            return {status: 400, data: "Required parameters: id (string)"};
        }
        try {
            const deletedImageCount = await this.imageDAO.destroy({ where: { FOLDERID: req.query.id } });
            const deletedFolderCount = await this.folderDAO.destroy({ where: { ID: req.query.id } });
            console.log (`DELETE: folder count = ${deletedFolderCount} | image count = ${deletedImageCount}`)
            return {status: 200, data: deletedFolderCount};
        } catch (ex) {
            this.logger.error(`delete - Error occurred while deleting folder data:\n${ex}`);
            return {status: 500, data: `delete - Error occurred while deleting folder data:\n${ex}`};
        }
    }
    async addFolder(req) {
        if (!checkParams(req,['name','teamId'])) {
            return {status: 400, data: "Required parameters: name (string), team (string)"};
        }
        try {
            const folderData = await this.folderDAO.create({NAME: req.body.name, TEAMID: req.body.teamId});
            let folderRes = folderData.toJSON();
            return {status: 200, data: folderRes};
        } catch (ex) {
            this.logger.error(`add - Error occurred while adding a new folder:\n${ex}`);
            return {status: 500, data: `add - Error occurred while adding a new folder:\n${ex}`};
        }
    }
    async getFolderInfo(req) {
        if (!checkParams(req,['id'])) {
            return {status: 400, data: "Required parameters: id (string)"};
        }
        try {
            const folderData = await this.folderDAO.findAll({ where: { ID: req.query.id } });
            return {status: 200, data: folderData[0]};
        } catch (ex) {
            this.logger.error(`add - Error occurred while adding folder:\n${ex}`);
            return {status: 500, data: `add - Error occurred while adding folder:\n${ex}`};
        }
    }
}