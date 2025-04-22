using StartUpX.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class NotificationModel
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("isAndroiodDevice")]
        public bool IsAndroiodDevice { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class GoogleNotification
    {
        public class DataPayload
            
        {
            public DataPayload()
            {

                this.Img = new List<PostImageForMobileModel>();
            }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("notificationType")]
            public string NotificationType { get; set; }
            // [JsonArray()]
            [JsonProperty("image")]
            public List<PostImageForMobileModel> Img { get; set; }


        }
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";

        [JsonProperty("data")]
        public DataPayload Data { get; set; }

        [JsonProperty("notification")]
        public DataPayload Notification { get; set; }
    }


    public class GoogleCropNotification
    {
        public class CropDataPayload

        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("notificationType")]
            public string NotificationType { get; set; }
            // [JsonArray()]
            [JsonProperty("image")]
            public string Image { get; set; }


        }
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";

        [JsonProperty("data")]
        public CropDataPayload Data { get; set; }

        [JsonProperty("notification")]
        public CropDataPayload Notification { get; set; }
    }
}
