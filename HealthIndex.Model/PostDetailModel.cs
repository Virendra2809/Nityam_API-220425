using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class PostDetailModel
    {
        public PostDetailModel()
        {
            this.PostImages = new List<PostImageModel>();
            this.Images = new List<PostImageForMobileModel>();

        }

        public int PostId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? PostDate { get; set; }
        public string PostHeading { get; set; }
        public string PostDetail1 { get; set; }

        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public decimal CommentCount { get; set; }
        public decimal Likes { get; set; }
        public string PostUrl { get; set; }

        public List<PostImageModel> PostImages { get; set; }
        public List<PostImageForMobileModel> Images { get; set; }



    }
}
