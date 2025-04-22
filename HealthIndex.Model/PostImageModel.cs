using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class PostImageModel
    {

        public int ImageId { get; set; }
        public int? PostId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
    }

    public class PostImageForMobileModel
    {
        public string Img { get; set; }
    }
}
