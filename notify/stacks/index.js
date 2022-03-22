import MyStack from "./MyStack";
import QueueStack from "./QueueStack";
import StorageStack from "./StorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  
  const storageStack = new StorageStack(app, "storage");
  
  new QueueStack(app,"WhatsappmessageQueue");
  new MyStack(app, "my-stack",{
    table: storageStack.table,
    bucket: storageStack.bucket
  });

  // Add more stacks
}
