using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace StartUpX.Business.Implementation
{
    public class CropDiseaseMasterService : ICropDiseaseMasterService

    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        // private IConfiguration _configuration;
        private ConfigurationModel _configuration;

        public object Value => throw new NotImplementedException();
        public CropDiseaseMasterService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<CropDiseaseMasterModel> GetAllCropDisease()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropDiseaseMasterModelList = new List<CropDiseaseMasterModel>();
            var CropDiseaseMasterListEntity = (from CropDiseaseMaster in _agriContext.CropDiseaseMasters
                                               join crop in _agriContext.CropMasters
                                               on CropDiseaseMaster.CropId equals crop.CropId
                                               where CropDiseaseMaster.DeleteStatus == false
                                               select new
                                               {
                                                   CropDiseaseMaster.CropDiseaseId,
                                                   CropDiseaseMaster.CropDiseaseName,
                                                   crop.CropId,
                                                   crop.CropName,
                                                   CropDiseaseMaster.Description,
                                                   CropDiseaseMaster.Symptoms,
                                                   CropDiseaseMaster.EnteredBy,
                                                   CropDiseaseMaster.EnteredDate,
                                                   CropDiseaseMaster.ChangedBy,
                                                   CropDiseaseMaster.ChangedDate,
                                                   CropDiseaseMaster.DeleteStatus,

                                               }
                                  ).ToList();
            if (CropDiseaseMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropDiseaseMasterListEntity)
            {
                var model = new CropDiseaseMasterModel();
                model.CropDiseaseId = item.CropDiseaseId;
                model.CropDiseaseName = item.CropDiseaseName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                var DiseaseImageList = _agriContext.CropDiseaseImages
                                                   .Where(x => x.CropDiseaseId == item.CropDiseaseId).ToList();
                foreach (var itemImages in DiseaseImageList)
                {
                    var imgModel = new CropDiseaseImageModel();
                    imgModel.CropDiseaseImageId = itemImages.CropDiseaseImageId;
                    imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                    imgModel.DiseaseImage = _configuration.HostName + itemImages.DiseaseImage;
                    imgModel.ImageName = itemImages.ImageName;
                    model.DiseaseImages.Add(imgModel);
                }
                var DiseaseSymptomsList = _agriContext.CropDiseaseSymptoms
                                                 .Where(x => x.CropDiseaseId == item.CropDiseaseId).ToList();
                foreach (var itemImages in DiseaseSymptomsList)
                {
                    var imgModel = new CropDiseaseSymptomModel();
                    imgModel.CropDiseaseSymptomId = itemImages.CropDiseaseSymptomId;
                    imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                    imgModel.Symptom = itemImages.Symptom;
                    model.DiseaseSysmptoms.Add(imgModel);
                }
                var DiseaseManagementList = _agriContext.CropDiseaseManagements
                                                 .Where(x => x.CropDiseaseId == item.CropDiseaseId).ToList();
                foreach (var itemManagement in DiseaseManagementList)
                {
                    var managementModel = new CropDiseaseManagementModel();
                    managementModel.CropDiseaseManagementId = itemManagement.CropDiseaseManagementId;
                    managementModel.CropDiseaseId = itemManagement.CropDiseaseId;
                    managementModel.CropDiseaseManagement1 = itemManagement.CropDiseaseManagement1;
                    model.DiseaseManagement.Add(managementModel);
                }
                CropDiseaseMasterModelList.Add(model);
            }
            return CropDiseaseMasterModelList;
        }

        CropDiseaseMasterModel ICropDiseaseMasterService.GetById(long CropDiseaseId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropDiseaseEntity = (from CropDiseaseMaster in _agriContext.CropDiseaseMasters
                                     join crop in _agriContext.CropMasters
                                      on CropDiseaseMaster.CropId equals crop.CropId
                                     where CropDiseaseMaster.DeleteStatus == false && CropDiseaseMaster.CropDiseaseId == CropDiseaseId
                                     select new
                                     {
                                         CropDiseaseMaster.CropDiseaseId,
                                         CropDiseaseMaster.CropDiseaseName,
                                         crop.CropId,
                                         crop.CropName,
                                         CropDiseaseMaster.Description,
                                         CropDiseaseMaster.Symptoms,
                                         CropDiseaseMaster.EnteredBy,
                                         CropDiseaseMaster.EnteredDate,
                                         CropDiseaseMaster.ChangedBy,
                                         CropDiseaseMaster.ChangedDate,
                                         CropDiseaseMaster.DeleteStatus,

                                     }
                                 ).FirstOrDefault();

            if (cropDiseaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            var model = new CropDiseaseMasterModel();
            model.CropDiseaseId = cropDiseaseEntity.CropDiseaseId;
            model.CropDiseaseName = cropDiseaseEntity.CropDiseaseName;
            model.CropId = cropDiseaseEntity.CropId;
            model.CropName = cropDiseaseEntity.CropName;
            model.Description = cropDiseaseEntity.Description;
            model.EnteredBy = cropDiseaseEntity.EnteredBy;
            model.EnteredDate = cropDiseaseEntity.EnteredDate;
            model.ChangedBy = cropDiseaseEntity.ChangedBy;
            model.ChangedDate = cropDiseaseEntity.ChangedDate;
            model.DeleteStatus = cropDiseaseEntity.DeleteStatus;
            var DiseaseImageList = _agriContext.CropDiseaseImages
                                                   .Where(x => x.CropDiseaseId == cropDiseaseEntity.CropDiseaseId).ToList();
            foreach (var itemImages in DiseaseImageList)
            {
                var imgModel = new CropDiseaseImageModel();
                imgModel.CropDiseaseImageId = itemImages.CropDiseaseImageId;
                imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                imgModel.DiseaseImage = _configuration.HostName + itemImages.DiseaseImage;
                imgModel.ImageName = itemImages.ImageName;
                model.DiseaseImages.Add(imgModel);
            }
            var DiseaseSymptomsList = _agriContext.CropDiseaseSymptoms
                                               .Where(x => x.CropDiseaseId == cropDiseaseEntity.CropDiseaseId).ToList();
            foreach (var itemImages in DiseaseSymptomsList)
            {
                var imgModel = new CropDiseaseSymptomModel();
                imgModel.CropDiseaseSymptomId = itemImages.CropDiseaseSymptomId;
                imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                imgModel.Symptom = itemImages.Symptom;

                model.DiseaseSysmptoms.Add(imgModel);
            }
            var DiseaseManagementList = _agriContext.CropDiseaseManagements
                                                 .Where(x => x.CropDiseaseId == cropDiseaseEntity.CropDiseaseId).ToList();
            foreach (var itemManagement in DiseaseManagementList)
            {
                var managementModel = new CropDiseaseManagementModel();
                managementModel.CropDiseaseManagementId = itemManagement.CropDiseaseManagementId;
                managementModel.CropDiseaseId = itemManagement.CropDiseaseId;
                managementModel.CropDiseaseManagement1 = itemManagement.CropDiseaseManagement1;
                model.DiseaseManagement.Add(managementModel);
            }

            if (cropDiseaseEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string AddCropDisease(CropDiseaseMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.CropDiseaseMasters.Any(x => x.CropId == model.CropId && x.CropDiseaseName==model.CropDiseaseName);
            if (existing)
            {
                message = "Crop Name And Disease Name Data Already Exist ";
            }
            else
            {
                var CropDiseaseEntity = new CropDiseaseMaster();
                CropDiseaseEntity.CropDiseaseId = model.CropDiseaseId;
                CropDiseaseEntity.CropDiseaseName = model.CropDiseaseName;
                CropDiseaseEntity.Description = model.Description;
                CropDiseaseEntity.CropId = model.CropId;
                CropDiseaseEntity.EnteredBy = model.EnteredBy;
                CropDiseaseEntity.EnteredDate = DateTime.Now;
                CropDiseaseEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.CropDiseaseMasters.Add(CropDiseaseEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.DiseaseImages)
                {
                    var productImageEntity = new CropDiseaseImage();
                    productImageEntity.CropDiseaseImageId = item.CropDiseaseImageId;
                    productImageEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                    productImageEntity.ImageName = item.ImageName;
                    productImageEntity.DiseaseImage = item.DiseaseImage;
                    productImageEntity.EnteredDate = DateTime.UtcNow;
                    productImageEntity.EnteredBy = model.EnteredBy;
                    productImageEntity.DeleteStatus = false;
                    _agriContext.Add(productImageEntity);
                }
                _agriContext.SaveChanges();
                foreach (var item in model.DiseaseSysmptoms)
                {
                    var sysmptomsEntity = new CropDiseaseSymptom();
                    sysmptomsEntity.CropDiseaseSymptomId = item.CropDiseaseSymptomId;
                    sysmptomsEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                    sysmptomsEntity.Symptom = item.Symptom;
                    _agriContext.Add(sysmptomsEntity);
                }
                foreach (var item in model.DiseaseManagement)
                {
                    var managementEntity = new CropDiseaseManagement();
                    managementEntity.CropDiseaseManagementId = item.CropDiseaseManagementId;
                    managementEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                    managementEntity.CropDiseaseManagement1 = item.CropDiseaseManagement1;
                    _agriContext.Add(managementEntity);
                }
                _agriContext.SaveChanges();
                message = "Data Added Successfully";
            }
            return message;
        }

        public bool Put(CropDiseaseMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropDiseaseId = Convert.ToInt32(model.CropDiseaseId);
            var CropDiseaseEntity = _agriContext.CropDiseaseMasters.FirstOrDefault(x => x.CropDiseaseId == CropDiseaseId && x.CropDiseaseName==model.CropDiseaseName && !x.DeleteStatus);
            if (CropDiseaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropDiseaseEntity.CropDiseaseId = model.CropDiseaseId;
                CropDiseaseEntity.CropDiseaseName = model.CropDiseaseName;
                CropDiseaseEntity.CropId = model.CropId;
                CropDiseaseEntity.Description = model.Description;
                CropDiseaseEntity.ChangedBy = model.ChangedBy;
                CropDiseaseEntity.ChangedDate = DateTime.Now;
                CropDiseaseEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                if (model.DiseaseImages.Count > 0)
                {
                    foreach (var item in model.DiseaseImages)
                    {
                        var DiseaseImageEntity = new CropDiseaseImage();
                        DiseaseImageEntity.CropDiseaseImageId = item.CropDiseaseImageId;
                        DiseaseImageEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                        DiseaseImageEntity.ImageName = item.ImageName;
                        DiseaseImageEntity.DiseaseImage = item.DiseaseImage;
                        DiseaseImageEntity.ChangedDate = DateTime.UtcNow;
                        DiseaseImageEntity.ChangedBy = model.ChangedBy;
                        _agriContext.Add(DiseaseImageEntity);
                    }
                    _agriContext.SaveChanges();
                }
                foreach (var item in model.DiseaseSysmptoms)
                {

                    if (item.CropDiseaseSymptomId == 0)
                    {
                        CropDiseaseSymptom symptommodel = new CropDiseaseSymptom();
                        symptommodel.Symptom = item.Symptom;
                        symptommodel.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                        _agriContext.CropDiseaseSymptoms.Add(symptommodel);
                        _agriContext.SaveChanges();
                    }
                    else
                    {
                        // var sysmptomsEntity = new CropDiseaseSymptom();
                        var sysmptomsEntity = _agriContext.CropDiseaseSymptoms.FirstOrDefault(x => x.CropDiseaseSymptomId == item.CropDiseaseSymptomId);
                        sysmptomsEntity.CropDiseaseSymptomId = item.CropDiseaseSymptomId;
                        sysmptomsEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                        sysmptomsEntity.Symptom = item.Symptom;
                        // _agriContext.Add(sysmptomsEntity);
                        _agriContext.SaveChanges();

                    }
                }
                foreach (var item in model.DiseaseManagement)
                {
                    if (item.CropDiseaseManagementId == 0)
                    {
                        CropDiseaseManagement managementmodel = new CropDiseaseManagement();
                        managementmodel.CropDiseaseManagement1 = item.CropDiseaseManagement1;
                        managementmodel.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                        _agriContext.CropDiseaseManagements.Add(managementmodel);
                        _agriContext.SaveChanges();
                    }
                    else
                    {
                        // var managementEntity = new CropDiseaseManagement();
                        var managementEntity = _agriContext.CropDiseaseManagements.FirstOrDefault(x => x.CropDiseaseManagementId == item.CropDiseaseManagementId);
                        managementEntity.CropDiseaseManagementId = item.CropDiseaseManagementId;
                        managementEntity.CropDiseaseId = CropDiseaseEntity.CropDiseaseId;
                        managementEntity.CropDiseaseManagement1 = item.CropDiseaseManagement1;
                        //_agriContext.Add(managementEntity);
                        _agriContext.SaveChanges();

                    }

                }


            }

            return true;
        }

        public string Delete(long CropDiseaseId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropDiseaseMasterModel model = new CropDiseaseMasterModel();
            var cropDiseaseMasterEntity = _agriContext.CropDiseaseMasters.FirstOrDefault(x => x.CropDiseaseId == CropDiseaseId);
            if (cropDiseaseMasterEntity != null)
            {
                cropDiseaseMasterEntity.DeleteStatus = true;
                cropDiseaseMasterEntity.ChangedBy = model.ChangedBy;
                cropDiseaseMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        public string DeleteImageFromDB(CropDiseaseMasterModel model)
        {
            var imageEntityList = _agriContext.CropDiseaseImages.Where(x => x.CropDiseaseId == model.CropDiseaseId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.CropDiseaseImages.FirstOrDefault(x => x.CropDiseaseImageId == item.CropDiseaseImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }


        public string DeleteSymtom(long CropDiseaseSymptomId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var cropInsectMasterEntity = _agriContext.CropDiseaseSymptoms.FirstOrDefault(x => x.CropDiseaseSymptomId == CropDiseaseSymptomId);
            if (cropInsectMasterEntity != null)
            {
                _agriContext.Remove(cropInsectMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
        public string DeleteManagement(long CropDiseaseManagementId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var cropInsectMasterEntity = _agriContext.CropDiseaseManagements.FirstOrDefault(x => x.CropDiseaseManagementId == CropDiseaseManagementId);
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




