using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropDiseaseManagement
    {
        public int CropDiseaseManagementId { get; set; }
        public int? CropDiseaseId { get; set; }
        public string CropDiseaseManagement1 { get; set; }
        public int? SrNo { get; set; }

        public virtual CropDiseaseMaster CropDisease { get; set; }
    }
}
