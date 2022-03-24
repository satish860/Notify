using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api
{
    public class NotificationHandler
    {
        private readonly AmazonDynamoDBClient client;
        private readonly DynamoDBOperationConfig operationConfig;
        public NotificationHandler()
        {
            client = new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION")));
            operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = Environment.GetEnvironmentVariable("NOTIFICATION_TABLE")
            };
        }
        public async Task<APIGatewayHttpApiV2ProxyResponse> CreateNotification(APIGatewayHttpApiV2ProxyRequest request)
        {
            var notification = JsonSerializer.Deserialize<Notification>(request.Body);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            notification.NotificationId = Guid.NewGuid().ToString();
            await dynamoDBContext.SaveAsync(notification, operationConfig);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(notification),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
