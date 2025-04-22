using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductCategoryMaster
    {
        public ProductCategoryMaster()
        {
            CropProtectionProductMasters = new HashSet<CropProtectionProductMaster>();
            MachinaryProductMasters = new HashSet<MachinaryProductMaster>();
            ProductBrandMasters = new HashSet<ProductBrandMaster>();
            Products = new HashSet<Product>();
            SeedProductMasters = new HashSet<SeedProductMaster>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<CropProtectionProductMaster> CropProtectionProductMasters { get; set; }
        public virtual ICollection<MachinaryProductMaster> MachinaryProductMasters { get; set; }
        public virtual ICollection<ProductBrandMaster> ProductBrandMasters { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<SeedProductMaster> SeedProductMasters { get; set; }
    }
}
