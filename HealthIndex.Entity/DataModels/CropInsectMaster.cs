using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropInsectMaster
    {
        public CropInsectMaster()
        {
            CropInsectDetails = new HashSet<CropInsectDetail>();
            CropInsectManagements = new HashSet<CropInsectManagement>();
            CropInsectSymptoms = new HashSet<CropInsectSymptom>();
            InsectImages = new HashSet<InsectImage>();
        }

        public int CropInsectId { get; set; }
        public string CropInsectName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string TechnicalName { get; set; }
        public string Symptoms { get; set; }
        public string Identification { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual ICollection<CropInsectDetail> CropInsectDetails { get; set; }
        public virtual ICollection<CropInsectManagement> CropInsectManagements { get; set; }
        public virtual ICollection<CropInsectSymptom> CropInsectSymptoms { get; set; }
        public virtual ICollection<InsectImage> InsectImages { get; set; }
    }
}
