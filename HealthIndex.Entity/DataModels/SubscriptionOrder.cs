using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class SubscriptionOrder
    {
        public long SubscriptionOrderId { get; set; }
        public long? AppUserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? PlanId { get; set; }
        public int? ValidityDays { get; set; }
        public decimal? Amount { get; set; }
        public string TransactionToken { get; set; }
        public string ModeOfPayment { get; set; }
        public string TransactionReferanceNo { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentLink { get; set; }
        public string PaymentId { get; set; }
        public string PaymentEncrData { get; set; }
        public long? OrderNo { get; set; }
    }
}
