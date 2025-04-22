using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class SeedCropSpacingModel
    {

        public int SeedCropSpacingId { get; set; }
        public int? SeedCropId { get; set; }
        public string SeedCropName { get; set; }
        public decimal? RowSpacing { get; set; }
        public decimal? PlantSpacing { get; set; }
        public decimal? PlantPopulatin { get; set; }
    }
}
