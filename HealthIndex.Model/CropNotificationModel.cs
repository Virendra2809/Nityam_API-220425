using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropNotificationModel
    {
        public CropNotificationModel()
        {
            this.NotificationImages = new List<ImageModel>();
        }

        public int NotificationId { get; set; }
        public int CropId { get; set; }
        public int SeasonId { get; set; }
        public int? NotificationPhaseId { get; set; }
        public string NotificationPhaseName { get; set; }
        public string Description { get; set; }
        public int? DaysToPush { get; set; }
        public string InformationUrl { get; set; }
        public string Image { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }
        public string CropName { get; set; }
        public string SeasonName { get; set; }
        public List<ImageModel> NotificationImages { get; set; }

    }


    public class ImageModel
    {
        public string Image { get; set; }

    }

    public class CropNotificationGetModel
    {
        public CropNotificationGetModel()
        {
            this.Notification = new List<getNotification>();
        }
        public int NotificationId { get; set; }
        public int CropId { get; set; }
        public int SeasonId { get; set; }
        public string CropName { get; set; }
        public string SeasonName { get; set; }

        public List<getNotification> Notification { get; set; }
    }

    public class getNotification
    {
        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string Description { get; set; }
        public int? DaysToPush { get; set; }
        public string InformationUrl { get; set; }
        public string Image { get; set; }

    }

    public class NotificationDayModel
    {
        public string DaysToPush { get; set; }

        public string PhaseName { get; set; }
        public string NotificationDate { get; set; }

    }
}
