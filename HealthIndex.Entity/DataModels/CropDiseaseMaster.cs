using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropDiseaseMaster
    {
        public CropDiseaseMaster()
        {
            CropDiseaseDetails = new HashSet<CropDiseaseDetail>();
            CropDiseaseImages = new HashSet<CropDiseaseImage>();
            CropDiseaseManagements = new HashSet<CropDiseaseManagement>();
            CropDiseaseSymptoms = new HashSet<CropDiseaseSymptom>();
        }

        public int CropDiseaseId { get; set; }
        public string CropDiseaseName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string Symptoms { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual ICollection<CropDiseaseDetail> CropDiseaseDetails { get; set; }
        public virtual ICollection<CropDiseaseImage> CropDiseaseImages { get; set; }
        public virtual ICollection<CropDiseaseManagement> CropDiseaseManagements { get; set; }
        public virtual ICollection<CropDiseaseSymptom> CropDiseaseSymptoms { get; set; }
    }
}
