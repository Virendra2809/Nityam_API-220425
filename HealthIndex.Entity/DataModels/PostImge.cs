using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PostImge
    {
        public int ImageId { get; set; }
        public int? PostId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }

        public virtual PostDetail Post { get; set; }
    }
}
