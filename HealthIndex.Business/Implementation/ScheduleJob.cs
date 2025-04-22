using StartUpX.Entity.DataModels;
using StartUpX.Model;
using CorePush.Google;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StartUpX.Business.Implementation.GoogleCropNotification;

namespace StartUpX.Business.Implementation
{
    public class ScheduleJob : CronJobService
    {
        private readonly ILogger<ScheduleJob> _logger;
        private INotificationService _notificationService;
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        AgtonomicsAgriCultureDbContext _agriContext;


        public ScheduleJob(IScheduleConfig<ScheduleJob> config, IOptions<FcmNotificationSetting> settings, AgtonomicsAgriCultureDbContext agriContext,INotificationService notificationService,ILogger<ScheduleJob> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _notificationService = notificationService;
            _fcmNotificationSetting = settings.Value;
            _agriContext = agriContext;
 
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ScheduleJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
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
                var farmerlist = _agriContext.FarmerMasters.Where(x => x.DeleteStatus == false && x.DeviceToken != null).ToList();
                foreach (var tokens in farmerlist)
                {

                    var notificationdata = _agriContext.CropNotifications.Where(x => x.IsActive == true).ToList();

                    foreach (var item in notificationdata)
                    {
                        var notifications = (from cropnotification in _agriContext.CropNotifications

                                             join Crop in _agriContext.CropMasters
                                             on cropnotification.CropId equals Crop.CropId
                                             join phase in _agriContext.NotificationPhases
                                             on cropnotification.NotificationPhaseId equals phase.NotificationPhaseId
                                             where cropnotification.IsActive == true && cropnotification.NotificationId == item.NotificationId
                                             select new
                                             {
                                                 cropnotification.NotificationId,
                                                 cropnotification.CropId,
                                                 cropnotification.SeasonId,
                                                 Crop.CropName,
                                                 cropnotification.InformationUrl,
                                                 cropnotification.DaysToPush,
                                                 cropnotification.Description,
                                                 cropnotification.NotificationPhaseId,
                                                 phase.NotificationPhaseName,
                                                 cropnotification.Image

                                             }
                                     ).ToList();

                        //var notifications = _agriContext.CropNotifications.Where(x => x.NotificationId == item.NotificationId).ToList();
                        foreach (var phase in notifications)
                        {
                            var seasondays = _agriContext.SeasonMasters.Where(x => x.SeasonId == phase.SeasonId).FirstOrDefault();
                           // var days = seasondays.SeasonStartMonth.AddDays(phase.DaysToPush);
                           // cropmodel.DaysToPush = days.ToShortDateString();
                            if (cropmodel.DaysToPush == DateTime.Now.ToShortDateString())
                            {
                            string authorizationKey = string.Format("key={0}", settings.ServerKey);
                            string deviceToken = tokens.DeviceToken;
                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            CropDataPayload dataPayload = new CropDataPayload();
                            dataPayload.Title = phase.NotificationPhaseName + " " + " date of" + " " + phase.CropName + " " + "is today";
                            dataPayload.Body = phase.Description;
                            dataPayload.Image = phase.Image;
                            GoogleCropNotification notification = new GoogleCropNotification();
                            notification.Data = dataPayload;
                            notification.Notification = dataPayload;
                            var fcm = new FcmSender(settings, httpClient);
                            var fcmSendResponse = fcm.SendAsync(deviceToken, notification);


                            }
                            else
                            {

                            }
                        }
                    }
                }
                //}
                //else
                //{
                //    /* Code here for APN Sender (iOS Device) */
                //    //var apn = new ApnSender(apnSettings, httpClient);
                //    //await apn.SendAsync(notification, deviceToken);
                //}
                // return response;

            }
            catch (Exception ex)
            {
               // response.IsSuccess = false;
                //response.Message = "Something went wrong";
                // return response;

            }

            //_notificationService.SendNotification("f6hBdqz_RDu_7oV5e82IxE:APA91bGLteZkmfGFqmH5og2Z9qS4LG3odmQLGjLFJsi2NgYQLoHW9odjqqMuDCyDJv7tkLTaoaQlI3jn4OcwFpkXCEKeGC8B8qMccEDofUeP_WX7D1jeUo_zvOy7Zqgp9z4YVQWnaeDW", 0, "Hi", true, "Hi", "Policy");
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ScheduleJob is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ScheduleJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
