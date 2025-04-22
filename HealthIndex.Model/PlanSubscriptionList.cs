using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class PlanSubscription
    {
    public  List<PlanSubscriptionList> planSubscriptionLists { get; set; }


    }

    public class PlanSubscriptionList
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public int? Validity { get; set; }
        public string Code { get; set; }
        public string Amount { get; set; }
        public string OfferCode { get; set; }


    }



}
