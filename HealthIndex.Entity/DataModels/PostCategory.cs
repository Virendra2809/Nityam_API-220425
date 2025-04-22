using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PostCategory
    {
        public PostCategory()
        {
            PostDetails = new HashSet<PostDetail>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostDetail> PostDetails { get; set; }
    }
}
