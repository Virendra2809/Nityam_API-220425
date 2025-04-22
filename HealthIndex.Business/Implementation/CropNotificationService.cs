using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class CropNotificationService:ICropNotificationService
    {

        private AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public CropNotificationService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;
        }
        public List<CropNotificationModel> GetAllNotification()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropModelList = new List<CropNotificationModel>();
            var CropListEntity = (from cropnotification in _agriContext.CropNotifications
                                   join Crop in _agriContext.CropMasters
                                   on cropnotification.CropId equals Crop.CropId
                                   join phase in _agriContext.NotificationPhases
                                   on cropnotification.NotificationPhaseId equals phase.NotificationPhaseId
                                   join season in _agriContext.SeasonMasters
                                   on cropnotification.SeasonId equals season.SeasonId
                                   where cropnotification.IsActive == true
                                   select new
                                            {
                                                cropnotification.NotificationId,
                                                cropnotification.CropId,
                                                cropnotification.SeasonId,
                                                Crop.CropName,
                                                cropnotification.InformationUrl,
                                                cropnotification.DaysToPush,
                                                cropnotification.Description,
                                                season.SeasonName,
                                                cropnotification.NotificationPhaseId,
                                                phase.NotificationPhaseName,
                                                cropnotification.Image
                                            }
                                  ).ToList();
            if (CropListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropListEntity)
            {
                var model = new CropNotificationModel();
                model.NotificationId = item.NotificationId;
                model.NotificationPhaseId = item.NotificationPhaseId;
                model.NotificationPhaseName = item.NotificationPhaseName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeasonId = item.SeasonId;
                model.SeasonName = item.SeasonName;
                model.Description = item.Description;
                model.DaysToPush = item.DaysToPush;
                model.InformationUrl = item.InformationUrl;
                model.Image = item.Image;
                CropModelList.Add(model);
            }
            return CropModelList;
        }
        public string AddCropNotification(CropNotificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.CropNotifications.Any(x => x.NotificationId == model.NotificationId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var cropNotificationEntity = new CropNotification();
                cropNotificationEntity.NotificationId = model.NotificationId;
                cropNotificationEntity.NotificationPhaseId = model.NotificationPhaseId;
                cropNotificationEntity.CropId = model.CropId;
                cropNotificationEntity.Description = model.Description;
                cropNotificationEntity.DaysToPush = (int)model.DaysToPush;
                cropNotificationEntity.SeasonId = model.SeasonId;
                cropNotificationEntity.EnteredBy = model.EnteredBy;
                cropNotificationEntity.EnteredDate = DateTime.Now;
                cropNotificationEntity.InformationUrl = model.InformationUrl;
                cropNotificationEntity.Image = model.Image;
                cropNotificationEntity.IsActive = true;
                _agriContext.CropNotifications.Add(cropNotificationEntity);
                _agriContext.SaveChanges();
                message = "Data Added Succesfully ";
            }

            return message;
        }
        public CropNotificationModel GetNotificationById(long NotificationId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var CropListEntity = (from cropnotification in _agriContext.CropNotifications

                                  join Crop in _agriContext.CropMasters
                                  on cropnotification.CropId equals Crop.CropId
                                  join season in _agriContext.SeasonMasters
                                  on cropnotification.SeasonId equals season.SeasonId
                                  join phase in _agriContext.NotificationPhases
                                   on cropnotification.NotificationPhaseId equals phase.NotificationPhaseId
                                  where cropnotification.IsActive == true && cropnotification.NotificationId== NotificationId
                                  select new
                                  {
                                      cropnotification.NotificationId,
                                      cropnotification.CropId,
                                      cropnotification.SeasonId,
                                      Crop.CropName,
                                      cropnotification.InformationUrl,
                                      cropnotification.DaysToPush,
                                      cropnotification.Description,
                                      season.SeasonName,
                                      cropnotification.NotificationPhaseId,
                                      phase.NotificationPhaseName,
                                      cropnotification.Image

                                  }
                                  ).FirstOrDefault();
            if (CropListEntity == null)
            {
                return null;
            }
            else {
               
            var model = new CropNotificationModel();
            model.NotificationId = CropListEntity.NotificationId;
            model.CropId = CropListEntity.CropId;
            model.CropName = CropListEntity.CropName;
            model.SeasonId = CropListEntity.SeasonId;
            model.SeasonName = CropListEntity.SeasonName;
            model.Description = CropListEntity.Description;
            model.DaysToPush = CropListEntity.DaysToPush;
            model.Image = _configuration.HostName + CropListEntity.Image;
            model.InformationUrl = CropListEntity.InformationUrl;
            model.NotificationPhaseId = CropListEntity.NotificationPhaseId;
            model.NotificationPhaseName = CropListEntity.NotificationPhaseName;
                var ImageList = _agriContext.CropNotifications
                                            .Where(x => x.NotificationId == CropListEntity.NotificationId).ToList();
                foreach (var itemImages in ImageList)
                {
                    var imgModel = new ImageModel();
                    if (CropListEntity.Image == null)
                    {
                        imgModel.Image = _configuration.HostName + "/PostImages/no_image.png";
                    }
                    else
                    {
                        imgModel.Image = _configuration.HostName + itemImages.Image;

                    }
                    model.NotificationImages.Add(imgModel);
                }

              
            return model;
            }
        }

        public bool PutNotification(CropNotificationModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropId = Convert.ToInt32(model.CropId);
            var SeasonId = Convert.ToInt32(model.SeasonId);
            var cropNotificationEntity = _agriContext.CropNotifications.FirstOrDefault(x => x.CropId == CropId && x.SeasonId == SeasonId);
            cropNotificationEntity.CropId = model.CropId;
            cropNotificationEntity.SeasonId = model.SeasonId;
            cropNotificationEntity.Description = model.Description;
            cropNotificationEntity.DaysToPush = (int)model.DaysToPush;
            cropNotificationEntity.InformationUrl = model.InformationUrl;
            cropNotificationEntity.Image = model.Image;
            cropNotificationEntity.NotificationPhaseId = model.NotificationPhaseId;
            _agriContext.SaveChanges();
            return true;

        }


        public string DeleteNotification(long NotificationId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            CropNotificationModel model = new CropNotificationModel();
            var cropNotificationEntity = _agriContext.CropNotifications.FirstOrDefault(x => x.NotificationId == NotificationId);
            if (cropNotificationEntity != null)
            {
                cropNotificationEntity.IsActive = false;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        public List<NotificationPhaseModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var NotificationPhaseModelList = new List<NotificationPhaseModel>();
            var NotificationPhaseEntity = (from phase in _agriContext.NotificationPhases
                                          where phase.DeleteStatus == false
                                   select new
                                   {
                                       phase.NotificationPhaseId,
                                       phase.NotificationPhaseName,
                                       phase.Description,
                                       phase.EnteredBy,
                                       phase.EnteredDate,
                                       phase.ChangedBy,
                                       phase.ChangedDate,
                                       phase.DeleteStatus,

                                   }
                                  ).ToList();
            if (NotificationPhaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in NotificationPhaseEntity)
            {
                var model = new NotificationPhaseModel();
                model.NotificationPhaseId = item.NotificationPhaseId;
                model.NotificationPhaseName = item.NotificationPhaseName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                NotificationPhaseModelList.Add(model);
            }
            return NotificationPhaseModelList;



        }
        NotificationPhaseModel ICropNotificationService.GetById(long NotificationPhaseId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var NotificationPhaseEntity = _agriContext.NotificationPhases.FirstOrDefault(x => x.NotificationPhaseId == NotificationPhaseId && !x.DeleteStatus);
            if (NotificationPhaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new NotificationPhaseModel
            {
                NotificationPhaseId = NotificationPhaseEntity.NotificationPhaseId,
                NotificationPhaseName = NotificationPhaseEntity.NotificationPhaseName,
                Description = NotificationPhaseEntity.Description,
                EnteredBy = NotificationPhaseEntity.EnteredBy,
                EnteredDate = NotificationPhaseEntity.EnteredDate,
                ChangedBy = NotificationPhaseEntity.ChangedBy,
                ChangedDate = NotificationPhaseEntity.ChangedDate,
                DeleteStatus = NotificationPhaseEntity.DeleteStatus,
            };

        }

        public string Add(NotificationPhaseModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.NotificationPhases.Any(x => x.NotificationPhaseName == model.NotificationPhaseName);
            if (existing)
            {
                message = GlobalConstants.ExistingName;
            }
            else

            {
                var NotificationPhaseEntity = new NotificationPhase();
                NotificationPhaseEntity.NotificationPhaseId = model.NotificationPhaseId;
                NotificationPhaseEntity.NotificationPhaseName = model.NotificationPhaseName;
                NotificationPhaseEntity.Description = model.Description;
                NotificationPhaseEntity.EnteredBy = model.EnteredBy;
                NotificationPhaseEntity.EnteredDate = DateTime.Now;
                NotificationPhaseEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.NotificationPhases.Add(NotificationPhaseEntity);
                _agriContext.SaveChanges();
                message = "Data Added Succesfully ";
            }
            return message;
        }

        public bool Put(NotificationPhaseModel model, ref ErrorResponseModel errorResponseModel)
        {
            var NotificationPhaseId = Convert.ToInt32(model.NotificationPhaseId);
            var NotificationPhaseEntity = _agriContext.NotificationPhases.FirstOrDefault(x => x.NotificationPhaseId == NotificationPhaseId && !x.DeleteStatus);
            if (NotificationPhaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                NotificationPhaseEntity.NotificationPhaseId = model.NotificationPhaseId;
                NotificationPhaseEntity.NotificationPhaseName = model.NotificationPhaseName;
                NotificationPhaseEntity.Description = model.Description;
                NotificationPhaseEntity.ChangedBy = model.ChangedBy;
                NotificationPhaseEntity.ChangedDate = DateTime.Now;
                NotificationPhaseEntity.DeleteStatus = false;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long NotificationPhaseId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            NotificationPhaseModel model = new NotificationPhaseModel();
            var notificationEntity = _agriContext.NotificationPhases.FirstOrDefault(x => x.NotificationPhaseId == NotificationPhaseId);
            if (notificationEntity != null)
            {
                notificationEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }


}

