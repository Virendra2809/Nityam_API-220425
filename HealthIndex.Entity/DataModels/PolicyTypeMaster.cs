using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PolicyTypeMaster
    {
        public PolicyTypeMaster()
        {
            PolicyCategoryMasters = new HashSet<PolicyCategoryMaster>();
        }

        public int PolicyTypeId { get; set; }
        public string PolicyTypeName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<PolicyCategoryMaster> PolicyCategoryMasters { get; set; }
    }
}
