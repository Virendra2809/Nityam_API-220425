using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class SubscriptionDataModel
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Plan { get; set; }
        public string OrderStatus { get; set; }
        public long? OrderId { get; set; }

        public long SubscriptionOrderId { get; set; }
        public long? AppUserId { get; set; }
        public string Amount { get; set; }
        public DateTime? Date { get; set; }

    }

 }
