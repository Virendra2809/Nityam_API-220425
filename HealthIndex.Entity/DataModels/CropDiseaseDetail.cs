using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropDiseaseDetail
    {
        public int CropDiseaseDetailId { get; set; }
        public int? CropId { get; set; }
        public int? CropDiseaseId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual CropDiseaseMaster CropDisease { get; set; }
    }
}
