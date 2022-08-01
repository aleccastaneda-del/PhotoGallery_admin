const { DataTypes } = require('sequelize');
const BaseModel = require('./BaseModel');

class Folder extends BaseModel {
    // DEV NOTE: Predetermined queries can be defined as functions here
    static connect(connection) {
        Folder.init({
            ID: {
                type: DataTypes.UUID,
                defaultValue: DataTypes.UUIDV4,
                primaryKey: true,
                allowNull: false
            },
            NAME: {
                type: DataTypes.STRING(255),
                allowNull: false
            },
            TEAMID: {
                type: DataTypes.STRING(255),
                allowNull: false
            }
        },{
            sequelize: connection,
            modelName: 'Folder',
            tableName: 'FOLDER',
            timestamps: false
        });
        Folder.sync();
        return Folder;
    }
}

module.exports = Folder