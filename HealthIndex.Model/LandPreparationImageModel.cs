using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class LandPreparationImageModel
    {

        public int LandPreparationImageId { get; set; }
        public int? LandPreparationId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

    }
}
