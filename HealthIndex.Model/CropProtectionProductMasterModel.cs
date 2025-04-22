using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropProtectionProductMasterModel
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
        public string CropProtectionBrandName { get; set; }
        public string CropDiseaseName { get; set; }
        public string CropInsectName { get; set; }
        public string UnitName { get; set; }


    }
}
