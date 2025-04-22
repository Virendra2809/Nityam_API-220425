using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientsCategoryMaster
    {
        public NutrientsCategoryMaster()
        {
            NutrientMasters = new HashSet<NutrientMaster>();
        }

        public int NutrientsCategoryId { get; set; }
        public string NutrientsCategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<NutrientMaster> NutrientMasters { get; set; }
    }
}
