using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedCropSpacing
    {
        public int SeedCropSpacingId { get; set; }
        public int? SeedCropId { get; set; }
        public decimal? RowSpacing { get; set; }
        public decimal? PlantSpacing { get; set; }
        public decimal? PlantPopulatin { get; set; }

        public virtual SeedCropMaster SeedCrop { get; set; }
    }
}
