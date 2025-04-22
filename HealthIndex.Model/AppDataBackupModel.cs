using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class AppDataBackupModel
    {
        public int? DataBackupId { get; set; }
        [Required]
        public int? AppUserId { get; set; }
        public DateTime? BackupDate { get; set; }
        public string FileName { get; set; }

        public string BackupPath { get; set; }
        public string UplodedFile { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsRestored { get; set; }

        public DateTime? RestoreDate { get; set; }

    }

    public class RestoreData
    {
        [Required]
        public int? AppUserId { get; set; }
        public bool? IsRestored { get; set; }

        public DateTime? RestoreDate { get; set; }
    }


    public class RestoreFileData
    {
        [Required]
        public int? AppUserId { get; set; }
        public string FileName { get; set; }

    }
}
