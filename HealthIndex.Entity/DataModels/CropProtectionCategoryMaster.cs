using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropProtectionCategoryMaster
    {
        public CropProtectionCategoryMaster()
        {
            CropProtectionSubCategoryMasters = new HashSet<CropProtectionSubCategoryMaster>();
        }

        public int CropProtectionCategoryId { get; set; }
        public string CropProtectionCategoryName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<CropProtectionSubCategoryMaster> CropProtectionSubCategoryMasters { get; set; }
    }
}
