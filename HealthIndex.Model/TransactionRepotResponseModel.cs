using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class TransactionRepotResponseModel
    {
    //    public long SubscriptionOrderId { get; set; }
        public long? AppUserId { get; set; }
        public DateTime? OrderDate { get; set; }     
        public string PlanCode { get; set; }
        public int? ValidityDays { get; set; }
        public decimal? Amount { get; set; }
        public string TransactionToken { get; set; }
        public long? OrderNo { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string ModeOfPayment { get; set; }
        public string OrderStatus { get; set; }

    }

}
