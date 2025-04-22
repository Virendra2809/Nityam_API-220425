using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ModuleMaster
    {
        public ModuleMaster()
        {
            MenuMasters = new HashSet<MenuMaster>();
        }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleMarathiName { get; set; }
        public string ModuleIcon { get; set; }
        public string ModuleAreaName { get; set; }
        public int Seqno { get; set; }
        public bool IsDirectNode { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string ModuleUrl { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<MenuMaster> MenuMasters { get; set; }
    }
}
