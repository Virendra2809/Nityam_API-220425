using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PolicyCategoryMaster
    {
        public PolicyCategoryMaster()
        {
            PolicyDetails = new HashSet<PolicyDetail>();
        }

        public int PolicyCategoryId { get; set; }
        public string PolicyCategoryName { get; set; }
        public int? PolicyTypeId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual PolicyTypeMaster PolicyType { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
