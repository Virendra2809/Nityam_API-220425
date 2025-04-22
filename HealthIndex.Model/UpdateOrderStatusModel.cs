using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class UpdateOrderStatusModel
    {   
        public long AppUserId { get; set; }
        public long OrderId { get; set; }       
        public string ModeOfPayment { get; set; }   
  

    }

}
