using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CityMaster
    {
        public CityMaster()
        {
            MandiMasters = new HashSet<MandiMaster>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual StateMaster State { get; set; }
        public virtual ICollection<MandiMaster> MandiMasters { get; set; }
    }
}
