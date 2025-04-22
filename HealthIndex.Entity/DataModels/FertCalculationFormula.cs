using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class FertCalculationFormula
    {
        public int FormulaId { get; set; }
        public int CropId { get; set; }
        public int SoilTypeId { get; set; }
        public decimal ConstantForN1 { get; set; }
        public decimal ConstantForN2 { get; set; }
        public decimal PercentageOfNinUrea { get; set; }
        public decimal ConstantForP1 { get; set; }
        public decimal ConstantForP2 { get; set; }
        public decimal PercentageOfPinUrea { get; set; }
        public decimal ConstantForK1 { get; set; }
        public decimal ConstantForK2 { get; set; }
        public decimal PercentageOfKinUrea { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual SoilTypeMaster SoilType { get; set; }
    }
}
