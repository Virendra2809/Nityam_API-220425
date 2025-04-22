using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class QuestionSubscription
    {
        public int QuestionSubscriptionId { get; set; }
        public int? AppUserId { get; set; }
        public int? QuestionMasterId { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual AppUserMaster AppUser { get; set; }
        public virtual QuestionMaster QuestionMaster { get; set; }
    }
}
