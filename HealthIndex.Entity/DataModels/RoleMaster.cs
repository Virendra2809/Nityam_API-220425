using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class RoleMaster
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
