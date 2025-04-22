using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class AgriProductPriceDetail
    {
        public int AgriProductPriceId { get; set; }
        public int? MandiId { get; set; }
        public int? AgriProductId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? MaxRate { get; set; }
        public decimal? MinRate { get; set; }
        public int? UnitId { get; set; }
        public decimal? AverageRate { get; set; }
        public double? Inward { get; set; }
        public double? Outward { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual AgriProductMaster AgriProduct { get; set; }
        public virtual MandiMaster Mandi { get; set; }
        public virtual UnitofMeasurementMaster Unit { get; set; }
    }
}
