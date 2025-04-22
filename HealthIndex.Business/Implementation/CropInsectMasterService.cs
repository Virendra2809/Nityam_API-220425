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
   public class CropInsectMasterService: ICropInsectMasterservice
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CropInsectMasterService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName
)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<CropInsectMasterModel> GetAllCropInspect()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropInsectMasterModelList = new List<CropInsectMasterModel>();
            var CropInsectMasterListEntity = (from CropInsectMaster in _agriContext.CropInsectMasters
                                              join crop in _agriContext.CropMasters
                                              on CropInsectMaster.CropId equals crop.CropId
                                              where CropInsectMaster.DeleteStatus==false
                                    select new
                                    {
                                        CropInsectMaster.CropInsectId,
                                        CropInsectMaster.CropInsectName,
                                        crop.CropId,
                                        crop.CropName,
                                        CropInsectMaster.Description,
                                        CropInsectMaster.TechnicalName,
                                        CropInsectMaster.Symptoms,
                                        CropInsectMaster.Identification,
                                        CropInsectMaster.EnteredBy,
                                        CropInsectMaster.EnteredDate,
                                        CropInsectMaster.ChangedBy,
                                        CropInsectMaster.ChangedDate,
                                        CropInsectMaster.DeleteStatus,

                                    }
                                  ).ToList();
            if (CropInsectMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropInsectMasterListEntity)
            {
                var model = new CropInsectMasterModel();
                model.CropInsectId = item.CropInsectId;
                model.CropInsectName = item.CropInsectName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.Description = item.Description;
                model.TechnicalName = item.TechnicalName;
                model.Symptoms = item.Symptoms;
                model.Identification = item.Identification;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                var InsectImageList = _agriContext.InsectImages
                                      .Where(x => x.CropInsectId == item.CropInsectId).ToList();
                foreach (var itemImages in InsectImageList)
                {
                    var imgModel = new CropInsectImageModel();
                    imgModel.InsectImageId = itemImages.InsectImageId;
                    imgModel.CropInsectId = itemImages.CropInsectId;
                    imgModel.InsectImage1 =_configuration.HostName + itemImages.InsectImage1;
                    imgModel.ImageName = itemImages.ImageName;
                    model.InsectImage.Add(imgModel);
                }
                var InsectsymptomsList = _agriContext.CropInsectSymptoms
                                     .Where(x => x.CropInsectId == item.CropInsectId).ToList();
                foreach (var itemImages in InsectsymptomsList)
                {
                    var imgModel = new CropInsectSymptomModel();
                    imgModel.CropInsectSymptomId = itemImages.CropInsectSymptomId;
                    imgModel.CropInsectId = itemImages.CropInsectId;
                    imgModel.Symptom = itemImages.Symptom;
                    model.InsectSysmptoms.Add(imgModel);
                }
                var InsectmanagementList = _agriContext.CropInsectManagements
                                     .Where(x => x.CropInsectId == item.CropInsectId).ToList();
                foreach (var itemmanagement in InsectmanagementList)
                {
                    var managementModel = new CropInsectManagementModel();
                    managementModel.CropInsectManagementId = itemmanagement.CropInsectManagementId;
                    managementModel.CropInsectId = itemmanagement.CropInsectId;
                    managementModel.CropInsectManagement1 = itemmanagement.CropInsectManagement1;
                    model.InsectManagement.Add(managementModel);
                }
                CropInsectMasterModelList.Add(model);
            }
            return CropInsectMasterModelList;

        }
        CropInsectMasterModel ICropInsectMasterservice.GetById(long CropInsectId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropInsectEntity = (from CropInsectMaster in _agriContext.CropInsectMasters
                                    join crop in _agriContext.CropMasters
                                    on CropInsectMaster.CropId equals crop.CropId

                                    where CropInsectMaster.DeleteStatus == false && CropInsectMaster.CropInsectId == CropInsectId
                                              select new
                                              {
                                                  CropInsectMaster.CropInsectId,
                                                  CropInsectMaster.CropInsectName,
                                                  crop.CropId,
                                                  crop.CropName,
                                                  CropInsectMaster.Description,
                                                  CropInsectMaster.TechnicalName,
                                                  CropInsectMaster.Symptoms,
                                                  CropInsectMaster.Identification,
                                                  CropInsectMaster.EnteredBy,
                                                  CropInsectMaster.EnteredDate,
                                                  CropInsectMaster.ChangedBy,
                                                  CropInsectMaster.ChangedDate,
                                                  CropInsectMaster.DeleteStatus,

                                              }
                                  ).FirstOrDefault();
             
            if (cropInsectEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new CropInsectMasterModel();

            model.CropInsectId = cropInsectEntity.CropInsectId;
            model.CropInsectName = cropInsectEntity.CropInsectName;
            model.CropId = cropInsectEntity.CropId;
            model.CropName = cropInsectEntity.CropName;
            model.Description = cropInsectEntity.Description;
            model.TechnicalName = cropInsectEntity.TechnicalName;
            model.Symptoms = cropInsectEntity.Symptoms;
            model.Identification = cropInsectEntity.Identification;
            model.EnteredBy = cropInsectEntity.EnteredBy;
            model.EnteredDate = cropInsectEntity.EnteredDate;
            model.ChangedBy = cropInsectEntity.ChangedBy;
            model.ChangedDate = cropInsectEntity.ChangedDate;
            model.DeleteStatus = cropInsectEntity.DeleteStatus;
            var InsectImageList = _agriContext.InsectImages
                                  .Where(x => x.CropInsectId == cropInsectEntity.CropInsectId).ToList();
            foreach (var itemImages in InsectImageList)
            {
                var imgModel = new CropInsectImageModel();
                imgModel.InsectImageId = itemImages.InsectImageId;
                imgModel.CropInsectId = itemImages.CropInsectId;
                imgModel.InsectImage1 = _configuration.HostName + itemImages.InsectImage1;
                imgModel.ImageName = itemImages.ImageName;
                model.InsectImage.Add(imgModel);
            }
            var InsectsymptomsList = _agriContext.CropInsectSymptoms
                                   .Where(x => x.CropInsectId == cropInsectEntity.CropInsectId).ToList();
            foreach (var itemImages in InsectsymptomsList)
            {
                var imgModel = new CropInsectSymptomModel();
                imgModel.CropInsectSymptomId = itemImages.CropInsectSymptomId;
                imgModel.CropInsectId = itemImages.CropInsectId;
                imgModel.Symptom = itemImages.Symptom;
                model.InsectSysmptoms.Add(imgModel);
            }
            var InsectmanagementList = _agriContext.CropInsectManagements
                                    .Where(x => x.CropInsectId == cropInsectEntity.CropInsectId).ToList();
            foreach (var itemmanagement in InsectmanagementList)
            {
                var managementModel = new CropInsectManagementModel();
                managementModel.CropInsectManagementId = itemmanagement.CropInsectManagementId;
                managementModel.CropInsectId = itemmanagement.CropInsectId;
                managementModel.CropInsectManagement1 = itemmanagement.CropInsectManagement1;
                model.InsectManagement.Add(managementModel);
            }
            if (cropInsectEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string AddCrop(CropInsectMasterModel model, ref ErrorResponseModel errorResponseModel)
        
        {
            var message = string.Empty;
            
            var existing = _agriContext.CropInsectMasters.Any(x => x.CropId == model.CropId && x.CropInsectName == model.CropInsectName);
            if (existing)
            {
                message = "Crop Name And Insect Name Already Exist ";
            }
            else
            {
                var CropEntity = new CropInsectMaster();
                CropEntity.CropInsectId = model.CropInsectId;
                CropEntity.CropInsectName = model.CropInsectName;
                CropEntity.CropId = model.CropId;
                CropEntity.Description = model.Description;
                CropEntity.TechnicalName = model.TechnicalName;
                CropEntity.Symptoms = model.Symptoms;
                CropEntity.Identification = model.Identification;
                CropEntity.EnteredBy = model.EnteredBy;
                CropEntity.EnteredDate = DateTime.Now;
                CropEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.CropInsectMasters.Add(CropEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.InsectImage)
                {
                    var productImageEntity = new InsectImage();
                    productImageEntity.InsectImageId = item.InsectImageId;
                    productImageEntity.CropInsectId = CropEntity.CropInsectId;
                    productImageEntity.ImageName = item.ImageName;
                    productImageEntity.InsectImage1 = item.InsectImage1;
                    productImageEntity.EnteredDate = DateTime.UtcNow;
                    productImageEntity.EnteredBy = model.EnteredBy;
                    productImageEntity.DeleteStatus = false;
                    _agriContext.Add(productImageEntity);
                }
                _agriContext.SaveChanges();

                foreach (var item in model.InsectSysmptoms)
                {
                    var sysmptomsEntity = new CropInsectSymptom();
                    sysmptomsEntity.CropInsectSymptomId = item.CropInsectSymptomId;
                    sysmptomsEntity.CropInsectId = CropEntity.CropInsectId;
                    sysmptomsEntity.Symptom = item.Symptom;
                    _agriContext.Add(sysmptomsEntity);
                }
                foreach (var item in model.InsectManagement)
                {
                    var managementEntity = new CropInsectManagement();
                    managementEntity.CropInsectManagementId = item.CropInsectManagementId;
                    managementEntity.CropInsectId = CropEntity.CropInsectId;
                    managementEntity.CropInsectManagement1 = item.CropInsectManagement1;
                    _agriContext.Add(managementEntity);
                }
                _agriContext.SaveChanges();
                message = "Data Added Successfully";
            }
            return message;
        }


        public bool Put(CropInsectMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropInsectId = Convert.ToInt32(model.CropInsectId);
            var CropEntity = _agriContext.CropInsectMasters.FirstOrDefault(x => x.CropInsectId == CropInsectId && x.CropInsectName==model.CropInsectName && !x.DeleteStatus);
            if (CropEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                CropEntity.CropInsectId = model.CropInsectId;
                CropEntity.CropInsectName = model.CropInsectName;
                CropEntity.CropId = model.CropId;
                CropEntity.Description = model.Description;
                CropEntity.TechnicalName = model.TechnicalName;
                CropEntity.Symptoms = model.Symptoms;
                CropEntity.Identification = model.Identification;
                CropEntity.ChangedBy = model.ChangedBy;
                CropEntity.ChangedDate = DateTime.Now;
                CropEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();

                if (model.InsectImage.Count > 0)
                {
                    foreach (var item in model.InsectImage)
                    {
                        var InsectImageEntity = new InsectImage();

                        InsectImageEntity.InsectImageId = item.InsectImageId;
                        InsectImageEntity.CropInsectId = CropEntity.CropInsectId;
                        InsectImageEntity.ImageName = item.ImageName;
                        InsectImageEntity.InsectImage1 = item.InsectImage1;
                        InsectImageEntity.EnteredDate = DateTime.UtcNow;
                        InsectImageEntity.EnteredBy = model.EnteredBy;
                        _agriContext.Add(InsectImageEntity);

                    }
                    _agriContext.SaveChanges();
                }
                foreach (var item in model.InsectSysmptoms)
                {

                    if (item.CropInsectSymptomId == 0)
                    {
                        CropInsectSymptom symptommodel = new CropInsectSymptom();
                        symptommodel.Symptom = item.Symptom;
                        symptommodel.CropInsectId = CropEntity.CropInsectId;
                        _agriContext.CropInsectSymptoms.Add(symptommodel);
                        _agriContext.SaveChanges();
                    }
                    else


                    {
                        var sysmptomsEntity = _agriContext.CropInsectSymptoms.FirstOrDefault(x => x.CropInsectSymptomId == item.CropInsectSymptomId);
                        sysmptomsEntity.CropInsectSymptomId = item.CropInsectSymptomId;
                        sysmptomsEntity.CropInsectId = CropEntity.CropInsectId;
                        sysmptomsEntity.Symptom = item.Symptom;
                        _agriContext.SaveChanges();

                    }
                    //_agriContext.Add(sysmptomsEntity
                    //

                }
                foreach (var item in model.InsectManagement)
                {
                    if (item.CropInsectManagementId == 0)
                    {
                        CropInsectManagement managementmodel = new CropInsectManagement();
                        managementmodel.CropInsectManagement1 = item.CropInsectManagement1;
                        managementmodel.CropInsectId = CropEntity.CropInsectId;
                        _agriContext.CropInsectManagements.Add(managementmodel);
                        _agriContext.SaveChanges();
                    }
                    else
                    {
                        //var managementEntity = new CropInsectManagement();
                        var managementEntity = _agriContext.CropInsectManagements.FirstOrDefault(x => x.CropInsectManagementId == item.CropInsectManagementId);

                        managementEntity.CropInsectManagementId = item.CropInsectManagementId;
                        managementEntity.CropInsectId = CropEntity.CropInsectId;
                        managementEntity.CropInsectManagement1 = item.CropInsectManagement1;
                        _agriContext.SaveChanges();
                    }
                    // _agriContext.Add(managementEntity);
                }
            }

            

            return true;
        }

        public string Delete(long CropInsectId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropInsectMasterModel model = new CropInsectMasterModel ();
            var cropInsectMasterEntity = _agriContext.CropInsectMasters.FirstOrDefault(x => x.CropInsectId == CropInsectId);
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


        public string DeleteImageFromDB(CropInsectMasterModel model)
        {
            var imageEntityList = _agriContext.InsectImages.Where(x => x.CropInsectId == model.CropInsectId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.InsectImages.FirstOrDefault(x => x.InsectImageId == item.InsectImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }

        public string DeleteSysmtom(long CropInsectSymptomId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropInsectSymptom model = new CropInsectSymptom();
            var cropInsectMasterEntity = _agriContext.CropInsectSymptoms.FirstOrDefault(x => x.CropInsectSymptomId == CropInsectSymptomId);
            if (cropInsectMasterEntity != null)
            {
                _agriContext.Remove(cropInsectMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
        public string DeleteManagement(long CropInsectManagementId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            //CropInsectSymptom model = new CropInsectSymptom();
            var cropInsectMasterEntity = _agriContext.CropInsectManagements.FirstOrDefault(x => x.CropInsectManagementId == CropInsectManagementId);
            if (cropInsectMasterEntity != null)
            {
                _agriContext.Remove(cropInsectMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}
