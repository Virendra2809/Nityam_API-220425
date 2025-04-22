using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class FeedbackDetail
    {
        public FeedbackDetail()
        {
            FeedbackReplies = new HashSet<FeedbackReply>();
        }

        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public decimal? Rating { get; set; }
        public string Comments { get; set; }
        public DateTime? Feedbackdate { get; set; }

        public virtual AppUserMaster User { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
    }
}
