using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientMaster
    {
        public NutrientMaster()
        {
            NutrientDeficencyDetails = new HashSet<NutrientDeficencyDetail>();
            NutrientDetails = new HashSet<NutrientDetail>();
        }

        public int NutrientId { get; set; }
        public int? NutrientsCategoryId { get; set; }
        public string NutrientsName { get; set; }
        public string Alias { get; set; }
        public bool? Deletestatus { get; set; }
        public decimal? Percentage { get; set; }

        public virtual NutrientsCategoryMaster NutrientsCategory { get; set; }
        public virtual ICollection<NutrientDeficencyDetail> NutrientDeficencyDetails { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
    }
}
