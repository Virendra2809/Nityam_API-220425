using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class CropNotification
    {
        public int NotificationId { get; set; }
        public int CropId { get; set; }
        public int SeasonId { get; set; }
        public int? NotificationPhaseId { get; set; }
        public string Description { get; set; }
        public int? DaysToPush { get; set; }
       
        public string InformationUrl { get; set; }
        public string Image { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual NotificationPhase NotificationPhase { get; set; }
        public virtual SeasonMaster Season { get; set; }
    }
}
