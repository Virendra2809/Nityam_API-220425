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
    public class CropNutrtionCategoryMasterService : ICropNutrtionCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropNutrtionCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<CropNutrtionCategoryMasterModel> GetAllCropNutrtionCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropNutrtionCategoryModelList = new List<CropNutrtionCategoryMasterModel>();
            var cropNutrtionCategoryListEntity = _agriContext.CropNutrtionCategoryMasters.Where(x => x.DeleteStatus == false).ToList();
            if (cropNutrtionCategoryListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropNutrtionCategoryListEntity)
            {
                var model = new CropNutrtionCategoryMasterModel();
                model.CropNutritionCategoryId = item.CropNutritionCategoryId;
                model.CropNutritionCategoryName = item.CropNutritionCategoryName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                cropNutrtionCategoryModelList.Add(model);
            }
            return cropNutrtionCategoryModelList;
        }

        public CropNutrtionCategoryMasterModel GetCropNutrtionCategoryMasterById(long CropNutrtionCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropNutrtionCategoryMasterEntity = _agriContext.CropNutrtionCategoryMasters.FirstOrDefault(x => x.CropNutritionCategoryId == CropNutrtionCategoryId && !x.DeleteStatus);
            if (cropNutrtionCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropNutrtionCategoryMasterModel
            {
                CropNutritionCategoryName = cropNutrtionCategoryMasterEntity.CropNutritionCategoryName,
                Description = cropNutrtionCategoryMasterEntity.Description,
                SeqNo = (int)cropNutrtionCategoryMasterEntity.SeqNo,
                EnteredBy = cropNutrtionCategoryMasterEntity.EnteredBy,
                EnteredDate = cropNutrtionCategoryMasterEntity.EnteredDate,
                ChangedBy = cropNutrtionCategoryMasterEntity.ChangedBy,
                ChangedDate = cropNutrtionCategoryMasterEntity.ChangedDate
            };
        }

        public string AddCropNutrtionCategoryMaster(CropNutrtionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCropNutritionCategory = _agriContext.CropNutrtionCategoryMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingCropNutritionCategory)
            {
                return null;
            }
            else
            {
                CropNutrtionCategoryMaster cropNutritionCategoryMasterEntity = new CropNutrtionCategoryMaster();
                cropNutritionCategoryMasterEntity.CropNutritionCategoryName = model.CropNutritionCategoryName;
                cropNutritionCategoryMasterEntity.Description = model.Description;
                cropNutritionCategoryMasterEntity.SeqNo = model.SeqNo;
                cropNutritionCategoryMasterEntity.EnteredBy = model.EnteredBy;
                cropNutritionCategoryMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.CropNutrtionCategoryMasters.Add(cropNutritionCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }


        public bool UpdateCropNutrtionCategoryMaster(CropNutrtionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var cropNutrtionCategoryId = model.CropNutritionCategoryId;
            var cropNutritionCategoryMasterEntity = _agriContext.CropNutrtionCategoryMasters.FirstOrDefault(x => x.CropNutritionCategoryId == cropNutrtionCategoryId);
            if (cropNutritionCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropNutritionCategoryMasterEntity.CropNutritionCategoryName = model.CropNutritionCategoryName;
                cropNutritionCategoryMasterEntity.Description = model.Description;
                cropNutritionCategoryMasterEntity.SeqNo = model.SeqNo;
                cropNutritionCategoryMasterEntity.ChangedBy = model.ChangedBy;
                cropNutritionCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteCropNutrtionCategoryMaster(long CropNutrtionCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropNutrtionCategoryMasterModel model = new CropNutrtionCategoryMasterModel();
            var cropNutritionCategoryMasterEntity = _agriContext.CropNutrtionCategoryMasters.FirstOrDefault(x => x.CropNutritionCategoryId == CropNutrtionCategoryId);
            if (cropNutritionCategoryMasterEntity != null)
            {
                cropNutritionCategoryMasterEntity.DeleteStatus = true;
                cropNutritionCategoryMasterEntity.ChangedBy = model.ChangedBy;
                cropNutritionCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
