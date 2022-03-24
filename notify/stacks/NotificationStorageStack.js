import * as sst from "@serverless-stack/resources";

export default class NotificationStorageStack extends sst.Stack {

    notificationTable;

    constructor(scope,id,props){
        super(scope,id,props);

        this.notificationTable = new sst.Table(this,"Notification",{
            fields:{
                user_Id:sst.TableFieldType.STRING,
                notification_Id:sst.TableFieldType.STRING
            },
            primaryIndex: { partitionKey: "user_Id", sortKey: "notification_Id" }
        });

    }
}
