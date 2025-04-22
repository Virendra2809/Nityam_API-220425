using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropTypeMaster
    {
        public CropTypeMaster()
        {
            CropMasters = new HashSet<CropMaster>();
        }

        public int CropTypeId { get; set; }
        public string CropTypeName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<CropMaster> CropMasters { get; set; }
    }
}
