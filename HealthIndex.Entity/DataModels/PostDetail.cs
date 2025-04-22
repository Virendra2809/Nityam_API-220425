using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PostDetail
    {
        public PostDetail()
        {
            PostCommentDetails = new HashSet<PostCommentDetail>();
            PostImges = new HashSet<PostImge>();
            PostLikeDetails = new HashSet<PostLikeDetail>();
        }

        public int PostId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? PostDate { get; set; }
        public string PostHeading { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public string PostDetail1 { get; set; }
        public string PostUrl { get; set; }

        public virtual PostCategory Category { get; set; }
        public virtual ICollection<PostCommentDetail> PostCommentDetails { get; set; }
        public virtual ICollection<PostImge> PostImges { get; set; }
        public virtual ICollection<PostLikeDetail> PostLikeDetails { get; set; }
    }
}
