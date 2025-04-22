using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SeedCropMasterModel
    {
        public int SeedCropId { get; set; }
        public int? SeedSubCategoryId { get; set; }
        public string SeedSubCategoryName { get; set; }
        public string SeedCropName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CropId { get; set; }


        public string CropName { get; set; }

    }
}
