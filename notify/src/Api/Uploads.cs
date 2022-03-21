using Amazon;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Api
{
    public class Uploads
    {
        private readonly AmazonS3Client s3Client;
        private readonly string bucketName;

        public Uploads()
        {
            var region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"));
            s3Client = new AmazonS3Client(region);
            bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME");
        }

        public APIGatewayHttpApiV2ProxyResponse GetUploadUrl(APIGatewayHttpApiV2ProxyRequest request)
        {
            var serializer = JsonSerializer.Deserialize<UploadFile>(request.Body);
            GetPreSignedUrlRequest preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = serializer.FileName,
                Expires = DateTime.UtcNow.AddHours(24)
            };
            string url = s3Client.GetPreSignedURL(preSignedUrlRequest);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.Created,
                Body = url,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }

    public class UploadFile
    {
        public string FileName { get; set; }

        public string Filetype { get; set; }
    }
}
