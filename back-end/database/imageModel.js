const { DataTypes } = require('sequelize');
const BaseModel = require('./BaseModel');
const Folder = require('./folderModel');

class Image extends BaseModel {
    // DEV NOTE: Predetermined queries can be defined as functions here
    static connect(connection) {
        Image.init({
            ID: {
                type: DataTypes.UUID,
                defaultValue: DataTypes.UUIDV4,
                primaryKey: true
            },
            IMAGE: {
                type: DataTypes.BLOB('long'),
                allowNull: false
            },
            TITLE: {
                type: DataTypes.STRING(255),
                allowNull: false,
            },
            FAVORITE: {
                type: DataTypes.BOOLEAN,
                allowNull: false
            },
            TYPE: {
                type: DataTypes.STRING(36),
                allowNull: false
            },
            FOLDERID: {
                type: DataTypes.UUID,
                references: {
                    model: Folder,
                    key: 'ID'
                },
                allowNull: false
            }
        },{
            sequelize: connection,
            modelName: 'Image',
            tableName: 'IMAGE'
        });
        Image.sync();
        return Image;
    }
}

module.exports = Image