using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropProtectionSubCategoryMaster
    {
        public int CropProtectionSubCategoryId { get; set; }
        public int? CropProtectionCategoryId { get; set; }
        public string CropProtectionSubCategoryName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual CropProtectionCategoryMaster CropProtectionCategory { get; set; }
    }
}
