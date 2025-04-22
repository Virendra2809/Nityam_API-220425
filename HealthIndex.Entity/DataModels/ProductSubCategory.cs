using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductSubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public string SubCategoryDescription { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}
