using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SeedProductMasterModel
    {
        public int SeedProductId { get; set; }
        public int? SeedBrandId { get; set; }
        public int? SeedCropId { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public float? SeedQuantity { get; set; }
        public decimal? Amount { get; set; }
        public int? UnitId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? SequenceNo { get; set; }
        public string SeedBrandName { get; set; }
        public string SeedCropName { get; set; }
        public string UnitName { get; set; }

    }
}
