using StartUpX.Entity.DataModels;
using StartUpX.Model;
using CorePush.Google;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Threading.Tasks;
using static StartUpX.Business.Implementation.GoogleCropNotification;
using static StartUpX.Business.Implementation.GoogleNotification;

namespace StartUpX.Business.Implementation

{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(string DeviceId,int Id, string Message, Boolean IsAndroiodDevice, string Body,string Notification);
        Task<ResponseModel> SendCropNotification(string DeviceId,  string Message, Boolean IsAndroiodDevice, string Body, string Image);

    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;
        private readonly ILogger<ScheduleJob> _logger;


        public NotificationService(IOptions<FcmNotificationSetting> settings, IScheduleConfig<ScheduleJob> config, ILogger<ScheduleJob> logger, IOptions<ConfigurationModel> hostName, AgtonomicsAgriCultureDbContext agriContext)
        {
            _fcmNotificationSetting = settings.Value;
            _agriContext = agriContext;
            this._configuration = hostName.Value;
            _logger = logger;


        }

        public async Task<ResponseModel> SendNotification(string DeviceId, int Id, string Message, Boolean IsAndroiodDevice, string Body, string Notification)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (IsAndroiodDevice == true)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();
                    string authorizationKey = string.Format("key={0}", settings.ServerKey);
                    string deviceToken = DeviceId;
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = Message;
                    dataPayload.Body = Body;
                    dataPayload.NotificationType = Notification;
                    var imgModel = new PostImageForMobileModel();
                    if (Id != 0)
                    {
                        var PostImageList = _agriContext.PostImges
                                                  .Where(x => x.PostId == Id).ToList();
                        if (PostImageList.Count > 0)

                            foreach (var itemImages in PostImageList)
                            {
                                imgModel.Img = _configuration.HostName + itemImages.ImageUrl;
                                dataPayload.Img.Add(imgModel);
                            }
                    }
                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;
                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(DeviceId, notification);
                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        notification.Data = dataPayload;
                        var pathToSave1 = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                        pathToSave1 = pathToSave1 + "\\";
                        using (StreamWriter w = File.AppendText(pathToSave1 + "log.txt"))
                        {

                            Log((response.Message).ToString(), w);
                        }
                        using (StreamReader r = File.OpenText("log.txt"))
                        {
                            DumpLog(r);
                        }

                        return response;

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        var pathToSave1 = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                        pathToSave1 = pathToSave1 + "\\";
                        using (StreamWriter w = File.AppendText(pathToSave1 + "log.txt"))
                        {

                            Log((response.Message).ToString(), w);
                        }
                        using (StreamReader r = File.OpenText("log.txt"))
                        {
                            DumpLog(r);
                        }
                        return response;

                    }

                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                var pathToSave1 = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                pathToSave1 = pathToSave1 + "\\";
                using (StreamWriter w = File.AppendText(pathToSave1 + "log.txt"))
                {

                    Log((response).ToString(), w);
                }
                using (StreamReader r = File.OpenText("log.txt"))
                {
                    DumpLog(r);
                }
                return response;

            }

        }


        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public async Task<ResponseModel> SendCropNotification(string DeviceId, string Message, bool IsAndroiodDevice, string Body, string Image)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //if (IsAndroiodDevice == true)
                //{
                //    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();
                    var cropmodel = new NotificationDayModel();
                    var notificationdata = _agriContext.CropNotifications.Where(x => x.IsActive == true).ToList();

                    foreach (var item in notificationdata)
                    {
                        var notifications = _agriContext.CropNotifications.Where(x => x.NotificationId == item.NotificationId).ToList();
                        foreach (var phase in notifications)
                        {
                            var seasondays = _agriContext.SeasonMasters.Where(x => x.SeasonId == phase.SeasonId).FirstOrDefault();
                            //var days = seasondays.SeasonStartMonth.AddDays(phase.DaysToPush);
                            //cropmodel.DaysToPush = days.ToShortDateString();
                            //if (cropmodel.DaysToPush == DateTime.Now.ToShortDateString())
                            //{
                               string authorizationKey = string.Format("key={0}", settings.ServerKey);
                                string deviceToken = "f6hBdqz_RDu_7oV5e82IxE:APA91bGLteZkmfGFqmH5og2Z9qS4LG3odmQLGjLFJsi2NgYQLoHW9odjqqMuDCyDJv7tkLTaoaQlI3jn4OcwFpkXCEKeGC8B8qMccEDofUeP_WX7D1jeUo_zvOy7Zqgp9z4YVQWnaeDW";
                                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                CropDataPayload dataPayload = new CropDataPayload();
                                dataPayload.Title = "Hi";
                                dataPayload.Body = phase.NotificationId.ToString();
                                dataPayload.Image = Image;
                                GoogleCropNotification notification = new GoogleCropNotification();
                                notification.Data = dataPayload;
                                notification.Notification = dataPayload;
                                var fcm = new FcmSender(settings, httpClient);
                                 var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
                                 
                                if (fcmSendResponse.IsSuccess())
                                {
                                    response.IsSuccess = true;
                                    response.Message = "Notification sent successfully";
                                    notification.Data = dataPayload;
                                    _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ScheduleJob is working.");
                                       
                                }
                                else
                                {
                                    response.IsSuccess = false;
                                    response.Message = fcmSendResponse.Results[0].Error;

                                }
                            //}
                            //else
                            //{

                            //}
                        }
                    }
                //}
                //else
                //{
                //    /* Code here for APN Sender (iOS Device) */
                //    //var apn = new ApnSender(apnSettings, httpClient);
                //    //await apn.SendAsync(notification, deviceToken);
                //}
                return response;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                var pathToSave1 = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                pathToSave1 = pathToSave1 + "\\";
                using (StreamWriter w = File.AppendText(pathToSave1 + "log.txt"))
                {

                    Log((response).ToString(), w);
                }
                using (StreamReader r = File.OpenText("log.txt"))
                {
                    DumpLog(r);
                }
                return response;

            }

        }
    }
}
