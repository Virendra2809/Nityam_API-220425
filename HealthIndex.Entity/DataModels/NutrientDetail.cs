using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientDetail
    {
        public int NutrientDetailId { get; set; }
        public int CropId { get; set; }
        public int? SeasonId { get; set; }
        public int? SoilTypeId { get; set; }
        public int? NutrientId { get; set; }
        public decimal? QuantityInKg { get; set; }
        public int? UnitId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? Isactive { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual NutrientMaster Nutrient { get; set; }
        public virtual SeasonMaster Season { get; set; }
        public virtual SoilTypeMaster SoilType { get; set; }
        public virtual UnitofMeasurementMaster Unit { get; set; }
    }
}
