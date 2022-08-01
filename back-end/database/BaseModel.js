const { Model } = require("sequelize");
const { QueryTypes } = require("sequelize");


class BaseModel extends Model {
    // DEV NOTE: Predetermined queries can be defined as functions here
    static async select(props, condition) {
        const whereCondition = condition !== null && condition !== undefined ? `WHERE ${condition}` : '';
        return await this.sequelize.query(`SELECT ${props ? '"' + props.join('","') + '"':'*'} FROM public."${this.tableName}" ${whereCondition}`,{ type: QueryTypes.SELECT});
    }

    static async selectOrderBy(props,condition,orderByProp,{descending=false,limit}) {
        const whereCondition = condition !== null && condition !== undefined ? `WHERE ${condition}` : '';
        const ascendOrder = descending ? 'DESC' : 'ASC';
        const rowLimit = limit ? `LIMIT ${limit}` : '';
        return await this.sequelize.query(`SELECT ${props ? '"' + props.join('","') + '"':'*'} FROM public."${this.tableName}" ${whereCondition} ORDER BY "${orderByProp}" ${ascendOrder} ${rowLimit}`,{ type: QueryTypes.SELECT});
    }
}

module.exports = BaseModel;