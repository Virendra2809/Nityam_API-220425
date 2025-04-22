using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class WeedManagementCategory
    {
        public WeedManagementCategory()
        {
            WeedMgmtMethods = new HashSet<WeedMgmtMethod>();
        }

        public int WeedMgmtCategoryId { get; set; }
        public string WeedMgmtCategoryName { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<WeedMgmtMethod> WeedMgmtMethods { get; set; }
    }
}
