using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CropNutritionProductMasterModel
    {
        public int CropNutritionProductId { get; set; }
        public string CropNutritionProductName { get; set; }
        public int? CropNutritionBrandId { get; set; }
        public int? ProductCategoryId { get; set; }
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
        public string ProductCategoryName { get; set; }
        public string CropNutritionBrandName { get; set; }
        public string UnitName { get; set; }

    }
}
