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
    public class CropNutritionBrandMasterService : ICropNutritionBrandMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropNutritionBrandMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<CropNutritionBrandMasterModel> GetAllCropNutritionBrandMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropNutrtionBrandModelList = new List<CropNutritionBrandMasterModel>();
            var cropNutrtionBrandListEntity = _agriContext.CropNutritionBrandMasters.Where(x => x.DeleteStatus == false).ToList();
            if (cropNutrtionBrandListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropNutrtionBrandListEntity)
            {
                var model = new CropNutritionBrandMasterModel();
                model.CropNutritionBrandId = item.CropNutritionBrandId;
                model.CropNutritionBrandName = item.CropNutritionBrandName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                cropNutrtionBrandModelList.Add(model);
            }
            return cropNutrtionBrandModelList;
        }

        public CropNutritionBrandMasterModel GetCropNutritionBrandMasterById(long CropNutritionBrandId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropNutrtionBrandMasterEntity = _agriContext.CropNutritionBrandMasters.FirstOrDefault(x => x.CropNutritionBrandId == CropNutritionBrandId && !x.DeleteStatus);
            if (cropNutrtionBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropNutritionBrandMasterModel
            {
                CropNutritionBrandName = cropNutrtionBrandMasterEntity.CropNutritionBrandName,
                Description = cropNutrtionBrandMasterEntity.Description,
                SeqNo = (int)cropNutrtionBrandMasterEntity.SeqNo,
                EnteredBy = cropNutrtionBrandMasterEntity.EnteredBy,
                EnteredDate = cropNutrtionBrandMasterEntity.EnteredDate,
                ChangedBy = cropNutrtionBrandMasterEntity.ChangedBy,
                ChangedDate = cropNutrtionBrandMasterEntity.ChangedDate
            };
        }

        public string AddCropNutritionBrandMaster(CropNutritionBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCropNutritionBrand = _agriContext.CropNutritionBrandMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingCropNutritionBrand)
            {
                return null;
            }
            else
            {
                CropNutritionBrandMaster cropNutrtionBrandMasterEntity = new CropNutritionBrandMaster();
                cropNutrtionBrandMasterEntity.CropNutritionBrandName = model.CropNutritionBrandName;
                cropNutrtionBrandMasterEntity.Description = model.Description;
                cropNutrtionBrandMasterEntity.SeqNo = model.SeqNo;
                cropNutrtionBrandMasterEntity.EnteredBy = model.EnteredBy;
                cropNutrtionBrandMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.CropNutritionBrandMasters.Add(cropNutrtionBrandMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateCropNutritionBrandMaster(CropNutritionBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var cropNutrtionBrandId = model.CropNutritionBrandId;
            var cropNutrtionBrandMasterEntity = _agriContext.CropNutritionBrandMasters.FirstOrDefault(x => x.CropNutritionBrandId == cropNutrtionBrandId);
            if (cropNutrtionBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropNutrtionBrandMasterEntity.CropNutritionBrandName = model.CropNutritionBrandName;
                cropNutrtionBrandMasterEntity.Description = model.Description;
                cropNutrtionBrandMasterEntity.SeqNo = model.SeqNo;
                cropNutrtionBrandMasterEntity.ChangedBy = model.ChangedBy;
                cropNutrtionBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteCropNutrtionBrandMaster(long CropNutritionBrandId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropNutritionBrandMasterModel model = new CropNutritionBrandMasterModel();
            var cropNutrtionBrandMasterEntity = _agriContext.CropNutritionBrandMasters.FirstOrDefault(x => x.CropNutritionBrandId == CropNutritionBrandId);
            if (cropNutrtionBrandMasterEntity != null)
            {
                cropNutrtionBrandMasterEntity.DeleteStatus = true;
                cropNutrtionBrandMasterEntity.ChangedBy = model.ChangedBy;
                cropNutrtionBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
