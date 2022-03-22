import * as sst from "@serverless-stack/resources";

export default class QueueStack extends sst.Stack{
    queue;
    constructor(scope,id,props){
        super(scope,id,props);

        this.queue = new sst.Queue(this,"WhatsAppMessageQueue",{
            consumer:"Api::Api.NotificationProcessor::FunctionHandler"
        })
    }
} 