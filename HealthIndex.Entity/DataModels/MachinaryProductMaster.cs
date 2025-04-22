using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class MachinaryProductMaster
    {
        public int MachinaryProductId { get; set; }
        public string MachinaryProductName { get; set; }
        public int? MachinaryBrandId { get; set; }
        public int? MachinaryCategoryId { get; set; }
        public string ModelName { get; set; }
        public int? SequenceNo { get; set; }
        public decimal? ActualPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? ProductCategoryId { get; set; }

        public virtual MachinaryBrandMaster MachinaryBrand { get; set; }
        public virtual MachinaryCategoryMaster MachinaryCategory { get; set; }
        public virtual ProductCategoryMaster ProductCategory { get; set; }
    }
}
