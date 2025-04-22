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
   public class LandPreparationDetailService :ILandPreparationDetails
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        // private IConfiguration _configuration;
        private ConfigurationModel _configuration;

        public object Value => throw new NotImplementedException();
        public LandPreparationDetailService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName
)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<LandPreparationDetailsModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var LandPreparationDetailList = new List<LandPreparationDetailsModel>();
            var LandPreparationDetailEntity = (from LandPreparationDetail in _agriContext.LandPreparationDetails
                                                join CropMaster in _agriContext.CropMasters
                                                on LandPreparationDetail.CropId equals CropMaster.CropId

                                               join Season in _agriContext.SeasonMasters
                                               on LandPreparationDetail.SeasonId equals Season.SeasonId
                                               select new
                                                      {

                                                          LandPreparationDetail.LandPreparationId,
                                                          LandPreparationDetail.Title,
                                                          CropMaster.CropId,
                                                          CropMaster.CropName,
                                                          Season.SeasonId,
                                                          Season.SeasonName,
                                                          LandPreparationDetail.SeqNo,
                                                          LandPreparationDetail.Description,
                                                         
                                                      }
                                  ).ToList();
            if (LandPreparationDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in LandPreparationDetailEntity)
            {
                var model = new LandPreparationDetailsModel();
                model.LandPreparationId = item.LandPreparationId;
                model.Title = item.Title;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeasonId = item.SeasonId;
                model.SeasonName = item.SeasonName;
                model.SeqNo = item.SeqNo;
                model.Description = item.Description;

                var LandImageList = _agriContext.LandPreparationImages
                                                                .Where(x => x.LandPreparationId == item.LandPreparationId).ToList();
                foreach (var itemImages in LandImageList)
                {
                    var imgModel = new LandPreparationImageModel();
                    imgModel.LandPreparationId = itemImages.LandPreparationId;
                    imgModel.LandPreparationImageId = itemImages.LandPreparationImageId;
                    imgModel.ImageName = itemImages.ImageName;
                    imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                    model.LandPreparationImages.Add(imgModel);
                }

                LandPreparationDetailList.Add(model);
            }
            return LandPreparationDetailList;



        }
        LandPreparationDetailsModel ILandPreparationDetails.GetById(long LandPreparationId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var LandPreparationDetailEntity = (from LandPreparationDetail in _agriContext.LandPreparationDetails
                                               join CropMaster in _agriContext.CropMasters
                                               on LandPreparationDetail.CropId equals CropMaster.CropId

                                               join Season in _agriContext.SeasonMasters
                                              on LandPreparationDetail.SeasonId equals Season.SeasonId

                                               where LandPreparationDetail.LandPreparationId == LandPreparationId
                                               select new
                                               {

                                                   LandPreparationDetail.LandPreparationId,
                                                   LandPreparationDetail.Title,
                                                   CropMaster.CropId,
                                                   CropMaster.CropName,
                                                   Season.SeasonId,
                                                   Season.SeasonName,
                                                   LandPreparationDetail.SeqNo,
                                                   LandPreparationDetail.Description,
                                                  
                                               }
                                  ).FirstOrDefault();
            if (LandPreparationDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new LandPreparationDetailsModel();
            model.LandPreparationId = LandPreparationDetailEntity.LandPreparationId;
            model.Title = LandPreparationDetailEntity.Title;
            model.CropId = LandPreparationDetailEntity.CropId;
            model.CropName = LandPreparationDetailEntity.CropName;
            model.SeasonId = LandPreparationDetailEntity.SeasonId;
            model.SeasonName = LandPreparationDetailEntity.SeasonName;
            model.Description = LandPreparationDetailEntity.Description;
            model.SeqNo = LandPreparationDetailEntity.SeqNo;
               
                var LandImageList = _agriContext.LandPreparationImages
                                                                .Where(x => x.LandPreparationId == LandPreparationDetailEntity.LandPreparationId).ToList();
            foreach (var itemImages in LandImageList)
            {
                var imgModel = new LandPreparationImageModel();
                imgModel.LandPreparationId = itemImages.LandPreparationId;
                imgModel.LandPreparationImageId = itemImages.LandPreparationImageId;
                imgModel.ImageName = itemImages.ImageName;
                imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                model.LandPreparationImages.Add(imgModel);
            }
            if (LandPreparationDetailEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string Add(LandPreparationDetailsModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.LandPreparationDetails.Any(x => x.SeqNo == model.SeqNo);
            if (existing)
            {
                message = GlobalConstants.ExistingSequenceNumber;
            }
            else
            {
                var landPreparationEntity = new LandPreparationDetail();
                landPreparationEntity.LandPreparationId = model.LandPreparationId;
                landPreparationEntity.Title = model.Title;
                landPreparationEntity.CropId = model.CropId;
                landPreparationEntity.Description = model.Description;
                landPreparationEntity.SeqNo = model.SeqNo;
                landPreparationEntity.SeasonId = model.SeasonId;
                _agriContext.LandPreparationDetails.Add(landPreparationEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.LandPreparationImages)
                {
                    var productImageEntity = new LandPreparationImage();

                    productImageEntity.LandPreparationImageId = item.LandPreparationImageId;
                    productImageEntity.LandPreparationId = landPreparationEntity.LandPreparationId;
                    productImageEntity.ImageName = item.ImageName;
                    productImageEntity.ImageUrl = item.ImageUrl;
                    _agriContext.Add(productImageEntity);
                }
                _agriContext.SaveChanges();
                message = "Land Preparation Data Added Succesfully ";
            }
        
                return message;
        }
        

        public bool Put(LandPreparationDetailsModel model, ref ErrorResponseModel errorResponseModel)
        {
            var LandPreparationId = Convert.ToInt32(model.LandPreparationId);
            var landPreparationEntity = _agriContext.LandPreparationDetails.FirstOrDefault(x => x.LandPreparationId == LandPreparationId);
            if (landPreparationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                landPreparationEntity.LandPreparationId = model.LandPreparationId;
                landPreparationEntity.Title = model.Title;
                landPreparationEntity.CropId = model.CropId;
                landPreparationEntity.Description = model.Description;
                landPreparationEntity.SeqNo = model.SeqNo;
                landPreparationEntity.SeasonId = model.SeasonId;
                _agriContext.SaveChanges();
                if (model.LandPreparationImages.Count > 0)
                { 
                    foreach (var item in model.LandPreparationImages)
                    {
                        var productImageEntity = new LandPreparationImage();

                        productImageEntity.LandPreparationImageId = item.LandPreparationImageId;
                        productImageEntity.LandPreparationId = landPreparationEntity.LandPreparationId;
                        productImageEntity.ImageName = item.ImageName;
                        productImageEntity.ImageUrl = item.ImageUrl;
                        _agriContext.Add(productImageEntity);
                    }
                _agriContext.SaveChanges();
            }
                return true;
            }
        }


        public string DeleteImageFromDB(LandPreparationDetailsModel model)
        {
            var imageEntityList = _agriContext.LandPreparationImages.Where(x => x.LandPreparationId == model.LandPreparationId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.LandPreparationImages.FirstOrDefault(x => x.LandPreparationImageId == item.LandPreparationImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }
        public string Delete(long LandPreparationId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var landEntity = _agriContext.LandPreparationDetails.FirstOrDefault(x => x.LandPreparationId == LandPreparationId);
            if (landEntity != null)
            {
                _agriContext.Remove(landEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        LandPreparationModel ILandPreparationDetails.GetByCropId(long CropId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropEntity = (from land in _agriContext.LandPreparationDetails
                                     join crop in _agriContext.CropMasters
                                      on land.CropId equals crop.CropId
                                     join Season in _agriContext.SeasonMasters
                                     on land.SeasonId equals Season.SeasonId
                                     where land.CropId == CropId
                                     select new
                                     {
                                         land.LandPreparationId,
                                         Season.SeasonId,
                                         Season.SeasonName,
                                         crop.CropId,
                                         crop.CropName

                                     }
                                 ).FirstOrDefault();

            if (cropEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            var model = new LandPreparationModel();
            model.LandPreparationId = cropEntity.LandPreparationId;
            model.SeasonId = cropEntity.SeasonId;
            model.SeasonName = cropEntity.SeasonName;
            model.CropId = cropEntity.CropId;
            model.CropName = cropEntity.CropName;
            var LandImageList = _agriContext.LandPreparationImages
                                                               .Where(x => x.LandPreparationId == cropEntity.LandPreparationId).ToList();
            foreach (var itemImages in LandImageList)
            {
                var imgModel = new LandPreparationImageModel();
                imgModel.LandPreparationId = itemImages.LandPreparationId;
                imgModel.LandPreparationImageId = itemImages.LandPreparationImageId;
                imgModel.ImageName = itemImages.ImageName;
                imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                model.LandPreparationImages.Add(imgModel);
            }
            var LandDetailList = _agriContext.LandPreparationDetails
                                               .Where(x => x.CropId == cropEntity.CropId).ToList();
            foreach (var itemImages in LandDetailList)
            {
                var imgModel = new LandDetailModel();
                imgModel.SeqNo = itemImages.SeqNo;
                imgModel.Title = itemImages.Title;
                imgModel.Description = itemImages.Description;
                model.LandDetails.Add(imgModel);
            }
            if (cropEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }


    }
}



