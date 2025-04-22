using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropDiseaseImage
    {
        public int CropDiseaseImageId { get; set; }
        public int? CropDiseaseId { get; set; }
        public string ImageName { get; set; }
        public string DiseaseImage { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? IsActive { get; set; }

        public virtual CropDiseaseMaster CropDisease { get; set; }
    }
}
