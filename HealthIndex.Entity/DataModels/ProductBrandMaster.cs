using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductBrandMaster
    {
        public ProductBrandMaster()
        {
            Products = new HashSet<Product>();
        }

        public int ProductBrandId { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductBrandName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ProductCategoryMaster ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
