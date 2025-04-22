using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CropProtectionBrandMasterModel
    {
        public int CropProtectionBrandId { get; set; }
        public string CropProtectionBrandName { get; set; }
        public string Description { get; set; }
        public int SeqNo{ get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate{ get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate{ get; set; }
        public bool DeleteStatus{ get; set; }

    }
}
