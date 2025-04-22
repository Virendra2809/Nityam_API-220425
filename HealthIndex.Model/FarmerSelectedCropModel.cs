using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class FarmerSelectedCropModel
    {
        public int FarmerSelectedCropId { get; set; }
        public int FarmerId { get; set; }
        public int[] FarmerCropId { get; set; }
        public int FarmerCropId1 { get; set; }
        public bool? DeleteStatus { get; set; }
        public string CropImage { get; set; }

        public string CropName { get; set; }

    }
}
