using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class Notification
    {
        [DynamoDBProperty("message")]
        public string Message { get; set; }

        [DynamoDBProperty("message_template_id")]
        public string MessageTemplateId { get; set; }

        [DynamoDBProperty("recipent_list_file")]
        public string RecipientListFile { get; set; }

        [DynamoDBProperty("recipent")]
        public string Recipent { get; set; }

        [DynamoDBHashKey("user_Id")]
        public string UserId { get; set; }

        [DynamoDBRangeKey("notification_Id")]
        public string NotificationId { get; set; }
    }
}
