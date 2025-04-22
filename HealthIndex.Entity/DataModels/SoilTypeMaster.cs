using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SoilTypeMaster
    {
        public SoilTypeMaster()
        {
            CropIrrigationDetails = new HashSet<CropIrrigationDetail>();
            FarmerCropDetails = new HashSet<FarmerCropDetail>();
            FertCalculationFormulas = new HashSet<FertCalculationFormula>();
            NutrientDetails = new HashSet<NutrientDetail>();
        }

        public int SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<CropIrrigationDetail> CropIrrigationDetails { get; set; }
        public virtual ICollection<FarmerCropDetail> FarmerCropDetails { get; set; }
        public virtual ICollection<FertCalculationFormula> FertCalculationFormulas { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
    }
}
