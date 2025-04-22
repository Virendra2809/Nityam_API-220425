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
   public class WeedMasterService:IWeedMaster
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        // private IConfiguration _configuration;
        private ConfigurationModel _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public WeedMasterService(AgtonomicsAgriCultureDbContext agriContext,IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<WeedMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var weedMasterModelList = new List<WeedMasterModel>();
            var weedMasterListEntity = (from WeedMaster in _agriContext.WeedMasters
                                        join crop in _agriContext.CropMasters
                                        on WeedMaster.CropId equals crop.CropId

                                        where WeedMaster.DeleteStatus == false
                                              select new
                                              {
                                                  WeedMaster.WeedId,
                                                  WeedMaster.WeedName,
                                                  WeedMaster.TechnicalName,
                                                  WeedMaster.Identification,
                                                  crop.CropId,
                                                  crop.CropName,
                                                  WeedMaster.EnteredBy,
                                                  WeedMaster.EnteredDate,
                                                  WeedMaster.ChangedBy,
                                                  WeedMaster.ChangedDate,
                                                  WeedMaster.DeleteStatus,

                                              }
                                  ).ToList();
            if (weedMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in weedMasterListEntity)
            {
                var model = new WeedMasterModel();
                model.WeedId = item.WeedId;
                model.WeedName = item.WeedName;
                model.TechnicalName = item.TechnicalName;
                model.Identification = item.Identification;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                var weeddetaillist = _agriContext.WeedDetails
                                                                  .Where(x => x.WeedId == item.WeedId).ToList();
                foreach (var itemImages in weeddetaillist)
                {
                    var imgModel = new WeedDetailModel();
                    imgModel.WeedDetailId = itemImages.WeedDetailId;
                    imgModel.WeedId = model.WeedId;
                    imgModel.WeedManagement =  itemImages.WeedManagement;
                    model.Weeddetail.Add(imgModel);
                }
                var WeedImageList = _agriContext.WeedImages
                                               .Where(x => x.WeedId == item.WeedId).ToList();
                foreach (var itemImages in WeedImageList)
                {
                    var imgModel = new WeedImageModel();
                    imgModel.WeedImageId = itemImages.WeedImageId;
                    imgModel.WeedId = itemImages.WeedId;
                    imgModel.WeedImageName = itemImages.WeedImageName;
                    imgModel.WeedImageUrl =_configuration.HostName + itemImages.WeedImageUrl;
                    model.WeedMasterImages.Add(imgModel);
                }
                weedMasterModelList.Add(model);
            }
            return weedMasterModelList;

        }
        WeedMasterModel IWeedMaster.GetById(long WeedId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var weedMasterEntity = (from WeedMaster in _agriContext.WeedMasters
                                    join crop in _agriContext.CropMasters
                                       on WeedMaster.CropId equals crop.CropId

                                    where WeedMaster.DeleteStatus == false && WeedMaster.WeedId == WeedId
                                    select new
                                    {
                                        WeedMaster.WeedId,
                                        WeedMaster.WeedName,
                                        WeedMaster.TechnicalName,
                                        WeedMaster.Identification,
                                        crop.CropId,
                                        crop.CropName,
                                        WeedMaster.EnteredBy,
                                        WeedMaster.EnteredDate,
                                        WeedMaster.ChangedBy,
                                        WeedMaster.ChangedDate,
                                        WeedMaster.DeleteStatus,

                                    }
                                  ).FirstOrDefault();

            if (weedMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new WeedMasterModel();

            model.WeedId = weedMasterEntity.WeedId;
            model.WeedName = weedMasterEntity.WeedName;
            model.TechnicalName = weedMasterEntity.TechnicalName;
            model.Identification = weedMasterEntity.Identification;
            model.CropId = weedMasterEntity.CropId;
            model.CropName = weedMasterEntity.CropName;
            model.EnteredBy = weedMasterEntity.EnteredBy;
            model.EnteredDate = weedMasterEntity.EnteredDate;
            model.ChangedBy = weedMasterEntity.ChangedBy;
            model.ChangedDate = weedMasterEntity.ChangedDate;
            model.DeleteStatus = weedMasterEntity.DeleteStatus;
            var weeddetaillist = _agriContext.WeedDetails
           .Where(x => x.WeedId == weedMasterEntity.WeedId ).ToList();
            foreach (var itemImages in weeddetaillist)
            {
                var imgModel = new WeedDetailModel();
                imgModel.WeedDetailId = itemImages.WeedDetailId;
                imgModel.WeedId = model.WeedId;
                imgModel.WeedManagement = itemImages.WeedManagement;
                model.Weeddetail.Add(imgModel);
            
           
        }
            var WeedImageList = _agriContext.WeedImages
                                              .Where(x => x.WeedId == weedMasterEntity.WeedId).ToList();
            foreach (var itemImages in WeedImageList)
            {
                var imgModel = new WeedImageModel();
                imgModel.WeedImageId = itemImages.WeedImageId;
                imgModel.WeedId = itemImages.WeedId;
                imgModel.WeedImageName = itemImages.WeedImageName;
                imgModel.WeedImageUrl =_configuration.HostName + itemImages.WeedImageUrl;
                model.WeedMasterImages.Add(imgModel);
            }
            
            if (weedMasterEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string Add(WeedMasterModel model, ref ErrorResponseModel errorResponseModel)

        {

            var message = string.Empty;

            var existing = _agriContext.WeedMasters.Any(x => x.WeedId == model.WeedId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var weedEntity = new WeedMaster();

                weedEntity.WeedId = model.WeedId;
                weedEntity.WeedName = model.WeedName;
                weedEntity.TechnicalName = model.TechnicalName;
                weedEntity.Identification = model.Identification;
                weedEntity.CropId = model.CropId;
                weedEntity.EnteredBy = model.EnteredBy;
                weedEntity.EnteredDate = DateTime.Now;
                weedEntity.DeleteStatus = false;

                _agriContext.WeedMasters.Add(weedEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.Weeddetail)
                {
                    
                        var productImageEntity = new WeedDetail();
                        productImageEntity.WeedDetailId = item.WeedDetailId;
                        productImageEntity.WeedId = weedEntity.WeedId;
                        productImageEntity.WeedManagement = item.WeedManagement;
                        _agriContext.Add(productImageEntity);
                     
                }
                _agriContext.SaveChanges();
                foreach (var item1 in model.WeedMasterImages)
                {
                    var productImageEntity1 = new WeedImage();

                    productImageEntity1.WeedImageId = item1.WeedImageId;
                    productImageEntity1.WeedId = weedEntity.WeedId;
                    productImageEntity1.WeedImageName = item1.WeedImageName;
                    productImageEntity1.WeedImageUrl = item1.WeedImageUrl;

                    _agriContext.Add(productImageEntity1);
                }
                _agriContext.SaveChanges();


                message = "Data Added Successfully";
            }
            return message;
        }


        public bool Put(WeedMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var WeedId = Convert.ToInt32(model.WeedId);
           // var WeedDetailId=model.Weeddetail.Where(x=>x.WeedDetailId)
           
            var weedEntity = _agriContext.WeedMasters.FirstOrDefault(x => x.WeedId == WeedId && x.DeleteStatus==false );
            if (weedEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {


                weedEntity.WeedId = model.WeedId;
                weedEntity.WeedName = model.WeedName;
                weedEntity.TechnicalName = model.TechnicalName;
                weedEntity.Identification = model.Identification;
                weedEntity.CropId = model.CropId;
                weedEntity.EnteredBy = model.EnteredBy;
                weedEntity.EnteredDate = DateTime.Now;
                weedEntity.DeleteStatus = false;

                _agriContext.SaveChanges();

                if (model.Weeddetail.Count > 0)
                {
                    foreach (var item in model.Weeddetail)
                    {

                        if (item.WeedDetailId == 0)
                        {
                            WeedDetail weeddetailmodel = new WeedDetail();
                            weeddetailmodel.WeedManagement = item.WeedManagement;
                            weeddetailmodel.WeedId = weedEntity.WeedId;
                            _agriContext.WeedDetails.Add(weeddetailmodel);
                            _agriContext.SaveChanges();
                        }
                        else
                        {


                            var ImageEntity = new WeedDetail();
                            var wed = _agriContext.WeedDetails.FirstOrDefault(x => x.WeedDetailId == item.WeedDetailId);
                            wed.WeedDetailId = item.WeedDetailId;
                            wed.WeedId = weedEntity.WeedId;
                            wed.WeedManagement = item.WeedManagement;
                            //_agriContext.Add(ImageEntity);
                            _agriContext.SaveChanges();
                        }
                    }
                }
                if (model.WeedMasterImages.Count > 0)
                {

                    foreach (var item1 in model.WeedMasterImages)
                    {
                        var productImageEntity1 = new WeedImage();

                        productImageEntity1.WeedImageId = item1.WeedImageId;
                        productImageEntity1.WeedId = weedEntity.WeedId;
                        productImageEntity1.WeedImageName = item1.WeedImageName;
                        productImageEntity1.WeedImageUrl = item1.WeedImageUrl;

                        _agriContext.Add(productImageEntity1);
                    }
                    _agriContext.SaveChanges();
                }
            }

            return true;
        }

        public string Delete(long WeedId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            WeedMasterModel model = new WeedMasterModel();
            var cropInsectMasterEntity = _agriContext.WeedMasters.FirstOrDefault(x => x.WeedId == WeedId);
            if (cropInsectMasterEntity != null)
            {
                cropInsectMasterEntity.DeleteStatus = true;
                cropInsectMasterEntity.ChangedBy = model.ChangedBy;
                cropInsectMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public string DeleteImageFromDB(WeedMasterModel model)
        {
            var imageEntityList = _agriContext.WeedImages.Where(x => x.WeedId == model.WeedId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.WeedImages.FirstOrDefault(x => x.WeedImageId == item.WeedImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }
        public string DeleteManagement(long WeedDetailId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var weedEntity = _agriContext.WeedDetails.FirstOrDefault(x => x.WeedDetailId == WeedDetailId);
            if (weedEntity != null)
            {
                _agriContext.Remove(weedEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}
