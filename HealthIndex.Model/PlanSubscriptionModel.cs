using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class PlanSubscriptionModel
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public int? Validity { get; set; }
        public string Code { get; set; }
        public string Amount { get; set; }
        public bool? Status { get; set; }
        public string OfferCode { get; set; }
        public int? CreatedBy { get; set; }

    }

}
