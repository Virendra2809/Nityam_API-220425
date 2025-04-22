using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class NutrientDeficencyImageModel
    {
        public int NutrientDeficencyImageId { get; set; }
        public int? NutrientDeficencyId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

    }
}
