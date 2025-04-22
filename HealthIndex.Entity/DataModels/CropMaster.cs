using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropMaster
    {
        public CropMaster()
        {
            CropCultivationProcessDetails = new HashSet<CropCultivationProcessDetail>();
            CropDiseaseDetails = new HashSet<CropDiseaseDetail>();
            CropDiseaseMasters = new HashSet<CropDiseaseMaster>();
            CropInsectDetails = new HashSet<CropInsectDetail>();
            CropInsectMasters = new HashSet<CropInsectMaster>();
            CropIrrigationDetails = new HashSet<CropIrrigationDetail>();
            CropNotifications = new HashSet<CropNotification>();
            CropStageMasters = new HashSet<CropStageMaster>();
            ExpensesDetails = new HashSet<ExpensesDetail>();
            FarmerSelectedCrops = new HashSet<FarmerSelectedCrop>();
            FertCalculationFormulas = new HashSet<FertCalculationFormula>();
            NutrientDeficencyDetails = new HashSet<NutrientDeficencyDetail>();
            NutrientDetails = new HashSet<NutrientDetail>();
            SeedCropMasters = new HashSet<SeedCropMaster>();
            WeedMasters = new HashSet<WeedMaster>();
        }

        public int CropId { get; set; }
        public int? CropTypeId { get; set; }
        public string CropName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public string CropImage { get; set; }
        public int? CropDurMinDays { get; set; }
        public int? CropDurMaxDays { get; set; }

        public virtual CropTypeMaster CropType { get; set; }
        public virtual ICollection<CropCultivationProcessDetail> CropCultivationProcessDetails { get; set; }
        public virtual ICollection<CropDiseaseDetail> CropDiseaseDetails { get; set; }
        public virtual ICollection<CropDiseaseMaster> CropDiseaseMasters { get; set; }
        public virtual ICollection<CropInsectDetail> CropInsectDetails { get; set; }
        public virtual ICollection<CropInsectMaster> CropInsectMasters { get; set; }
        public virtual ICollection<CropIrrigationDetail> CropIrrigationDetails { get; set; }
        public virtual ICollection<CropNotification> CropNotifications { get; set; }
        public virtual ICollection<CropStageMaster> CropStageMasters { get; set; }
        public virtual ICollection<ExpensesDetail> ExpensesDetails { get; set; }
        public virtual ICollection<FarmerSelectedCrop> FarmerSelectedCrops { get; set; }
        public virtual ICollection<FertCalculationFormula> FertCalculationFormulas { get; set; }
        public virtual ICollection<NutrientDeficencyDetail> NutrientDeficencyDetails { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
        public virtual ICollection<SeedCropMaster> SeedCropMasters { get; set; }
        public virtual ICollection<WeedMaster> WeedMasters { get; set; }
    }
}
