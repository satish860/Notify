import MyStack from "./MyStack";
import StorageStack from "./StorageStack";

export default function main(app) {
  // Set default runtime for all functions
  app.setDefaultFunctionProps({
    runtime: "dotnetcore3.1"
  });
  
  const storageStack = new StorageStack(app, "storage");
  new MyStack(app, "my-stack",{
    table: storageStack.table,
  });

  // Add more stacks
}
