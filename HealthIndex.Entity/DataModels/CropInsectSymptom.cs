using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropInsectSymptom
    {
        public int CropInsectSymptomId { get; set; }
        public int? CropInsectId { get; set; }
        public string Symptom { get; set; }

        public virtual CropInsectMaster CropInsect { get; set; }
    }
}
