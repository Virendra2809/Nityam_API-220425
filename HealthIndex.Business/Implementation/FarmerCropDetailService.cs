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
    public class FarmerCropDetailService : IFarmerCropDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public object Value => throw new NotImplementedException();
        public FarmerCropDetailService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<FarmerCropDetailsModel> GetAll(int FarmerId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var FarmarCropDetailModelList = new List<FarmerCropDetailsModel>();

            var FarmarCropDetailListEntity = (from farm in _agriContext.FarmerCropDetails
                                              join soil in _agriContext.SoilTypeMasters
                                              on farm.SoilTypeId equals soil.SoilTypeId
                                              into ps
                                              from soil in ps.DefaultIfEmpty()
                                              where farm.DeleteStatus == false 
                                              select new
                                              {
                                                  farm.FarmerCropDetailId,
                                                  farm.FarmName,
                                                  farm.SowingDate,
                                                  farm.SowingAreaInAres,
                                                  farm.SowingAreaInAcre,
                                                  farm.SowingAreaInHect,
                                                  SoilTypeId = soil.SoilTypeId == null ? 0 : soil.SoilTypeId,
                                                  soil.SoilTypeName
                                              }
                                  ).ToList();
            if (FarmarCropDetailListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            foreach (var item in FarmarCropDetailListEntity)
            {

                var model = new FarmerCropDetailsModel();
                model.FarmerCropDetailId = item.FarmerCropDetailId;
                model.FarmName = item.FarmName;
                model.SowingDate = DateTime.Today;
                model.SowingAreaInHect = item.SowingAreaInHect;
                model.SowingAreaInAcre = item.SowingAreaInAcre;
                model.SowingAreaInAres = item.SowingAreaInAres;
                model.SoilTypeId = item.SoilTypeId;
                model.SoilTypeName = item.SoilTypeName;
                FarmarCropDetailModelList.Add(model);

            }
            return FarmarCropDetailModelList;

        }

        public List<FarmerSelectedCropModel> GetByFarmerId(int FarmerId)
        {
            var farmerList = new List<FarmerSelectedCropModel>();
            var farmerListEntity = (from FarmerCropDetail in _agriContext.FarmerSelectedCrops
                                    join Crop in _agriContext.CropMasters
                                    on FarmerCropDetail.FarmerCropId equals Crop.CropId
                                    //join soil in _agriContext.SoilTypeMasters
                                    //on FarmerCropDetail.SoilTypeId equals soil.SoilTypeId
                                    //into ps
                                    //from soil in ps.DefaultIfEmpty()
                                    where FarmerCropDetail.FarmerId == FarmerId && FarmerCropDetail.DeleteStatus==false
                                    select new
                                    {
                                        FarmerCropDetail.FarmerSelectedCropId,
                                        FarmerCropDetail.FarmerCropId,
                                        FarmerCropDetail.FarmerId,
                                        FarmerCropDetail.DeleteStatus,
                                        //FarmerCropDetail.SowingDate,
                                        //FarmerCropDetail.SowingAreaInHect,
                                        //FarmerCropDetail.SowingAreaInAcre,
                                        //FarmerCropDetail.SowingAreaInAres,
                                        Crop.CropImage,
                                        Crop.CropName,
                                        //SoilTypeId = soil.SoilTypeId == null ? 0 : soil.SoilTypeId,
                                        //soil.SoilTypeName,
                                        //Latitude= FarmerCropDetail.Longitude == null ? 0:FarmerCropDetail.Latitude,
                                        //Longitude= FarmerCropDetail.Longitude == null ? 0 : FarmerCropDetail.Longitude,
                                        //FarmerCropDetail.Latitude,
                                        //FarmerCropDetail.Longitude,

                                    }).ToList();

            foreach (var item in farmerListEntity)
            {

                var model = new FarmerSelectedCropModel();

                model.FarmerSelectedCropId = item.FarmerSelectedCropId;
                //model.FarmerCropId1 = item.FarmerCropId;
                model.FarmerCropId1 = (int)item.FarmerCropId;
                model.FarmerId = (int)item.FarmerId;
                //model.SowingAreaInHect = item.SowingAreaInHect;
                //model.SowingAreaInAcre = item.SowingAreaInAcre;
                //model.SowingAreaInAres = item.SowingAreaInAres;
                model.DeleteStatus = item.DeleteStatus;
                model.CropImage = _configuration.HostName + item.CropImage;
                model.CropName = item.CropName;
                //model.SoilTypeId = Convert.ToInt32(item.SoilTypeId);
                //model.SoilTypeName = item.SoilTypeName;
                //model.Latitude = item.Latitude;
                //model.Longitude = item.Longitude;
                //if (item.SowingAreaInAcre == null && item.SowingAreaInAres == null && item.SowingAreaInHect == null && (item.SoilTypeId == 0 || item.SoilTypeId == null) && item.SowingDate == null && item.Latitude == null && item.Longitude == null)
                //{
                //    model.AvailableDetails = false;
                //}
                //else
                //{
                //    model.AvailableDetails = true;
                //}
                farmerList.Add(model);

            }
            if (farmerListEntity == null)
            {
                return null;
            }
            else
            {
                return farmerList;
            }
        }

        FarmerCropDetailsModel IFarmerCropDetailService.GetById(long FarmerId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var farmerDetailEntity = (from FarmerCropDetail in _agriContext.FarmerCropDetails
                                      where FarmerCropDetail.FarmerCropDetailId == FarmerId  
                                      join soil in _agriContext.SoilTypeMasters
                                      on FarmerCropDetail.SoilTypeId equals soil.SoilTypeId
                                      into ps
                                      from soil in ps.DefaultIfEmpty()

                                      select new
                                      {
                                          FarmerCropDetail.FarmerCropDetailId,
                                          FarmerCropDetail.FarmName,
                                          FarmerCropDetail.DeleteStatus,
                                          FarmerCropDetail.SowingDate,
                                          FarmerCropDetail.SowingAreaInHect,
                                          FarmerCropDetail.SowingAreaInAcre,
                                          FarmerCropDetail.SowingAreaInAres,
                                          SoilTypeId = soil.SoilTypeId == null ? 0 : soil.SoilTypeId,
                                          soil.SoilTypeName,
                                          FarmerCropDetail.Latitude,
                                          FarmerCropDetail.Longitude

                                      }
                                  ).FirstOrDefault();
            if (farmerDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new FarmerCropDetailsModel
            {

                FarmerCropDetailId = farmerDetailEntity.FarmerCropDetailId,
                FarmName=farmerDetailEntity.FarmName,
                SowingDate = farmerDetailEntity.SowingDate,
                SowingAreaInHect = farmerDetailEntity.SowingAreaInHect,
                SowingAreaInAres = farmerDetailEntity.SowingAreaInAres,
                SowingAreaInAcre = farmerDetailEntity.SowingAreaInAcre,
                SoilTypeId = farmerDetailEntity.SoilTypeId,
                SoilTypeName = farmerDetailEntity.SoilTypeName,
                DeleteStatus = farmerDetailEntity.DeleteStatus,
                Latitude = (decimal)farmerDetailEntity.Latitude,
                Longitude = (decimal)farmerDetailEntity.Longitude

            };

        }

        public string Add(FarmAddModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            //var FarmarCropModelList = new FarmerCropDetailsModel();

            //int[] arr = model.FarmerSelectedCropId;
            //var t2 = arr.Length;
            //foreach (var i in arr)
            //{
                

                    FarmerCropDetail farmerEntity = new FarmerCropDetail();
                    farmerEntity.FarmerCropDetailId = model.FarmerCropDetailId;
                    farmerEntity.FarmName = model.FarmName;
                    farmerEntity.FarmerSelectedCropId = model.FarmerSelectedCropId; 
                    farmerEntity.SowingDate = model.SowingDate;
                    farmerEntity.SowingAreaInHect = model.SowingAreaInHect;
                    farmerEntity.SowingAreaInAres = model.SowingAreaInAres;
                    farmerEntity.SowingAreaInAcre = model.SowingAreaInAcre;
                    farmerEntity.SoilTypeId = model.SoilTypeId;
                    farmerEntity.Latitude = model.Latitude;
                    farmerEntity.Longitude = model.Longitude;
                    farmerEntity.DeleteStatus = false;
                    _agriContext.FarmerCropDetails.Add(farmerEntity);
                    _agriContext.SaveChanges();
                    message = "Data Added Succesfully";


                
           // }

            return message;
        }

        public bool Put(FarmerCropDetailsModel model, ref ErrorResponseModel errorResponseModel)
        {
            var FarmerCropDetailId = Convert.ToInt32(model.FarmerCropDetailId);
            var farmerEntity = _agriContext.FarmerCropDetails.FirstOrDefault(x => x.FarmerCropDetailId == FarmerCropDetailId);
            if (farmerEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                farmerEntity.FarmerCropDetailId = model.FarmerCropDetailId;
                farmerEntity.FarmName = model.FarmName;
                farmerEntity.SowingDate = model.SowingDate;
                farmerEntity.SowingAreaInHect = model.SowingAreaInHect;
                farmerEntity.SowingAreaInAres = model.SowingAreaInAres;
                farmerEntity.SowingAreaInAcre = model.SowingAreaInAcre;
                farmerEntity.SoilTypeId = model.SoilTypeId;
                farmerEntity.DeleteStatus = false;
                farmerEntity.Latitude = model.Latitude;
                farmerEntity.Longitude = model.Longitude;
                farmerEntity.FarmerSelectedCropId = model.FarmerSelectedCropId;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public List<FarmerSelectedCropModel> GetCropListByFarmerId(int FarmerId)
        {
            var farmerList = new List<FarmerSelectedCropModel>();
            var farmerListEntity = (from FarmerCropDetail in _agriContext.FarmerSelectedCrops
                                    join Crop in _agriContext.CropMasters
                                    on FarmerCropDetail.FarmerCropId equals Crop.CropId
                                    where FarmerCropDetail.FarmerId == FarmerId && FarmerCropDetail.DeleteStatus==false && Crop.DeleteStatus==false
                                    select new
                                    {
                                        FarmerCropDetail.FarmerSelectedCropId,
                                     
                                        FarmerCropDetail.FarmerCropId,
                                        FarmerCropDetail.FarmerId,
                                        FarmerCropDetail.DeleteStatus,
                                        Crop.CropId,
                                        Crop.CropImage,
                                        Crop.CropName,
                                    }).ToList();

            foreach (var item in farmerListEntity)
            {

                var model = new FarmerSelectedCropModel();
                model.FarmerSelectedCropId = item.FarmerSelectedCropId;
                model.FarmerCropId1 = (int)item.FarmerCropId;
                model.FarmerId = (int)item.FarmerId;
                model.DeleteStatus = item.DeleteStatus;
                model.CropImage = _configuration.HostName + item.CropImage;
                model.CropName = item.CropName;
                farmerList.Add(model);

            }
            if (farmerListEntity == null)
            {
                return null;
            }
            else
            {
                return farmerList;
            }
        }

        public string Delete(long FarmerCropDetailId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            FarmerCropDetailsModel model = new FarmerCropDetailsModel();
            var farmerCropEntity = _agriContext.FarmerCropDetails.FirstOrDefault(x => x.FarmerCropDetailId == FarmerCropDetailId);
            if (farmerCropEntity != null)
            {
                farmerCropEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;

        }

        public List<FarmerCropDetailsModel> GetAllFarmerDetails(int FarmerId,int CropId)
        {

            var farmerList = new List<FarmerCropDetailsModel>();

            var farmerListEntity = (from FarmerCropDetail in _agriContext.FarmerCropDetails
                                    join selectedcrop in _agriContext.FarmerSelectedCrops
                                    on FarmerCropDetail.FarmerSelectedCropId equals selectedcrop.FarmerSelectedCropId

                                    join Crop in _agriContext.CropMasters
                                    on selectedcrop.FarmerCropId equals Crop.CropId

                                    join soil in _agriContext.SoilTypeMasters
                                    on FarmerCropDetail.SoilTypeId equals soil.SoilTypeId
                                    into ps
                                    from soil in ps.DefaultIfEmpty()

                                    where selectedcrop.FarmerId == FarmerId && selectedcrop.FarmerCropId == CropId && selectedcrop.DeleteStatus == false && FarmerCropDetail.DeleteStatus==false
                                    select new
                                    {
                                        FarmerCropDetail.FarmerCropDetailId,
                                        FarmerCropDetail.FarmName,
                                        selectedcrop.FarmerCropId,
                                        selectedcrop.FarmerId,
                                        FarmerCropDetail.DeleteStatus,
                                        FarmerCropDetail.SowingDate,
                                        FarmerCropDetail.SowingAreaInHect,
                                        FarmerCropDetail.SowingAreaInAcre,
                                        FarmerCropDetail.SowingAreaInAres,
                                        Crop.CropImage,
                                        Crop.CropName,
                                        SoilTypeId = soil.SoilTypeId == null ? 0 : soil.SoilTypeId,
                                        soil.SoilTypeName,
                                        //Latitude= FarmerCropDetail.Longitude == null ? 0:FarmerCropDetail.Latitude,
                                        //Longitude= FarmerCropDetail.Longitude == null ? 0 : FarmerCropDetail.Longitude,
                                        FarmerCropDetail.Latitude,
                                        FarmerCropDetail.Longitude,
                                        selectedcrop.FarmerSelectedCropId,
                                      

                                    }).ToList();

            foreach (var item in farmerListEntity)
            {

                var model = new FarmerCropDetailsModel();

                model.FarmerCropDetailId = item.FarmerCropDetailId;
                model.FarmName = item.FarmName;
                model.FarmerSelectedCropId = item.FarmerSelectedCropId;
                model.FarmerCropId1 = (int)item.FarmerCropId;
                model.FarmerId = (int)item.FarmerId;
                model.SowingDate = item.SowingDate;
                model.SowingAreaInHect = item.SowingAreaInHect;
                model.SowingAreaInAcre = item.SowingAreaInAcre;
                model.SowingAreaInAres = item.SowingAreaInAres;
                model.DeleteStatus = item.DeleteStatus;
                model.CropImage = _configuration.HostName + item.CropImage;
                model.CropName = item.CropName;
                model.SoilTypeId = Convert.ToInt32(item.SoilTypeId);
                model.SoilTypeName = item.SoilTypeName;
                model.Latitude = item.Latitude;
                model.Longitude = item.Longitude;

                farmerList.Add(model);

            }
            if (farmerListEntity == null)
            {
                return null;
            }
            else
            {
                return farmerList;
            }
            return null;
        }


        //Apis For Selected Crops
        public List<FarmerSelectedCropModel> GetAllfarmerCrop()
        {
            var errorResponseModel = new ErrorResponseModel();
            var FarmarCropModelList = new List<FarmerSelectedCropModel>();
            var FarmarCropListEntity = (from FarmerCrop in _agriContext.FarmerSelectedCrops

                                        join Crop in _agriContext.CropMasters
                                        on FarmerCrop.FarmerCropId equals Crop.CropId
                                        where FarmerCrop.DeleteStatus == false


                                        select new
                                        {
                                            FarmerCrop.FarmerSelectedCropId,
                                            FarmerCrop.FarmerCropId,
                                            FarmerCrop.FarmerId,
                                            FarmerCrop.DeleteStatus,
                                            Crop.CropImage,
                                            Crop.CropName,

                                        }
                                  ).ToList();
            if (FarmarCropListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            foreach (var item in FarmarCropListEntity)
            {

                var model = new FarmerSelectedCropModel();
                model.FarmerSelectedCropId = item.FarmerSelectedCropId;
                model.FarmerId = (int)item.FarmerId;
                model.CropName = item.CropName;
                model.FarmerCropId1 = (int)item.FarmerCropId;
                model.CropImage = _configuration.HostName + item.CropImage;
                FarmarCropModelList.Add(model);
            }
            return FarmarCropModelList;

        }

        public string AddSelectedCrop(FarmerSelectedCropModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var FarmarCropModelList = new FarmerSelectedCropModel();

            int[] arr = model.FarmerCropId;
            var t2 = arr.Length;
            foreach (var i in arr)
            {
                var existing = _agriContext.FarmerSelectedCrops.Any(x => x.FarmerId == model.FarmerId
                && x.FarmerCropId == i && x.DeleteStatus == false);
                if (existing)
                {
                    message = "Seleceted Crop Is Already Exist";
                }

                else
                {

                    FarmerSelectedCrop farmerEntity = new FarmerSelectedCrop();
                    farmerEntity.FarmerSelectedCropId = model.FarmerSelectedCropId;
                    farmerEntity.FarmerCropId = i;
                    farmerEntity.FarmerId = model.FarmerId;
                    farmerEntity.DeleteStatus = false;
                    _agriContext.FarmerSelectedCrops.Add(farmerEntity);
                    _agriContext.SaveChanges();
                    message = "Data Added Succesfully";


                }
            }

            return message;
        }

        public FarmerSelectedCropModel FarmerSelectedCropId(long FarmerId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var farmerDetailEntity = (from FarmerCrop in _agriContext.FarmerSelectedCrops

                                      join Crop in _agriContext.CropMasters
                                      on FarmerCrop.FarmerCropId equals Crop.CropId
                                      where FarmerCrop.FarmerId == FarmerId
                                      select new
                                      {
                                          FarmerCrop.FarmerSelectedCropId,
                                          FarmerCrop.FarmerCropId,
                                          FarmerCrop.FarmerId,
                                          FarmerCrop.DeleteStatus,
                                          Crop.CropImage,
                                          Crop.CropName,
                                         
                                      }
                                  ).FirstOrDefault();
            if (farmerDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new FarmerSelectedCropModel
            {

                FarmerSelectedCropId = farmerDetailEntity.FarmerSelectedCropId,
                FarmerCropId1 = Convert.ToInt32(farmerDetailEntity.FarmerCropId),
                FarmerId = (int)farmerDetailEntity.FarmerId,
                CropImage = _configuration.HostName + farmerDetailEntity.CropImage,
                CropName = farmerDetailEntity.CropName,
                DeleteStatus = farmerDetailEntity.DeleteStatus,
            
            };

        }

        public bool PutFarmerSelectedCrop(FarmerSelectedCropModel model, ref ErrorResponseModel errorResponseModel)
        {
            var FarmerSelectedCropId = Convert.ToInt32(model.FarmerSelectedCropId);
            var farmerEntity = _agriContext.FarmerSelectedCrops.FirstOrDefault(x => x.FarmerSelectedCropId == FarmerSelectedCropId);
            if (farmerEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                farmerEntity.FarmerCropId = model.FarmerCropId1;
                farmerEntity.FarmerSelectedCropId = model.FarmerSelectedCropId;
                farmerEntity.FarmerId = model.FarmerId;
                farmerEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }
    
        public string DeleteSelectedCrops(long FarmerSelectedCropId, ref ErrorResponseModel errorResponseModel)
        {
        var message = "";
        errorResponseModel = new ErrorResponseModel();
        FarmerSelectedCropModel model = new FarmerSelectedCropModel();
        var farmerCropEntity = _agriContext.FarmerSelectedCrops.FirstOrDefault(x => x.FarmerSelectedCropId == FarmerSelectedCropId);
        if (farmerCropEntity != null)
        {
            farmerCropEntity.DeleteStatus = true;
            _agriContext.SaveChanges();
            message = "Deleted Successfully";
        }
        return message;

    }

    }

}