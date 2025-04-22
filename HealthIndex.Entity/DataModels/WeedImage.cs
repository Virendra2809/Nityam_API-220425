using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class WeedImage
    {
        public int WeedImageId { get; set; }
        public int? WeedId { get; set; }
        public string WeedImageName { get; set; }
        public string WeedImageUrl { get; set; }

        public virtual WeedMaster Weed { get; set; }
    }
}
