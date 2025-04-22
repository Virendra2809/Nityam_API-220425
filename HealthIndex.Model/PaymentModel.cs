using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class PaymentModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }

        public string Purpose { get; set; }

        
        //public string RedirectUrl { get; set; }
        //public string WebhookUrl { get; set; }
    }

}
