using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedCropMaster
    {
        public SeedCropMaster()
        {
            FarmerMasters = new HashSet<FarmerMaster>();
            SeedCropSpacings = new HashSet<SeedCropSpacing>();
            SeedProductMasters = new HashSet<SeedProductMaster>();
        }

        public int SeedCropId { get; set; }
        public int? SeedSubCategoryId { get; set; }
        public string SeedCropName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual SeedSubCategoryMaster SeedSubCategory { get; set; }
        public virtual ICollection<FarmerMaster> FarmerMasters { get; set; }
        public virtual ICollection<SeedCropSpacing> SeedCropSpacings { get; set; }
        public virtual ICollection<SeedProductMaster> SeedProductMasters { get; set; }
    }
}
