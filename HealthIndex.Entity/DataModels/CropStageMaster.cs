using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropStageMaster
    {
        public CropStageMaster()
        {
            CropIrrigationDetails = new HashSet<CropIrrigationDetail>();
        }

        public int CropStageId { get; set; }
        public string CropStageName { get; set; }
        public int? CropId { get; set; }
        public int? SeqNo { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual ICollection<CropIrrigationDetail> CropIrrigationDetails { get; set; }
    }
}
