using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class WeedMgmtMethod
    {
        public int WeedMgmtMethodId { get; set; }
        public int? WeedMgmtCategoryId { get; set; }
        public string WeedMgmtMethodName { get; set; }
        public string MethodImageName { get; set; }
        public string ImageUrl { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual WeedManagementCategory WeedMgmtCategory { get; set; }
    }
}
