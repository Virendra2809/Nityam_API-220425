using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class FeedbackReplyModel
    {
        public int FeedbackReplyId { get; set; }
        public int? FeedbackId { get; set; }
        public string ReplyComment { get; set; }
        public DateTime? ReplyDate { get; set; }
        public int? EnteredBy { get; set; }
    }
}
