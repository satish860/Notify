using System.Collections.Generic;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;


namespace Api
{
    public class TemplateHandler
    {
        public APIGatewayHttpApiV2ProxyResponse CreateTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Create Templates for Message - {request.RequestContext.Time}.",
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

        public APIGatewayHttpApiV2ProxyResponse ListTemplates(APIGatewayHttpApiV2ProxyRequest request)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"List Templates for Message - {request.RequestContext.Time}.",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
