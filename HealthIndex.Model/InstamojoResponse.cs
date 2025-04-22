using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class InstamojoResponse
    {
        public string id { get; set; }
        public string user { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string buyer_name { get; set; }
        public string amount { get; set; }
        public string purpose { get; set; }
        public string status { get; set; }
       // public string payments { get; set; }
        public string send_sms { get; set; }
        public string send_email { get; set; }
        public string sms_status { get; set; }
        public string email_status { get; set; }
        public string shorturl { get; set; }
        public string longurl { get; set; }
        public string redirect_url { get; set; }
        public string webhook { get; set; }
        public string scheduled_at { get; set; }
        public string expires_at { get; set; }
        public string allow_repeated_payments { get; set; }
        public string mark_fulfilled { get; set; }
        public string created_at { get; set; }
        public string modified_at { get; set; }
        public string resource_uri { get; set; }


    }

}
