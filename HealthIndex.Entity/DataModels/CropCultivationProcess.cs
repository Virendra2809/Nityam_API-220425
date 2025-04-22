using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropCultivationProcess
    {
        public CropCultivationProcess()
        {
            CropCultivationProcessDetails = new HashSet<CropCultivationProcessDetail>();
        }

        public int CropCultivationProcessId { get; set; }
        public string CropCultivationProcessName { get; set; }
        public string Description { get; set; }
        public int SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<CropCultivationProcessDetail> CropCultivationProcessDetails { get; set; }
    }
}
