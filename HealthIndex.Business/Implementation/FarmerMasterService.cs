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
    public class FarmerMasterService : IFarmerMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public FarmerMasterService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<FarmerMasterModel> GetAllFarmerMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var farmerMasterModelList = new List<FarmerMasterModel>();
            var farmerMasterListEntity = (from farm in _agriContext.FarmerMasters

                                          where farm.DeleteStatus == false
                                          select new
                                          {
                                              farm.FarmerId,
                                              farm.FirstName,
                                              farm.LastName,
                                              farm.Address,
                                              farm.MobileNumber,
                                              farm.FirmIds,
                                              farm.LanguageId,
                                              farm.DeviceToken,
                                              farm.FarmerImage,
                                          }).ToList();
            if (farmerMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in farmerMasterListEntity)
            {
                var model = new FarmerMasterModel();
                model.FarmerId = item.FarmerId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.MobileNumber = item.MobileNumber;
                model.Address = item.Address;
                model.FirmIds = item.FirmIds;
                model.LanguageId = item.LanguageId;
                model.DeviceToken = item.DeviceToken;
                if (model.FarmerImage == null)
                {
                    model.FarmerImage= _configuration.HostName+"/ProductImage/no_image.png";
                }
                else
                {
                    model.FarmerImage = _configuration.HostName + item.FarmerImage;

                }
                farmerMasterModelList.Add(model);
            }
            return farmerMasterModelList;
        }

        public FarmerMasterModel GetFarmerMasterById(long FarmerId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var farmerMasterEntity = (from farm in _agriContext.FarmerMasters

                                      where farm.DeleteStatus == false && farm.FarmerId == FarmerId
                                      select new
                                      {
                                          farm.FarmerId,
                                          farm.FirstName,
                                          farm.LastName,
                                          farm.Address,
                                          farm.MobileNumber,
                                          farm.FirmIds,
                                          farm.LanguageId,
                                          farm.DeviceToken,
                                          farm.FarmerImage,
                                      }).FirstOrDefault();
            if (farmerMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new FarmerMasterModel();
            model.FarmerId = farmerMasterEntity.FarmerId;
            model.FirstName = farmerMasterEntity.FirstName;
            model.LastName = farmerMasterEntity.LastName;
            model.MobileNumber = farmerMasterEntity.MobileNumber;
            model.Address = farmerMasterEntity.Address;
            model.FirmIds = farmerMasterEntity.FirmIds;
            model.LanguageId = farmerMasterEntity.LanguageId;
            model.DeviceToken = farmerMasterEntity.DeviceToken;
            model.FarmerImage = farmerMasterEntity.FarmerImage;
            if (model.FarmerImage == null)
            {
                model.FarmerImage = _configuration.HostName+"/ProductImage/no_image.png";
            }
            else
            {
                model.FarmerImage = _configuration.HostName + farmerMasterEntity.FarmerImage;

            }
            if (farmerMasterEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }
         

        public string AddFarmerMaster(FarmerAddModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            //var farmerId = model.FarmerId;
            //var farmerMasterEntity = _agriContext.FarmerMasters.FirstOrDefault(x => x.FarmerId == farmerId);

            var existingFarmer = _agriContext.FarmerMasters.Where(x => x.MobileNumber == model.MobileNumber).FirstOrDefault();
            if (existingFarmer != null)
            {
                message = (existingFarmer.FarmerId).ToString();
            }
            else
            {
                FarmerMaster farmerMasterEntity = new FarmerMaster();
                farmerMasterEntity.FirstName = model.FirstName;
                farmerMasterEntity.LastName = model.LastName;
                farmerMasterEntity.MobileNumber = model.MobileNumber;
                farmerMasterEntity.Address = model.Address;
                farmerMasterEntity.FirmIds = model.FirmIds;
                farmerMasterEntity.LanguageId = model.LanguageId;
                farmerMasterEntity.DeleteStatus = false;
                farmerMasterEntity.DeviceToken = model.DeviceToken;
                //farmerMasterEntity.FarmerImage = (model.FarmerImage);
                _agriContext.FarmerMasters.Add(farmerMasterEntity);
                _agriContext.SaveChanges();
                message = (farmerMasterEntity.FarmerId).ToString();
            }
            return message;
        }


        public FarmerMasterModel ExistingFarmer(string MobileNumber, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var farmerMasterEntity = (from farm in _agriContext.FarmerMasters

                                      where farm.DeleteStatus == false && farm.MobileNumber == MobileNumber
                                      select new
                                      {
                                          farm.FarmerId,
                                          farm.FirstName,
                                          farm.LastName,
                                          farm.Address,
                                          farm.MobileNumber,
                                          farm.FirmIds,
                                          farm.LanguageId,
                                          farm.DeviceToken
                                      }).FirstOrDefault();
            if (farmerMasterEntity == null)
            {
              
                return null;
            }
            return new FarmerMasterModel
            {
                FarmerId = farmerMasterEntity.FarmerId,
                FirstName = farmerMasterEntity.FirstName,
                LastName = farmerMasterEntity.LastName,
                MobileNumber = farmerMasterEntity.MobileNumber,
                Address = farmerMasterEntity.Address,
                FirmIds = farmerMasterEntity.FirmIds,
                LanguageId = farmerMasterEntity.LanguageId,
                DeviceToken=farmerMasterEntity.DeviceToken
            };
        }

        public bool UpdatefarmerMaster(FarmerMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var farmerId = model.FarmerId;
            var farmerMasterEntity = _agriContext.FarmerMasters.FirstOrDefault(x => x.FarmerId == farmerId);
            if (farmerMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                farmerMasterEntity.FirstName = model.FirstName;
                farmerMasterEntity.LastName = model.LastName;
                farmerMasterEntity.MobileNumber = model.MobileNumber;
                farmerMasterEntity.Address = model.Address;
                farmerMasterEntity.FirmIds = model.FirmIds;
                farmerMasterEntity.LanguageId = model.LanguageId;
                farmerMasterEntity.DeviceToken = model.DeviceToken;
                farmerMasterEntity.FarmerImage = model.FarmerImage;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public bool UpdateDeviceToken(FarmerDeviceTokenModel model, ref ErrorResponseModel errorResponseModel)
        {
            var farmerId = model.FarmerId;
            var farmerMasterEntity = _agriContext.FarmerMasters.FirstOrDefault(x => x.FarmerId == farmerId);
            if (farmerMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                farmerMasterEntity.DeviceToken = model.DeviceToken;
                _agriContext.SaveChanges();
                return true;
            }
        }
        public string DeleteFarmerMaster(long FarmerId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            FarmerMasterModel model = new FarmerMasterModel();
            var farmerMasterEntity = _agriContext.FarmerMasters.FirstOrDefault(x => x.FarmerId == FarmerId);
            if (farmerMasterEntity != null)
            {
                farmerMasterEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public string DeleteImageFromDB(FarmerMasterModel model)
        {
            var imageEntityList = _agriContext.FarmerMasters.FirstOrDefault(x => x.FarmerId == model.FarmerId);
            imageEntityList.FarmerImage = null;
          //  _agriContext.Remove(imageEntityList);
            _agriContext.SaveChanges();
            return "deleted.";

        }

    }


}
