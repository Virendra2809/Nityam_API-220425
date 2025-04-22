using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class WeedMaster
    {
        public WeedMaster()
        {
            WeedDetails = new HashSet<WeedDetail>();
            WeedImages = new HashSet<WeedImage>();
        }

        public int WeedId { get; set; }
        public string WeedName { get; set; }
        public string TechnicalName { get; set; }
        public string Identification { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual ICollection<WeedDetail> WeedDetails { get; set; }
        public virtual ICollection<WeedImage> WeedImages { get; set; }
    }
}
