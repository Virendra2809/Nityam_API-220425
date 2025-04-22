using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CropProtectionSubCategoryMasterModel
    {
        public int CropProtectionSubCategoryId { get; set; }
        public long? CropProtectionCategoryId { get; set; }
        public string CropProtectionCategoryName { get; set;}
        public string CropProtectionSubCategoryName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

    }
}
