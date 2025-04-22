using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropDiseaseSymptom
    {
        public int CropDiseaseSymptomId { get; set; }
        public int? CropDiseaseId { get; set; }
        public string Symptom { get; set; }

        public virtual CropDiseaseMaster CropDisease { get; set; }
    }
}
