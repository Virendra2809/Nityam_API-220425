using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class FeedbackReply
    {
        public int FeedbackReplyId { get; set; }
        public int? FeedbackId { get; set; }
        public string ReplyComment { get; set; }
        public DateTime? ReplyDate { get; set; }
        public int? EnteredBy { get; set; }

        public virtual FeedbackDetail Feedback { get; set; }
    }
}
