import MyStack from "./MyStack";
import QueueStack from "./QueueStack";
import StorageStack from "./StorageStack";
import NotificationStorageStack from "./NotificationStorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  new QueueStack(app,"WhatsappmessageQueue");
  new NotificationStorageStack(app,"notificationStorage");
  const storageStack = new StorageStack(app, "storage");
  new MyStack(app, "my-stack",{
    table: storageStack.table,
    bucket: storageStack.bucket
  });

  // Notification Stack
}
