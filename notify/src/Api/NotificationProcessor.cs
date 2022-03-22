using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class NotificationProcessor
    {
        public async Task FunctionHandler(SQSEvent sQSEvent,ILambdaContext lambdaContext)
        {
            await Task.CompletedTask;
        }
    }
}
