import MyStack from "./MyStack";
import QueueStack from "./QueueStack";
import StorageStack from "./StorageStack";
import NotificationStorageStack from "./NotificationStorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  const queue = new QueueStack(app,"WhatsappmessageQueue");
  const notificationTable = new NotificationStorageStack(app,"notificationStorage");
  const storageStack = new StorageStack(app, "storage");
  new MyStack(app, "my-stack",{
    table: storageStack.table,
    bucket: storageStack.bucket,
    notificationTable: notificationTable.notificationTable,
    queue:queue.queue
  });

  // Notification Stack
}
