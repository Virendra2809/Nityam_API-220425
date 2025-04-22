using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class AgriProductPriceDetailModel
    {
        public int AgriProductPriceId { get; set; }
        public int? MandiId { get; set; }
        public int? AgriProductId { get; set; }
        public int? AgriProductTypeId { get; set; }

        public string MandiName { get; set; }
        public string AgriProductName { get; set; }
        public DateTime Date { get; set; }
        public decimal? MaxRate { get; set; }
        public decimal? MinRate { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? AverageRate { get; set; }
        public double? Inward { get; set; }
        public double? Outward { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public string AgriProductTypeName { get; set; }


    }
}
