using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class Template
    {
        [DynamoDBHashKey("user_Id")]
        public string UserId { get; set; }

        [DynamoDBRangeKey("template_id")]
        public string TemplateId { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("text")]
        public string Text { get; set; }
    }
}
