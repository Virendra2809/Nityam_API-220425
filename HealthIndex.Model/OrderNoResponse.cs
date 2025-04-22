using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class OrderNoResponse
    {     
        public long? OrderNo { get; set; }
        public string status { get; set; }
        public string Amount { get; set; }
        public int? Validity { get; set; }
        public string Total { get; set; }
        public string paymentThrough { get; set; }
        public string PlanName { get; set; }
        public string RequestData { get; set; }
        public string ExpiryDate { get; set; }

    }

}
