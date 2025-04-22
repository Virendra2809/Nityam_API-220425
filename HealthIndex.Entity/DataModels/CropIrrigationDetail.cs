using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropIrrigationDetail
    {
        public int IrrigationDetailId { get; set; }
        public int? SoilTypeId { get; set; }
        public int? CropId { get; set; }
        public int? CropStageId { get; set; }
        public int? IrrigationDay { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual CropStageMaster CropStage { get; set; }
        public virtual SoilTypeMaster SoilType { get; set; }
    }
}
