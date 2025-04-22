using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class StateMaster
    {
        public StateMaster()
        {
            CityMasters = new HashSet<CityMaster>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<CityMaster> CityMasters { get; set; }
    }
}
