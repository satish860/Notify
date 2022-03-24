using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class Notification
    {
        public string Message { get; set; }

        public string MessageTemplateId { get; set; }

        public string RecipientListFile { get; set; }

        public string UserId { get; set; }

        public string NotificationId { get; set; }
    }
}
