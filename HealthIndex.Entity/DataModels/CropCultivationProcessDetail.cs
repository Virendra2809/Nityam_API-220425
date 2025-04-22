using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropCultivationProcessDetail
    {
        public int CropCultivationProcessDetailId { get; set; }
        public int CropCultivationProcessId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual CropCultivationProcess CropCultivationProcess { get; set; }
    }
}
