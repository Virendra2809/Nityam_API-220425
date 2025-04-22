using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PostLikeDetail
    {
        public int PostLikeId { get; set; }
        public int? PostId { get; set; }
        public int? LikedBy { get; set; }
        public DateTime? LikedDate { get; set; }

        public virtual PostDetail Post { get; set; }
    }
}
