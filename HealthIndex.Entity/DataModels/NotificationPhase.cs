using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NotificationPhase
    {
        public NotificationPhase()
        {
            CropNotifications = new HashSet<CropNotification>();
        }

        public int NotificationPhaseId { get; set; }
        public string NotificationPhaseName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<CropNotification> CropNotifications { get; set; }
    }
}
