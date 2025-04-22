using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class AppDataBackup
    {
        public int DataBackupId { get; set; }
        public int? AppUserId { get; set; }
        public DateTime? BackupDate { get; set; }
        public string BackupPath { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRestored { get; set; }
        public DateTime? RestoreDate { get; set; }

        public virtual AppUserMaster AppUser { get; set; }
    }
}
