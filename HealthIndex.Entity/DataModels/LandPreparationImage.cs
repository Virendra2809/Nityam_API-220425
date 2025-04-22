using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class LandPreparationImage
    {
        public int LandPreparationImageId { get; set; }
        public int? LandPreparationId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        public virtual LandPreparationDetail LandPreparation { get; set; }
    }
}
