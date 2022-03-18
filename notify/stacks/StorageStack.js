import * as sst from "@serverless-stack/resources";

export default class StorageStack extends sst.Stack{
    table;

    constructor(scope,id,props){
        super(scope,id,props)

        this.table = new sst.Table(this,"Templates",{
            fields: {
                user_Id : sst.TableFieldType.STRING,
                template_id : sst.TableFieldType.STRING
            },
            primaryIndex: {partitionKey:"user_Id",sortKey:"template_id"}
        })
    }
}