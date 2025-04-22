using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class WeedImageModel
    {
        public int WeedImageId { get; set; }
        public int? WeedId { get; set; }
        public string WeedImageName { get; set; }
        public string WeedImageUrl { get; set; }

    }
}
