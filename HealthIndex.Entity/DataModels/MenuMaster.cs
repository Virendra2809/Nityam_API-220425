using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class MenuMaster
    {
        public MenuMaster()
        {
            RoleDetails = new HashSet<RoleDetail>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int MenuId { get; set; }
        public int ModuleId { get; set; }
        public string MenuName { get; set; }
        public string MenuNameMarathi { get; set; }
        public string MenuType { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuUrl { get; set; }
        public string Description { get; set; }
        public string MenuIcon { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool? IsLeaf { get; set; }
        public bool? ShowInMainMenu { get; set; }
        public int? SeqNo { get; set; }
        public string FirmIds { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ModuleMaster Module { get; set; }
        public virtual ICollection<RoleDetail> RoleDetails { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
