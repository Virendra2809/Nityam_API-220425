using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class PostCommentDetailModel
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public int? FarmerId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string CommentDetails { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string PostHeading { get; set; }
        public string FarmerName { get; set; }
       

    }
}
