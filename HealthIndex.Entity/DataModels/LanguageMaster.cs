using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class LanguageMaster
    {
        public LanguageMaster()
        {
            FarmerMasters = new HashSet<FarmerMaster>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<FarmerMaster> FarmerMasters { get; set; }
    }
}
