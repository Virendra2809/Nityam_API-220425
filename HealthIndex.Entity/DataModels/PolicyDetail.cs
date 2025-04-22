using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class PolicyDetail
    {
        public int PolicyId { get; set; }
        public int? PolicyCategoryId { get; set; }
        public string PolicyTitle { get; set; }
        public string Description { get; set; }
        public string PolicyUrl { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SeqNo { get; set; }

        public virtual PolicyCategoryMaster PolicyCategory { get; set; }
    }
}
