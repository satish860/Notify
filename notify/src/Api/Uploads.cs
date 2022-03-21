using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Api
{
    public class Uploads
    {
        public APIGatewayHttpApiV2ProxyResponse GetUploadUrl(APIGatewayHttpApiV2ProxyRequest request)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = $"Getting the Upload URL for the Bucket { Environment.GetEnvironmentVariable("BUCKET_NAME")}",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
