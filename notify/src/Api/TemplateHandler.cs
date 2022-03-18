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
    }
}
