using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropInsectDetail
    {
        public int CropInsectDetailId { get; set; }
        public int? CropId { get; set; }
        public int? CropInsectId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual CropInsectMaster CropInsect { get; set; }
    }
}
