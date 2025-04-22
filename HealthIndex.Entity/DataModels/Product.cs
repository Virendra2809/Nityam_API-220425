using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? ProductBrandId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? UnitId { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Igst { get; set; }
        public string ProductDetails { get; set; }
        public string ProductSpecification { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public decimal? Rating { get; set; }

        public virtual ProductBrandMaster ProductBrand { get; set; }
        public virtual ProductCategoryMaster ProductCategory { get; set; }
        public virtual UnitofMeasurementMaster Unit { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
