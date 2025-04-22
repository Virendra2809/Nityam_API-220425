using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropCultivationProcessModel
    {

        public int CropCultivationProcessId { get; set; }
        public string CropCultivationProcessName { get; set; }
        public string? Description { get; set; }
        public int SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

    }
}
