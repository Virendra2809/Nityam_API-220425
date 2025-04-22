using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class MessageModel
    {
        public string message { get; set; }
        public string OtpforLogin { get; set; }
        public int AppUserId { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int StatusCode { get; set; }

    }

    public class Message
    {
        public string message { get; set; }

    }


    public class ResponseMessage
    {
        public string message { get; set; }

    }
}
