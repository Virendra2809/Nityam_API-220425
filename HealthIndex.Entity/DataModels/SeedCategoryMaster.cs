using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeedCategoryMaster
    {
        public int SeedCategoryId { get; set; }
        public string SeedCategoryName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
