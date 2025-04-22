using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedProductMaster
    {
        public int SeedProductId { get; set; }
        public int? SeedCropId { get; set; }
        public int? SeedBrandId { get; set; }
        public double? SeedQuantity { get; set; }
        public decimal? Amount { get; set; }
        public int? UnitId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? SequenceNo { get; set; }
        public int? ProductCategoryId { get; set; }

        public virtual ProductCategoryMaster ProductCategory { get; set; }
        public virtual SeedBrandMaster SeedBrand { get; set; }
        public virtual SeedCropMaster SeedCrop { get; set; }
        public virtual UnitofMeasurementMaster Unit { get; set; }
    }
}
