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
    public class NutrientDeficencyDetailService: INutrientDeficencyDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public object Value => throw new NotImplementedException();
        public NutrientDeficencyDetailService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName
 )
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;
        }

        public List<NutrientDeficencyDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var NutrientDeficencyModelList = new List<NutrientDeficencyDetailModel>();
            var NutrientDeficencyListEntity = (from NutrientDeficencyDetail in _agriContext.NutrientDeficencyDetails
                                               join NutrientMaster in _agriContext.NutrientMasters
                                               on NutrientDeficencyDetail.NutrientId equals NutrientMaster.NutrientId
                                               join crop in _agriContext.CropMasters
                                             on NutrientDeficencyDetail.CropId equals crop.CropId

                                               where NutrientDeficencyDetail.DeleteStatus == false
                                               select new
                                               {
                                                   NutrientDeficencyDetail.NutrientDeficencyId,
                                                   NutrientMaster.NutrientId,
                                                   NutrientMaster.NutrientsName,
                                                   crop.CropId,
                                                   crop.CropName,
                                                   NutrientDeficencyDetail.DeleteStatus,
                                                  
                                               }
                                  ).ToList();
            if (NutrientDeficencyListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in NutrientDeficencyListEntity)
            {
                var model = new NutrientDeficencyDetailModel();
                model.NutrientDeficencyId = item.NutrientDeficencyId;
                model.NutrientId = item.NutrientId;
                model.NutrientsName = item.NutrientsName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                
                model.DeleteStatus = item.DeleteStatus;
                var ImageList = _agriContext.NutrientDeficencyImages
                                                   .Where(x => x.NutrientDeficencyId == item.NutrientDeficencyId).ToList();
                foreach (var itemImages in ImageList)
                {
                    var imgModel = new NutrientDeficencyImageModel();
                    imgModel.NutrientDeficencyImageId = itemImages.NutrientDeficencyImageId;
                    imgModel.NutrientDeficencyId = itemImages.NutrientDeficencyId;
                    imgModel.ImageUrl =_configuration.HostName + itemImages.ImageUrl;
                    imgModel.ImageName = itemImages.ImageName;
                    model.NutrientImages.Add(imgModel);
                }
                var NutrientSymptomsList = _agriContext.NutrientDeficencySymptoms
                                                .Where(x => x.NutrientDeficencyId == item.NutrientDeficencyId).ToList();
                foreach (var symptom in NutrientSymptomsList)
                {
                    var symptommodel = new NutrientDeficencySymptomModel();
                    symptommodel.NutrientDeficencySymptomId = symptom.NutrientDeficencySymptomId;
                    symptommodel.NutrientDeficencyId = symptom.NutrientDeficencyId;
                    symptommodel.Symptom = symptom.Symptom;
                    model.NutrientDeficencySymptom.Add(symptommodel);
                }
                var NutrientManagementList = _agriContext.NutrientDeficencyManagements
                                                 .Where(x => x.NutrientDeficencyId == item.NutrientDeficencyId).ToList();
                foreach (var itemManagement in NutrientManagementList)
                {
                    var managementModel = new NutrientDeficencyManagementmodel();
                    managementModel.NutrientDeficencyManagementId = itemManagement.NutrientDeficencyManagementId;
                    managementModel.NutrientDeficencyId = itemManagement.NutrientDeficencyId;
                    managementModel.NutrientManagement = itemManagement.NutrientManagement;
                    model.NutrientDeficencyManagement.Add(managementModel);
                }
                NutrientDeficencyModelList.Add(model);
            }
            return NutrientDeficencyModelList;
        }

        NutrientDeficencyDetailModel INutrientDeficencyDetailService.GetById(long NutrientDeficencyId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var nutrientDeficencyEntity = (from NutrientDeficencyDetail in _agriContext.NutrientDeficencyDetails
                                           join NutrientMaster in _agriContext.NutrientMasters
                                            on NutrientDeficencyDetail.NutrientId equals NutrientMaster.NutrientId
                                           join crop in _agriContext.CropMasters
                                           on NutrientDeficencyDetail.CropId equals crop.CropId

                                           where NutrientDeficencyDetail.DeleteStatus == false && NutrientDeficencyDetail.NutrientDeficencyId == NutrientDeficencyId
                                           select new
                                     {
                                               NutrientDeficencyDetail.NutrientDeficencyId,
                                               NutrientMaster.NutrientId,
                                               NutrientMaster.NutrientsName,
                                               crop.CropId,
                                               crop.CropName,
                                               NutrientDeficencyDetail.DeleteStatus,

                                           }
                                 ).FirstOrDefault();

            if (nutrientDeficencyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            var model = new NutrientDeficencyDetailModel();
            model.NutrientDeficencyId = nutrientDeficencyEntity.NutrientDeficencyId;
            model.NutrientId = nutrientDeficencyEntity.NutrientId;
            model.NutrientsName = nutrientDeficencyEntity.NutrientsName;
            model.CropId = nutrientDeficencyEntity.CropId;
            model.CropName = nutrientDeficencyEntity.CropName;
           
            model.DeleteStatus = nutrientDeficencyEntity.DeleteStatus;
            var ImageList = _agriContext.NutrientDeficencyImages
                                                  .Where(x => x.NutrientDeficencyId == nutrientDeficencyEntity.NutrientDeficencyId).ToList();
            foreach (var itemImages in ImageList)
            {
                var imgModel = new NutrientDeficencyImageModel();
                imgModel.NutrientDeficencyImageId = itemImages.NutrientDeficencyImageId;
                imgModel.NutrientDeficencyId = itemImages.NutrientDeficencyId;
                imgModel.ImageUrl = _configuration.HostName + itemImages.ImageUrl;
                imgModel.ImageName = itemImages.ImageName;
                model.NutrientImages.Add(imgModel);
            }
            var NutrientSymptomsList = _agriContext.NutrientDeficencySymptoms
                                                .Where(x => x.NutrientDeficencyId == nutrientDeficencyEntity.NutrientDeficencyId).ToList();
            foreach (var symptom in NutrientSymptomsList)
            {
                var symptommodel = new NutrientDeficencySymptomModel();
                symptommodel.NutrientDeficencySymptomId = symptom.NutrientDeficencySymptomId;
                symptommodel.NutrientDeficencyId = symptom.NutrientDeficencyId;
                symptommodel.Symptom = symptom.Symptom;
                model.NutrientDeficencySymptom.Add(symptommodel);
            }
            var NutrientManagementList = _agriContext.NutrientDeficencyManagements
                                             .Where(x => x.NutrientDeficencyId == nutrientDeficencyEntity.NutrientDeficencyId).ToList();
            foreach (var itemManagement in NutrientManagementList)
            {
                var managementModel = new NutrientDeficencyManagementmodel();
                managementModel.NutrientDeficencyManagementId = itemManagement.NutrientDeficencyManagementId;
                managementModel.NutrientDeficencyId = itemManagement.NutrientDeficencyId;
                managementModel.NutrientManagement = itemManagement.NutrientManagement;
                model.NutrientDeficencyManagement.Add(managementModel);
            }
            if (nutrientDeficencyEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        public string Add(NutrientDeficencyDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.NutrientDeficencyDetails.Any(x => x.NutrientId == model.NutrientId && x.CropId==model.CropId);
            if (existing)
            {
                message = GlobalConstants.ExistingName;
            }
            else
            {
                var nutrientDeficencyEntity = new NutrientDeficencyDetail();

                nutrientDeficencyEntity.NutrientDeficencyId = model.NutrientDeficencyId;
                nutrientDeficencyEntity.NutrientId = model.NutrientId;
                nutrientDeficencyEntity.CropId = model.CropId;
                              nutrientDeficencyEntity.DeleteStatus = false;
              
                _agriContext.NutrientDeficencyDetails.Add(nutrientDeficencyEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.NutrientImages)
                {
                    var ImageEntity = new NutrientDeficencyImage();

                    ImageEntity.NutrientDeficencyImageId = item.NutrientDeficencyImageId;
                    ImageEntity.NutrientDeficencyId = nutrientDeficencyEntity.NutrientDeficencyId;
                    ImageEntity.ImageName =  item.ImageName;
                    ImageEntity.ImageUrl = item.ImageUrl;
                    _agriContext.Add(ImageEntity);
                }
                _agriContext.SaveChanges();
                foreach (var item in model.NutrientDeficencySymptom)
                {
                    var sysmptomsEntity = new NutrientDeficencySymptom();

                    sysmptomsEntity.NutrientDeficencySymptomId = item.NutrientDeficencySymptomId;
                    sysmptomsEntity.NutrientDeficencyId = nutrientDeficencyEntity.NutrientDeficencyId;
                    sysmptomsEntity.Symptom = item.Symptom;
                    _agriContext.Add(sysmptomsEntity);
                }
                _agriContext.SaveChanges();
                foreach (var item in model.NutrientDeficencyManagement)
                {
                    var managementEntity = new NutrientDeficencyManagement();
                    managementEntity.NutrientDeficencyManagementId = item.NutrientDeficencyManagementId;
                    managementEntity.NutrientDeficencyId = nutrientDeficencyEntity.NutrientDeficencyId;
                    managementEntity.NutrientManagement = item.NutrientManagement;
                    _agriContext.Add(managementEntity);
                }
                _agriContext.SaveChanges();

                message = "Data Added Successfully";
            }
            return message;
        }

        public bool Put(NutrientDeficencyDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var NutrientDeficencyId = Convert.ToInt32(model.NutrientDeficencyId);
            var NutrientDeficencyEntity = _agriContext.NutrientDeficencyDetails.FirstOrDefault(x => x.NutrientDeficencyId == NutrientDeficencyId && x.DeleteStatus==false);
            if (NutrientDeficencyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                NutrientDeficencyEntity.NutrientDeficencyId = model.NutrientDeficencyId;
                NutrientDeficencyEntity.NutrientId = model.NutrientId;
                NutrientDeficencyEntity.CropId = model.CropId;
                NutrientDeficencyEntity.DeleteStatus = false;
                _agriContext.SaveChanges();


                if (model.NutrientImages.Count > 0)
                {
                    foreach (var item in model.NutrientImages)
                    {
                        var imgModel = new NutrientDeficencyImage();
                        imgModel.NutrientDeficencyImageId = item.NutrientDeficencyImageId;
                        imgModel.NutrientDeficencyId = NutrientDeficencyEntity.NutrientDeficencyId;
                        imgModel.ImageUrl = item.ImageUrl;
                        imgModel.ImageName = item.ImageName;
                        _agriContext.Add(imgModel);

                    }
                    _agriContext.SaveChanges();
                }

                foreach (var item in model.NutrientDeficencySymptom)
                {

                    if (item.NutrientDeficencySymptomId == 0)
                    {
                        NutrientDeficencySymptom symptommodel = new NutrientDeficencySymptom();
                        symptommodel.Symptom = item.Symptom;
                        symptommodel.NutrientDeficencyId = NutrientDeficencyEntity.NutrientDeficencyId;
                        _agriContext.NutrientDeficencySymptoms.Add(symptommodel);
                        _agriContext.SaveChanges();
                    }
                    else
                    {
                        var sysmptomsEntity = _agriContext.NutrientDeficencySymptoms.FirstOrDefault(x => x.NutrientDeficencySymptomId == item.NutrientDeficencySymptomId);
                        sysmptomsEntity.NutrientDeficencySymptomId = item.NutrientDeficencySymptomId;
                        sysmptomsEntity.NutrientDeficencyId = NutrientDeficencyEntity.NutrientDeficencyId;
                        sysmptomsEntity.Symptom = item.Symptom;
                           _agriContext.SaveChanges();

                    }
                }
                foreach (var item in model.NutrientDeficencyManagement)
                {
                    if (item.NutrientDeficencyManagementId == 0)
                    {
                        NutrientDeficencyManagement managementmodel = new NutrientDeficencyManagement();
                        managementmodel.NutrientManagement = item.NutrientManagement;
                        managementmodel.NutrientDeficencyId = NutrientDeficencyEntity.NutrientDeficencyId;
                        _agriContext.NutrientDeficencyManagements.Add(managementmodel);
                        _agriContext.SaveChanges();
                    }
                    else
                    {
                        var managementEntity = _agriContext.NutrientDeficencyManagements.FirstOrDefault(x => x.NutrientDeficencyManagementId == item.NutrientDeficencyManagementId);
                        managementEntity.NutrientDeficencyManagementId = item.NutrientDeficencyManagementId;
                        managementEntity.NutrientDeficencyId = NutrientDeficencyEntity.NutrientDeficencyId;
                        managementEntity.NutrientManagement = item.NutrientManagement;
                        _agriContext.SaveChanges();

                    }

                }


            }

            return true;
        }

        public string Delete(long NutrientDeficencyId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            NutrientDeficencyDetailModel model = new NutrientDeficencyDetailModel();
            var NutrientDeficencyEntity = _agriContext.NutrientDeficencyDetails.FirstOrDefault(x => x.NutrientDeficencyId == NutrientDeficencyId);
            if (NutrientDeficencyEntity != null)
            {
                NutrientDeficencyEntity.DeleteStatus = true;
               _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public string DeleteImageFromDB(NutrientDeficencyDetailModel model)
        {
            var imageEntityList = _agriContext.NutrientDeficencyImages.Where(x => x.NutrientDeficencyId == model.NutrientDeficencyId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.NutrientDeficencyImages.FirstOrDefault(x => x.NutrientDeficencyImageId == item.NutrientDeficencyImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }

        public string DeleteSymptom(long NutrientDeficencySymptomId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var symptomMasterEntity = _agriContext.NutrientDeficencySymptoms.FirstOrDefault(x => x.NutrientDeficencySymptomId == NutrientDeficencySymptomId);
            if (symptomMasterEntity != null)
            {
                _agriContext.Remove(symptomMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
        public string DeleteManagement(long NutrientDeficencyManagementId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var managementMasterEntity = _agriContext.NutrientDeficencyManagements.FirstOrDefault(x => x.NutrientDeficencyManagementId == NutrientDeficencyManagementId);
            if (managementMasterEntity != null)
            {
                _agriContext.Remove(managementMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}





