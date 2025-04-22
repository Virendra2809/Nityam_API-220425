using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedSubCategoryMaster
    {
        public SeedSubCategoryMaster()
        {
            SeedCropMasters = new HashSet<SeedCropMaster>();
        }

        public int SeedSubCategoryId { get; set; }
        public int? SeedCategoryId { get; set; }
        public string SeedSubCategoryName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool? HaschildCategory { get; set; }

        public virtual ICollection<SeedCropMaster> SeedCropMasters { get; set; }
    }
}
