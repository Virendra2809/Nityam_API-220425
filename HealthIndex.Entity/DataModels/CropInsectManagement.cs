using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropInsectManagement
    {
        public int CropInsectManagementId { get; set; }
        public int? CropInsectId { get; set; }
        public string CropInsectManagement1 { get; set; }

        public virtual CropInsectMaster CropInsect { get; set; }
    }
}
