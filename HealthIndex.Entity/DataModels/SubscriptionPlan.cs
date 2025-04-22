using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class SubscriptionPlan
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Amount { get; set; }
        public bool? Status { get; set; }
        public string OfferCode { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? Validity { get; set; }
        public string PaymentEncrData { get; set; }
    }
}
