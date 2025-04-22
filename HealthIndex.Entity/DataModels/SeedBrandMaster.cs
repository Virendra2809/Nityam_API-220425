using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedBrandMaster
    {
        public SeedBrandMaster()
        {
            SeedProductMasters = new HashSet<SeedProductMaster>();
        }

        public int SeedBrandId { get; set; }
        public string SeedBrandName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<SeedProductMaster> SeedProductMasters { get; set; }
    }
}
