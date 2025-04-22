using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropProtectionProductMaster
    {
        public int CropProtectionProductId { get; set; }
        public int? CropProtectionBrandId { get; set; }
        public string CropProtectionProductName { get; set; }
        public double? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public int? UnitId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? SequenceNo { get; set; }
        public int? CropDiseaseId { get; set; }
        public int? CropInsectId { get; set; }
        public int? ProductCategoryId { get; set; }

        public virtual ProductCategoryMaster ProductCategory { get; set; }
    }
}
