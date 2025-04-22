using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientDeficencyImage
    {
        public int NutrientDeficencyImageId { get; set; }
        public int? NutrientDeficencyId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        public virtual NutrientDeficencyDetail NutrientDeficency { get; set; }
    }
}
