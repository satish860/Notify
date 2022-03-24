import * as sst from "@serverless-stack/resources";

export default class MyStack extends sst.Stack {
  constructor(scope, id, props) {
    super(scope, id, props);
    
    const { table,bucket } = props;


    // Create a HTTP API
    const api = new sst.Api(this, "Api", {
      defaultFunctionProps: {
        environment:{
          TABLE_NAME: table.tableName,
          BUCKET_NAME: bucket.bucketName
        },
        srcPath: "src/Api",
      },
      routes: {
        "POST    /templates": "Api::Api.TemplateHandler::CreateTemplates",
        "PUT     /templates/{user_id}/{template_id}": "Api::Api.TemplateHandler::UpdateTemplates",
        "Delete  /templates/{user_id}/{template_id}": "Api::Api.TemplateHandler::DeleteTemplates",
        "Get     /templates/{user_id}/{template_id}": "Api::Api.TemplateHandler::DetailsTemplates",
        "Get     /templates/{user_id}": "Api::Api.TemplateHandler::ListTemplates",
        "POST    /upload-url/{user_id}": "Api::Api.Uploads::GetUploadUrl" ,
        "POST    /notification": "Api::Api.NotificationHandler::CreateNotification"
      }
    });
    api.attachPermissions([bucket])
    // Show the endpoint in the output
    this.addOutputs({
      "ApiEndpoint": api.url,
    });
  }
}
