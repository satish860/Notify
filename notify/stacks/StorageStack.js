import * as sst from "@serverless-stack/resources";

export default class StorageStack extends sst.Stack {
    table;
    bucket;

    constructor(scope, id, props) {
        super(scope, id, props)

        this.table = new sst.Table(this, "Templates", {
            fields: {
                user_Id: sst.TableFieldType.STRING,
                template_id: sst.TableFieldType.STRING
            },
            primaryIndex: { partitionKey: "user_Id", sortKey: "template_id" }
        });

        this.bucket = new sst.Bucket(this, "uploads", {
            s3Bucket: {
                cors: [
                    {
                        maxAge: 3000,
                        allowedOrigins: ["*"],
                        allowedHeaders: ["*"],
                        allowedMethods: ["GET", "PUT", "POST", "DELETE", "HEAD"]
                    },
                ],
            },
        });
    }
}