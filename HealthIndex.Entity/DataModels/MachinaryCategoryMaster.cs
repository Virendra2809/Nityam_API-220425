using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class MachinaryCategoryMaster
    {
        public MachinaryCategoryMaster()
        {
            MachinaryProductMasters = new HashSet<MachinaryProductMaster>();
        }

        public int MachinaryCategoryId { get; set; }
        public string MachinaryCategoryName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<MachinaryProductMaster> MachinaryProductMasters { get; set; }
    }
}
