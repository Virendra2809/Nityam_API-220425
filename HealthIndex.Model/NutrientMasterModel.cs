using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class NutrientMasterModel
    {
        public int NutrientId { get; set; }
        public int? NutrientsCategoryId { get; set; }
        public string NutrientsCategoryName { get; set; }
        public string NutrientsName { get; set; }
        public string Alias { get; set; }
        public bool? Deletestatus { get; set; }
        public decimal? Percentage { get; set; }

    }
}
