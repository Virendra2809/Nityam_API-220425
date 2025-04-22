using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductSubCategories = new HashSet<ProductSubCategory>();
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
