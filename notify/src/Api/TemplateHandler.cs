using System.Collections.Generic;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.DynamoDBv2.DocumentModel;

namespace Api
{
    public class TemplateHandler
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
        public async Task<APIGatewayHttpApiV2ProxyResponse> CreateTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var serializer = JsonSerializer.Deserialize<Template>(request.Body);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            var TableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            Template template = new Template
            {
                TemplateId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Name = serializer.Name,
                Text = serializer.Text,
            };
            await dynamoDBContext.SaveAsync(template, new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            });
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(template),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse UpdateTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Update Templates for Message - {request.RequestContext.Time}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse DeleteTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            var templateId = request.PathParameters["template_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            var TableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            Template template = new Template
            {
                UserId = userId,
                TemplateId = templateId,
            };
            dynamoDBContext.DeleteAsync(template, new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            });
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Delete Templates for Message - {request.RequestContext.Time}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public APIGatewayHttpApiV2ProxyResponse DetailsTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Details Templates for Message - {request.RequestContext.Time}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayHttpApiV2ProxyResponse> ListTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            var userId = request.PathParameters["user_id"];
            DynamoDBContext dynamoDBContext = new DynamoDBContext(client);
            var TableName = Environment.GetEnvironmentVariable("TABLE_NAME");
            var searchResult = dynamoDBContext.QueryAsync<Template>(userId, new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            });
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
