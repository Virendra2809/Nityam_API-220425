using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class FeedbackDetailModel
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public decimal? Rating { get; set; }
        public string Comments { get; set; }
        public DateTime? Feedbackdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }

    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public decimal? Rating { get; set; }

        public string Comments { get; set; }
        public DateTime? Feedbackdate { get; set; }
    }


    public class FeedbackListModel
    {
        public int FeedbackId { get; set; }
       public int? UserId { get; set; }
        public decimal? Rating { get; set; }
        public string Comments { get; set; }
        public DateTime? Feedbackdate { get; set; }
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }



    public class FeedbackListModel1
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public decimal? Rating { get; set; }
        public string Comments { get; set; }
        public string Feedbackdate { get; set; }
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }

    public class Response
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class FeedbackCountModel
    {
        public int FeedbackDetailCount { get; set; }
    }


    public class Feedbackcomment
    {
        public string Comments { get; set; }
    }

}
