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
    public class CropMasterService : ICropMasterService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        // IConfiguration _configuration;
        private ConfigurationModel _configuration;

        public CropMasterService(AgtonomicsAgriCultureDbContext agriContext,  IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
          //  _configuration = configuration;
            this._configuration = hostName.Value;

        }

        public List<CropMasterModel> GetAllCropMaster(CropParameters cropParameters)
        {

            var errorResponseModel = new ErrorResponseModel();
            var cropMasterModelList = new List<CropMasterModel>();
            var cropMasterListEntity = (from crop in _agriContext.CropMasters
                                                  join cropType in _agriContext.CropTypeMasters
                                                  on crop.CropTypeId equals cropType.CropTypeId
                                                  where crop.DeleteStatus == false
                                                  select new
                                                  {
                                                      crop.CropId,
                                                      crop.CropName,
                                                      crop.Description,
                                                      crop.EnteredBy,
                                                      crop.EnteredDate,
                                                      crop.ChangedBy,
                                                      crop.ChangedDate,
                                                      cropType.CropTypeId,
                                                      cropType.CropTypeName,
                                                      crop.CropImage,
                                                      crop.CropDurMinDays,
                                                      crop.CropDurMaxDays
                                                  }).Skip((cropParameters.PageNumber - 1) * cropParameters.PageSize)
        .Take(cropParameters.PageSize)
        .Distinct();
                                                           
            if (cropMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropMasterListEntity)
            {
                var model = new CropMasterModel();
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.Description = item.Description;
                model.CropTypeId = item.CropTypeId;
                model.CropTypeName = item.CropTypeName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.CropImage = _configuration.HostName + item.CropImage;
                model.CropDurMinDays = item.CropDurMinDays;
                model.CropDurMaxDays = item.CropDurMaxDays;
                cropMasterModelList.Add(model);

            }
            return cropMasterModelList;
        }

        public CropMasterModel GetCropMasterById(long CropId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropMasterEntity = (from crop in _agriContext.CropMasters
                                      join cropType in _agriContext.CropTypeMasters
                                      on crop.CropTypeId equals cropType.CropTypeId
                                      where crop.DeleteStatus == false && crop.CropId == CropId
                                      select new
                                      {
                                          crop.CropId,
                                          crop.CropName,
                                          crop.Description,
                                          crop.EnteredBy,
                                          crop.EnteredDate,
                                          crop.ChangedBy,
                                          crop.ChangedDate,
                                          crop.CropImage,
                                          cropType.CropTypeId,
                                          cropType.CropTypeName,
                                          crop.CropDurMinDays,
                                          crop.CropDurMaxDays
                                      }).FirstOrDefault();
            if (cropMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropMasterModel
            {
                CropId = cropMasterEntity.CropId,
                CropTypeId = cropMasterEntity.CropTypeId,
                CropName = cropMasterEntity.CropName,
                CropTypeName = cropMasterEntity.CropTypeName,
                Description = cropMasterEntity.Description,
                EnteredBy = cropMasterEntity.EnteredBy,
                EnteredDate = cropMasterEntity.EnteredDate,
                ChangedBy = cropMasterEntity.ChangedBy,
                ChangedDate = cropMasterEntity.ChangedDate,
                CropDurMinDays=cropMasterEntity.CropDurMinDays,
                CropDurMaxDays=cropMasterEntity.CropDurMaxDays,
                CropImage = _configuration.HostName + cropMasterEntity.CropImage,
        };
        }

        public string AddCropMaster(CropMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCrop = _agriContext.CropMasters.Any(x => x.CropId == model.CropId);
            if (existingCrop)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                CropMaster cropMasterEntity = new CropMaster();
                cropMasterEntity.CropName = model.CropName;
                cropMasterEntity.CropTypeId = model.CropTypeId;
                cropMasterEntity.Description = model.Description;
                cropMasterEntity.EnteredBy = 1;
                cropMasterEntity.EnteredDate = DateTime.Now;
                cropMasterEntity.CropImage = model.CropImage;
                cropMasterEntity.CropDurMinDays = model.CropDurMinDays;
                cropMasterEntity.CropDurMaxDays = model.CropDurMaxDays;
                cropMasterEntity.DeleteStatus = false;
                _agriContext.CropMasters.Add(cropMasterEntity);
                _agriContext.SaveChanges();

                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateCropMaster(CropMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropId = model.CropId;
            var cropMasterEntity = _agriContext.CropMasters.FirstOrDefault(x => x.CropId == CropId);
            if (cropMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropMasterEntity.CropName = model.CropName;
                cropMasterEntity.CropImage = model.CropImage;
                cropMasterEntity.CropTypeId = model.CropTypeId;
                cropMasterEntity.Description = model.Description;
                cropMasterEntity.ChangedBy = model.ChangedBy;
                cropMasterEntity.ChangedDate = DateTime.Now;
                cropMasterEntity.DeleteStatus = false;
                cropMasterEntity.CropDurMinDays = model.CropDurMinDays;
                cropMasterEntity.CropDurMaxDays = model.CropDurMaxDays;

                _agriContext.SaveChanges();
                return true;
            }
        }


        public string DeleteImageFromDB(CropMasterModel model)
        {
            var imageEntityList = _agriContext.CropMasters.FirstOrDefault(x => x.CropId == model.CropId);
            imageEntityList.CropImage = "";

//            _agriContext.Remove(imageEntityList);

           _agriContext.SaveChanges();

            return "deleted.";

        }


        public string DeleteCropMaster(long CropId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropMasterModel model = new CropMasterModel();
            var cropMasterEntity = _agriContext.CropMasters.FirstOrDefault(x => x.CropId == CropId);
            if (cropMasterEntity != null)
            {
                cropMasterEntity.DeleteStatus = true;
                cropMasterEntity.ChangedBy = model.EnteredBy;
                cropMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        CropDetailsModel ICropMasterService.GetByCropId(long CropId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropDiseaseEntity = (from crop in _agriContext.CropMasters
                                     join cropType in _agriContext.CropTypeMasters
                                     on crop.CropTypeId equals cropType.CropTypeId

                                     where crop.DeleteStatus == false && crop.CropId == CropId

                                     select new
                                     {
                                         crop.CropId,
                                         crop.CropName,
                                         crop.Description,
                                         crop.EnteredBy,
                                         crop.EnteredDate,
                                         crop.ChangedBy,
                                         crop.ChangedDate,
                                         crop.CropImage,
                                         cropType.CropTypeId,
                                         cropType.CropTypeName,
                                         crop.CropDurMinDays,
                                         crop.CropDurMaxDays
                                     }
                                 ).FirstOrDefault();

            if (cropDiseaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            var model = new CropDetailsModel();
            model.CropId = cropDiseaseEntity.CropId;
            model.CropName = cropDiseaseEntity.CropName;
            model.Description = cropDiseaseEntity.Description;
            model.CropTypeId = cropDiseaseEntity.CropTypeId;
            model.CropTypeName = cropDiseaseEntity.CropTypeName;
            model.CropImage = _configuration.HostName + cropDiseaseEntity.CropImage;
            var SeedCropList = (from Seedspacing  in _agriContext.SeedCropSpacings
                                join seed in _agriContext.SeedCropMasters
                                on Seedspacing.SeedCropId equals seed.SeedCropId
                                where seed.CropId == cropDiseaseEntity.CropId

                             
            select new
            {
                Seedspacing.SeedCropId,
                Seedspacing.RowSpacing,
                Seedspacing.PlantPopulatin,
                Seedspacing.PlantSpacing,
               
              }).ToList();
            foreach (var seedcrop in SeedCropList)
            {
                var seedcropmodel = new SeedCropDetailModel();
                seedcropmodel.SeedCropId = (int)seedcrop.SeedCropId;
                seedcropmodel.RowSpacing = ((int?)seedcrop.RowSpacing);
                seedcropmodel.PlantPopulatin = (int?)seedcrop.PlantPopulatin;
                seedcropmodel.PlantSpacing = (int?)seedcrop.PlantSpacing;
                seedcropmodel.SeedCropId = (int)seedcrop.SeedCropId;
                seedcropmodel.CropName = cropDiseaseEntity.CropName;

                model.SeedCrop.Add(seedcropmodel);
            }

            var CropirrigationList = _agriContext.CropIrrigationDetails
                                                  .Where(x => x.CropId == cropDiseaseEntity.CropId).ToList();

            foreach (var irrigation in CropirrigationList)
            {
                var irrigationmodel = new CropIrrigationDetailsModel();

                var SoilList = _agriContext.SoilTypeMasters
                                                  .Where(x => x.SoilTypeId == irrigation.SoilTypeId).FirstOrDefault();
                var stageList = _agriContext.CropStageMasters
                                                 .Where(x => x.CropStageId == irrigation.CropStageId).FirstOrDefault();

                irrigationmodel.SoilTypeId = irrigation.SoilTypeId;
                irrigationmodel.SoilTypeName = SoilList.SoilTypeName;
                irrigationmodel.CropStageId = irrigation.CropStageId;
                irrigationmodel.CropStageName = stageList.CropStageName;
                irrigationmodel.SeqNo = stageList.SeqNo;
                irrigationmodel.IrrigationDay = (int)irrigation.IrrigationDay;
                model.Irrigation.Add(irrigationmodel);
            }
            var Insectlist = _agriContext.CropInsectMasters
                                                  .Where(x => x.CropId == cropDiseaseEntity.CropId && x.DeleteStatus==false).ToList();

            foreach (var Insect in Insectlist)
            {
                var insectmodel = new CropInsectDetailsModel();
                var insectdetailList = _agriContext.CropInsectMasters
                                                .Where(x => x.CropInsectId == Insect.CropInsectId && x.DeleteStatus==false).FirstOrDefault();
                insectmodel.CropInsectId = Insect.CropInsectId;
                insectmodel.CropInsectname = insectdetailList.CropInsectName;
                insectmodel.TechnicalName = insectdetailList.TechnicalName;
                insectmodel.Identification = insectdetailList.Identification;
               
                var InsectImageList = _agriContext.InsectImages
                                .Where(x => x.CropInsectId == Insect.CropInsectId).ToList();
                foreach (var itemImages in InsectImageList)
                {
                    var imgModel = new CropInsectImagesModel();
                    imgModel.InsectImageId = itemImages.InsectImageId;
                    imgModel.CropInsectId = itemImages.CropInsectId;
                    imgModel.InsectImage1 = _configuration.HostName + itemImages.InsectImage1;
                    imgModel.ImageName = itemImages.ImageName;
                    insectmodel.InsectImage.Add(imgModel);
                }
                var insectsymptomList = _agriContext.CropInsectSymptoms
                                      .Where(x => x.CropInsectId == insectdetailList.CropInsectId).ToList();
                foreach (var item in insectsymptomList)
                {
                    var sysmptomsEntity = new CropInsectSymptomModel();
                    sysmptomsEntity.CropInsectSymptomId = item.CropInsectSymptomId;
                    sysmptomsEntity.CropInsectId = insectdetailList.CropInsectId;
                    sysmptomsEntity.Symptom = item.Symptom;
                    insectmodel.InsectSysmptoms.Add(sysmptomsEntity);
                }
                var InsectmanagementList = _agriContext.CropInsectManagements
                                   .Where(x => x.CropInsectId == insectdetailList.CropInsectId).ToList();
                foreach (var itemmanagement in InsectmanagementList)
                {
                    var managementModel = new CropInsectManagementModel();
                    managementModel.CropInsectManagementId = itemmanagement.CropInsectManagementId;
                    managementModel.CropInsectId = itemmanagement.CropInsectId;
                    managementModel.CropInsectManagement1 = itemmanagement.CropInsectManagement1;
                    insectmodel.InsectManagement.Add(managementModel);
                }
                model.CropInsect.Add(insectmodel);

            }

            var Diseaselist = _agriContext.CropDiseaseMasters
                                             .Where(x => x.CropId == cropDiseaseEntity.CropId && x.DeleteStatus == false).ToList();


            foreach (var Disease in Diseaselist)
            {
                var diseasemodel = new CropDiseaseDetailsModel();

                var desiasedetailList = _agriContext.CropDiseaseMasters
                                               .Where(x => x.CropDiseaseId == Disease.CropDiseaseId && x.DeleteStatus == false).FirstOrDefault();
                diseasemodel.CropDiseaseId = Disease.CropDiseaseId;
                diseasemodel.CropDiseaseName = desiasedetailList.CropDiseaseName;
                //diseasemodel.CropId = (int)desiasedetailList.CropId;
                //diseasemodel.CropName = cropDiseaseEntity.CropName;
                var DiseaseImageList = _agriContext.CropDiseaseImages
                                .Where(x => x.CropDiseaseId == Disease.CropDiseaseId).ToList();
                foreach (var itemImages in DiseaseImageList)
                {
                    var imgModel = new CropDiseaseImagesModel();
                    imgModel.CropDiseaseImageId = itemImages.CropDiseaseImageId;
                    imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                    imgModel.DiseaseImage = _configuration.HostName + itemImages.DiseaseImage;
                    imgModel.ImageName = itemImages.ImageName;
                    diseasemodel.DiseaseImages.Add(imgModel);
                }

                var DiseaseSymptomsList = _agriContext.CropDiseaseSymptoms
                                              .Where(x => x.CropDiseaseId == diseasemodel.CropDiseaseId).ToList();
                foreach (var itemImages in DiseaseSymptomsList)
                {
                    var imgModel = new CropDiseaseSymptomModel();
                    imgModel.CropDiseaseSymptomId = itemImages.CropDiseaseSymptomId;
                    imgModel.CropDiseaseId = itemImages.CropDiseaseId;
                    imgModel.Symptom = itemImages.Symptom;

                    diseasemodel.DiseaseSysmptoms.Add(imgModel);
                }
                var DiseaseManagementList = _agriContext.CropDiseaseManagements
                                                .Where(x => x.CropDiseaseId == diseasemodel.CropDiseaseId).ToList();
                foreach (var itemManagement in DiseaseManagementList)
                {
                    var managementModel = new CropDiseaseManagementModel();
                    managementModel.CropDiseaseManagementId = itemManagement.CropDiseaseManagementId;
                    managementModel.CropDiseaseId = itemManagement.CropDiseaseId;
                    managementModel.CropDiseaseManagement1 = itemManagement.CropDiseaseManagement1;
                    diseasemodel.DiseaseManagement.Add(managementModel);
                }
                model.CropDisease.Add(diseasemodel);
            }
            var CropLandPreparationList = _agriContext.LandPreparationDetails
                                                 .Where(x => x.CropId == cropDiseaseEntity.CropId).ToList();
            foreach (var landPreparation in CropLandPreparationList)
            {
                var landpreparationmodel = new CropLandPreparationModel();

                var SeasonList = _agriContext.SeasonMasters
                                                  .Where(x => x.SeasonId == landPreparation.SeasonId).FirstOrDefault();
                landpreparationmodel.LandPreparationId = landPreparation.LandPreparationId;
                landpreparationmodel.Title = landPreparation.Title;
                landpreparationmodel.SeqNo = landPreparation.SeqNo;
                landpreparationmodel.Description = landPreparation.Description;
                landpreparationmodel.SeasonId = (int)landPreparation.SeasonId;
                landpreparationmodel.SeasonName = SeasonList.SeasonName;
                var LandImageList = _agriContext.LandPreparationImages
                                    .Where(x => x.LandPreparationId == landPreparation.LandPreparationId).ToList();
                foreach (var itemImages in LandImageList)
                {
                    var imgModel = new LandPreparationImageModel();
                    imgModel.LandPreparationId = itemImages.LandPreparationId;
                    imgModel.LandPreparationImageId = itemImages.LandPreparationImageId;
                    imgModel.ImageName = _configuration.HostName + itemImages.ImageName;
                    landpreparationmodel.LandPreparationImages.Add(imgModel);
                }

                model.LandPreparation.Add(landpreparationmodel);
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

        public FertilizerCalculatorResultModel AddDataInCalculator(FertilizerCalculatorModel model, ref ErrorResponseModel errorResponseModel)
        {
                var message = string.Empty;
                FertilizerCalculatorModel cropMasterEntity = new FertilizerCalculatorModel();
                cropMasterEntity.CropId = model.CropId;
                cropMasterEntity.FarmArea = model.FarmArea;
                cropMasterEntity.Nitrogen = model.Nitrogen;
                cropMasterEntity.Yeild = model.Yeild;
                cropMasterEntity.Phosphorous = model.Phosphorous;
                cropMasterEntity.Potassium = model.Potassium;
                   _agriContext.SaveChanges();
                var values = _agriContext.FertCalculationFormulas.Where(x => x.CropId == cropMasterEntity.CropId).FirstOrDefault();
                return new FertilizerCalculatorResultModel
                {
                    RecommendationOfUreaForN = (((values.ConstantForN1 * cropMasterEntity.Yeild) - (values.ConstantForN2 * cropMasterEntity.Nitrogen) - values.PercentageOfNinUrea)),
                    RecommendationOfUreaForP = (((values.ConstantForP1 * cropMasterEntity.Yeild) - (values.ConstantForP2 * cropMasterEntity.Phosphorous) - values.PercentageOfPinUrea)),
                    RecommendationOfUreaForK = (((values.ConstantForK1 * cropMasterEntity.Yeild) - (values.ConstantForK2 * cropMasterEntity.Potassium) - values.PercentageOfKinUrea)),

                };
               return new FertilizerCalculatorResultModel();
        }

        // Fertilizer Formulae
        public string AddFormulae(FertCalculationFormulaModel model, ref ErrorResponseModel errorResponseModel)
        {
                var message = string.Empty;
           FertCalculationFormula fertCalculationEntity = new FertCalculationFormula();
            fertCalculationEntity.FormulaId = model.FormulaId;
            fertCalculationEntity.CropId = model.CropId;
            fertCalculationEntity.SoilTypeId = model.SoilTypeId;
            fertCalculationEntity.ConstantForN1 = model.ConstantForN1;
            fertCalculationEntity.ConstantForN2 = model.ConstantForN2;
            fertCalculationEntity.PercentageOfNinUrea = model.PercentageOfNinUrea;
            fertCalculationEntity.ConstantForP1 = model.ConstantForP1;
            fertCalculationEntity.ConstantForP2 = model.ConstantForP2;
            fertCalculationEntity.PercentageOfPinUrea = model.PercentageOfPinUrea;
            fertCalculationEntity.ConstantForK1 = model.ConstantForK1;
            fertCalculationEntity.ConstantForK2 = model.ConstantForK2;
            fertCalculationEntity.PercentageOfKinUrea = model.PercentageOfKinUrea;
            fertCalculationEntity.EnteredBy = model.EnteredBy;
            fertCalculationEntity.EnteredDate = DateTime.Now;
            fertCalculationEntity.ChangedBy = model.ChangedBy;
            fertCalculationEntity.ChangedDate = DateTime.Now;
            fertCalculationEntity.IsActive = true;
            _agriContext.FertCalculationFormulas.Add(fertCalculationEntity);
            _agriContext.SaveChanges();
            message = "Added Successfully";
            return message;
        }

        public FertCalculationFormulaModel GetByFormulaeId(long FormulaId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var formulaeEntity = (from formula in _agriContext.FertCalculationFormulas
                                    join crop in _agriContext.CropMasters
                                    on formula.CropId equals crop.CropId
                                    join soil in _agriContext.SoilTypeMasters
                                    on formula.SoilTypeId equals soil.SoilTypeId
                                    where formula.IsActive == true && formula.FormulaId == FormulaId
                                    select new
                                    {
                                        formula.FormulaId,
                                        crop.CropId,
                                        crop.CropName,
                                        formula.ConstantForP1,
                                        formula.ConstantForP2,
                                        formula.PercentageOfPinUrea,
                                        formula.ConstantForN1,
                                        formula.ConstantForN2,
                                        formula.PercentageOfNinUrea,
                                        formula.ConstantForK1,
                                        formula.ConstantForK2,
                                        formula.PercentageOfKinUrea,
                                        soil.SoilTypeId,
                                        soil.SoilTypeName
                                    }).FirstOrDefault();
            if (formulaeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new FertCalculationFormulaModel
            {
                CropId = formulaeEntity.CropId,
                FormulaId=formulaeEntity.FormulaId,
                SoilTypeId = formulaeEntity.SoilTypeId,
                CropName = formulaeEntity.CropName,
                SoilName = formulaeEntity.SoilTypeName,
                ConstantForN1 = formulaeEntity.ConstantForN1,
                ConstantForN2 = formulaeEntity.ConstantForN2,
                ConstantForP1 = formulaeEntity.ConstantForP1,
                ConstantForP2 = formulaeEntity.ConstantForP2,
                ConstantForK1 = formulaeEntity.ConstantForK1,
                ConstantForK2 = formulaeEntity.ConstantForK2,
                PercentageOfNinUrea = formulaeEntity.PercentageOfNinUrea,
                PercentageOfPinUrea = formulaeEntity.PercentageOfPinUrea,
                PercentageOfKinUrea = formulaeEntity.PercentageOfKinUrea,
            };
        }

        public bool UpdateFormulae(FertCalculationFormulaModel model, ref ErrorResponseModel errorResponseModel)
        {
            var FormulaId = model.FormulaId;
            var fertCalculationEntity = _agriContext.FertCalculationFormulas.FirstOrDefault(x => x.FormulaId == FormulaId);
            if (fertCalculationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                fertCalculationEntity.FormulaId = model.FormulaId;
                fertCalculationEntity.CropId = model.CropId;
                fertCalculationEntity.SoilTypeId = model.SoilTypeId;
                fertCalculationEntity.ConstantForN1 = model.ConstantForN1;
                fertCalculationEntity.ConstantForN2 = model.ConstantForN2;
                fertCalculationEntity.PercentageOfNinUrea = model.PercentageOfNinUrea;
                fertCalculationEntity.ConstantForP1 = model.ConstantForP1;
                fertCalculationEntity.ConstantForP2 = model.ConstantForP2;
                fertCalculationEntity.PercentageOfPinUrea = model.PercentageOfPinUrea;
                fertCalculationEntity.ConstantForK1 = model.ConstantForK1;
                fertCalculationEntity.ConstantForK2 = model.ConstantForK2;
                fertCalculationEntity.PercentageOfKinUrea = model.PercentageOfKinUrea;
                fertCalculationEntity.EnteredBy = model.EnteredBy;
                fertCalculationEntity.EnteredDate = DateTime.Now;
                fertCalculationEntity.ChangedBy = model.ChangedBy;
                fertCalculationEntity.ChangedDate = DateTime.Now;
                fertCalculationEntity.IsActive = true;
                _agriContext.SaveChanges();
                return true;
            }
        }


        public List<FertCalculationFormulaModel> GetAllFormulae()
        {
            var errorResponseModel = new ErrorResponseModel();
            var formulaeModelList = new List<FertCalculationFormulaModel>();
            var formulaeEntity = (from formula in _agriContext.FertCalculationFormulas
                                  join crop in _agriContext.CropMasters
                                  on formula.CropId equals crop.CropId
                                  join soil in _agriContext.SoilTypeMasters
                                  on formula.SoilTypeId equals soil.SoilTypeId
                                  where formula.IsActive == true 
                                  select new
                                  {
                                      formula.FormulaId,
                                      crop.CropId,
                                      crop.CropName,
                                      formula.ConstantForP1,
                                      formula.ConstantForP2,
                                      formula.PercentageOfPinUrea,
                                      formula.ConstantForN1,
                                      formula.ConstantForN2,
                                      formula.PercentageOfNinUrea,
                                      formula.ConstantForK1,
                                      formula.ConstantForK2,
                                      formula.PercentageOfKinUrea,
                                      soil.SoilTypeId,
                                      soil.SoilTypeName
                                  }).ToList();

            if (formulaeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in formulaeEntity)
            {
                var model = new FertCalculationFormulaModel();
                model.FormulaId = item.FormulaId;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SoilTypeId = item.SoilTypeId;
                model.SoilName = item.SoilTypeName;
                model.ConstantForN1 = item.ConstantForN1;
                model.ConstantForN2 = item.ConstantForN2;
                model.PercentageOfNinUrea = item.PercentageOfNinUrea;
                model.ConstantForP1 = item.ConstantForP1;
                model.ConstantForP2 = item.ConstantForP2;
                model.PercentageOfPinUrea = item.PercentageOfPinUrea;
                model.ConstantForK1 = item.ConstantForK1;
                model.ConstantForK2 = item.ConstantForK2;
                model.PercentageOfKinUrea = item.PercentageOfKinUrea;
                formulaeModelList.Add(model);
            }
            return formulaeModelList;
        }

    }
}
