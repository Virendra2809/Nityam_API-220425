using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PostCommentDetail
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public int? FarmerId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string CommentDetails { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }

        public virtual FarmerMaster Farmer { get; set; }
        public virtual PostDetail Post { get; set; }
    }
}
