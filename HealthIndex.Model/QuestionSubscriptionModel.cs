using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class QuestionSubscriptionModel
    {
        public int QuestionSubscriptionId { get; set; }
        public int? AppUserId { get; set; }
        public int? QuestionMasterId { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
