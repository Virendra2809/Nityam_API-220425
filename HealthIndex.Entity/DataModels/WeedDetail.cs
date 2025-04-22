using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class WeedDetail
    {
        public int WeedDetailId { get; set; }
        public int? WeedId { get; set; }
        public string WeedManagement { get; set; }

        public virtual WeedMaster Weed { get; set; }
    }
}
