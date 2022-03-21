using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using System;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;
using System.Text.Json;

namespace Api
{
    public class TemplateHandler
    {
        private readonly AmazonDynamoDBClient client;
        private readonly DynamoDBOperationConfig operationConfig;
        public TemplateHandler()
        {
            client = new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION")));
            operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = Environment.GetEnvironmentVariable("TABLE_NAME")
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> CreateTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var serializer = JsonSerializer.Deserialize<Template>(request.Body);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            Template template = new Template
            {
                TemplateId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Name = serializer.Name,
                Text = serializer.Text,
            };
            await dynamoDBContext.SaveAsync(template, operationConfig);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(template),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> UpdateTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var template = JsonSerializer.Deserialize<Template>(request.Body);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            await dynamoDBContext.SaveAsync(template, operationConfig);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.Accepted,
                Body = $"Update accepted for processing.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse DeleteTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            var templateId = request.PathParameters["template_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            Template template = new Template
            {
                UserId = userId,
                TemplateId = templateId,
            };
            dynamoDBContext.DeleteAsync(template,operationConfig);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.NoContent,
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> DetailsTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            var templateId = request.PathParameters["template_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            Template template = new Template
            {
                UserId = userId,
                TemplateId = templateId,
            };
            var templateDetails = await dynamoDBContext.LoadAsync(template, operationConfig);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(templateDetails),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> ListTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            var searchResult = dynamoDBContext.QueryAsync<Template>(userId, operationConfig);
            var templates = new List<Template>();
            do
            {
                List<Template> nextSet = await searchResult.GetNextSetAsync();
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
