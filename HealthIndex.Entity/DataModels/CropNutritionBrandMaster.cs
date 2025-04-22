using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropNutritionBrandMaster
    {
        public CropNutritionBrandMaster()
        {
            CropNutritionProductMasters = new HashSet<CropNutritionProductMaster>();
        }

        public int CropNutritionBrandId { get; set; }
        public string CropNutritionBrandName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<CropNutritionProductMaster> CropNutritionProductMasters { get; set; }
    }
}
