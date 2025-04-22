using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class FarmerSelectedCrop
    {
        public int FarmerSelectedCropId { get; set; }
        public int? FarmerId { get; set; }
        public int? FarmerCropId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual FarmerMaster Farmer { get; set; }
        public virtual CropMaster FarmerCrop { get; set; }
    }
}
