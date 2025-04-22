using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class LikeModel
    {
        public int PostLikeId { get; set; }
        public int? PostId { get; set; }
        public int? LikedBy { get; set; }
        public DateTime? LikedDate { get; set; }
    }
}
