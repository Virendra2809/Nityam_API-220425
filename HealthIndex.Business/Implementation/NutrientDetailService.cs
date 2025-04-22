using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class NutrientDetailService:INutrientDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public NutrientDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<NutrientDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var NutrientDetailModelList = new List<NutrientDetailModel>();
            var NutrientDetailListEntity = (from NutrientDetail in _agriContext.NutrientDetails

                                             join Crop in _agriContext.CropMasters
                                             on NutrientDetail.CropId equals Crop.CropId

                                       join Season in _agriContext.SeasonMasters
                                       on NutrientDetail.SeasonId equals Season.SeasonId

                                       join SoilType in _agriContext.SoilTypeMasters
                                       on NutrientDetail.SoilTypeId equals SoilType.SoilTypeId

                                       join Nutrient in _agriContext.NutrientMasters
                                       on NutrientDetail.NutrientId equals Nutrient.NutrientId

                                       join unit in _agriContext.UnitofMeasurementMasters
                                       on NutrientDetail.UnitId equals unit.UnitId

                                       where NutrientDetail.DeleteStatus == false
                                       select new
                                       {
                                           NutrientDetail.NutrientDetailId,
                                           NutrientDetail.QuantityInKg,
                                           Crop.CropId,
                                           Crop.CropName,
                                           Season.SeasonId,
                                           Season.SeasonName,
                                           SoilType.SoilTypeId,
                                           SoilType.SoilTypeName,
                                           Nutrient.NutrientId,
                                           Nutrient.NutrientsName,
                                           unit.UnitId,
                                           unit.UnitName,
                                           NutrientDetail.EnteredBy,
                                           NutrientDetail.EnteredDate,
                                           NutrientDetail.ChangedBy,
                                           NutrientDetail.ChangedDate,
                                           NutrientDetail.Isactive,
                                       }
                                  ).ToList();
            if (NutrientDetailListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in NutrientDetailListEntity)
            {
                var model = new NutrientDetailModel();
                model.NutrientDetailId = item.NutrientDetailId;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeasonId = item.SeasonId;
                model.SeasonName = item.SeasonName;
                model.SoilTypeId = item.SoilTypeId;
                model.SoilTypeName = item.SoilTypeName;
                model.NutrientId = item.NutrientId;
                model.NutrientsName = item.NutrientsName;
                model.UnitId = item.UnitId;
                model.UnitName = item.UnitName;
                model.QuantityInKg = item.QuantityInKg;
                model.Isactive = item.Isactive;
                NutrientDetailModelList.Add(model);
            }
            return NutrientDetailModelList;
        }

        NutrientDetailModel INutrientDetailService.GetById(long NutrientDetailIdageId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var NutrientDetailListEntity = (from NutrientDetail in _agriContext.NutrientDetails
                                            join Crop in _agriContext.CropMasters
                                            on NutrientDetail.CropId equals Crop.CropId

                                            join Season in _agriContext.SeasonMasters
                                            on NutrientDetail.SeasonId equals Season.SeasonId

                                            join SoilType in _agriContext.SoilTypeMasters
                                            on NutrientDetail.SoilTypeId equals SoilType.SoilTypeId

                                            join Nutrient in _agriContext.NutrientMasters
                                            on NutrientDetail.NutrientId equals Nutrient.NutrientId

                                            join unit in _agriContext.UnitofMeasurementMasters
                                            on NutrientDetail.UnitId equals unit.UnitId

                                            where NutrientDetail.DeleteStatus == false && NutrientDetail.NutrientDetailId== NutrientDetailIdageId
                                            select new
                                            {
                                                NutrientDetail.NutrientDetailId,
                                                NutrientDetail.QuantityInKg,
                                                Crop.CropId,
                                                Crop.CropName,
                                                Season.SeasonId,
                                                Season.SeasonName,
                                                SoilType.SoilTypeId,
                                                SoilType.SoilTypeName,
                                                Nutrient.NutrientId,
                                                Nutrient.NutrientsName,
                                                unit.UnitId,
                                                unit.UnitName,
                                                NutrientDetail.EnteredBy,
                                                NutrientDetail.EnteredDate,
                                                NutrientDetail.ChangedBy,
                                                NutrientDetail.ChangedDate,
                                                NutrientDetail.Isactive,
                                            }).FirstOrDefault();

            if (NutrientDetailListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new NutrientDetailModel
            {
                NutrientDetailId = NutrientDetailListEntity.NutrientDetailId,
                CropName = NutrientDetailListEntity.CropName,
                CropId = NutrientDetailListEntity.CropId,
                SeasonId = NutrientDetailListEntity.SeasonId,
                SeasonName = NutrientDetailListEntity.SeasonName,
                SoilTypeId = NutrientDetailListEntity.SoilTypeId,
                SoilTypeName = NutrientDetailListEntity.SoilTypeName,
                NutrientId = NutrientDetailListEntity.NutrientId,
                NutrientsName = NutrientDetailListEntity.NutrientsName,
                UnitId = NutrientDetailListEntity.UnitId,
                UnitName = NutrientDetailListEntity.UnitName,
                EnteredBy=NutrientDetailListEntity.EnteredBy,
                EnteredDate=NutrientDetailListEntity.EnteredDate,
                ChangedBy=NutrientDetailListEntity.ChangedBy,
                ChangedDate=NutrientDetailListEntity.ChangedDate,
                Isactive=NutrientDetailListEntity.Isactive,
                QuantityInKg=NutrientDetailListEntity.QuantityInKg,

            };

        }

        public string Add(NutrientDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.NutrientDetails.Any(x => x.NutrientDetailId == model.NutrientDetailId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var NutrientDetailEntity = new NutrientDetail();

                NutrientDetailEntity.NutrientDetailId = model.NutrientDetailId;
                NutrientDetailEntity.CropId = model.CropId;
                NutrientDetailEntity.SeasonId = model.SeasonId;
                NutrientDetailEntity.SoilTypeId = model.SoilTypeId;
                NutrientDetailEntity.NutrientId = model.NutrientId;
                NutrientDetailEntity.UnitId = model.UnitId;
                NutrientDetailEntity.QuantityInKg = model.QuantityInKg;
                NutrientDetailEntity.EnteredBy = model.EnteredBy;
                NutrientDetailEntity.EnteredDate = DateTime.Now;
                NutrientDetailEntity.DeleteStatus = false;
                _agriContext.NutrientDetails.Add(NutrientDetailEntity);
                _agriContext.SaveChanges();

                message = "Data Added Successfully";
            }
            return message;
        }

        public bool Put(NutrientDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var NutrientDetailId = Convert.ToInt32(model.NutrientDetailId);
            var NutrientDetailEntity = _agriContext.NutrientDetails.FirstOrDefault(x => x.NutrientDetailId == NutrientDetailId && x.DeleteStatus == false);
            if (NutrientDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                NutrientDetailEntity.NutrientDetailId = model.NutrientDetailId;
                NutrientDetailEntity.CropId = model.CropId;
                NutrientDetailEntity.SeasonId = model.SeasonId;
                NutrientDetailEntity.SoilTypeId = model.SoilTypeId;
                NutrientDetailEntity.NutrientId = model.NutrientId;
                NutrientDetailEntity.UnitId = model.UnitId;
                NutrientDetailEntity.QuantityInKg = model.QuantityInKg;
                NutrientDetailEntity.ChangedBy = model.ChangedBy;
                NutrientDetailEntity.ChangedDate = DateTime.Now;
                NutrientDetailEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
            }

            return true;
        }

        public string Delete(long NutrientDetailId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            NutrientDetailModel model = new NutrientDetailModel();
            var NutrientDetailEntity = _agriContext.NutrientDetails.FirstOrDefault(x => x.NutrientDetailId == NutrientDetailId);
            if (NutrientDetailEntity != null)
            {
                NutrientDetailEntity.DeleteStatus = true;

                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

       
    }
}





