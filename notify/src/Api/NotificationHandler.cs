using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.SQS;
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
        private readonly IAmazonSQS amazonSQS;
        public NotificationHandler()
        {
            var region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"));
            client = new AmazonDynamoDBClient(region);
            operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = Environment.GetEnvironmentVariable("NOTIFICATION_TABLE")
            };
            amazonSQS = new AmazonSQSClient(region);
        }
        public async Task<APIGatewayHttpApiV2ProxyResponse> CreateNotification(APIGatewayHttpApiV2ProxyRequest request)
        {
            var notification = JsonSerializer.Deserialize<Notification>(request.Body);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            notification.NotificationId = Guid.NewGuid().ToString();
            await dynamoDBContext.SaveAsync(notification, operationConfig);
            var url = Environment.GetEnvironmentVariable("queueUrl");
            var body = JsonSerializer.Serialize(notification);
            await amazonSQS.SendMessageAsync(url, body);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = body,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> GetNotification(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            var searchResult = dynamoDBContext.QueryAsync<Notification>(userId, operationConfig);
            var templates = new List<Notification>();
            do
            {
                List<Notification> nextSet = await searchResult.GetNextSetAsync();
                templates.AddRange(nextSet);
            }
            while (!searchResult.IsDone);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(templates),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
