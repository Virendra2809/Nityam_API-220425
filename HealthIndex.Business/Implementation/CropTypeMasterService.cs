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
    public class CropTypeMasterService : ICropTypeMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropTypeMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }
        public List<CropTypeMasterModel> GetAllCropTypeMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var pcropModelList = new List<CropTypeMasterModel>();
            var cropListEntity = _agriContext.CropTypeMasters.Where(x => x.DeleteStatus == false).ToList();
            if (cropListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropListEntity)
            {
                var model = new CropTypeMasterModel();
                model.CropTypeId = item.CropTypeId;
                model.CropTypeName = item.CropTypeName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                pcropModelList.Add(model);
            }
            return pcropModelList;
        }

        public CropTypeMasterModel GetCropTypeMasterById(long CropTypeId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropTypeEntity = _agriContext.CropTypeMasters.FirstOrDefault(x => x.CropTypeId == CropTypeId && x.DeleteStatus == false);
            if (cropTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropTypeMasterModel
            {
                CropTypeName = cropTypeEntity.CropTypeName,
                Description = cropTypeEntity.Description,
                EnteredBy = cropTypeEntity.EnteredBy,
                EnteredDate = cropTypeEntity.EnteredDate,
                ChangedBy = cropTypeEntity.ChangedBy,
                ChangedDate = cropTypeEntity.ChangedDate
            };
        }

        public string AddCropTypeMaster(CropTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCrop = _agriContext.CropTypeMasters.Any(x => x.CropTypeId == model.CropTypeId);
            if (existingCrop)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                CropTypeMaster cropMasterEntity = new CropTypeMaster();
                cropMasterEntity.CropTypeId = model.CropTypeId;
                cropMasterEntity.CropTypeName = model.CropTypeName;
                cropMasterEntity.Description = model.Description;
                cropMasterEntity.EnteredBy = model.EnteredBy;
                cropMasterEntity.EnteredDate = DateTime.Now;
                cropMasterEntity.DeleteStatus = false;
                _agriContext.CropTypeMasters.Add(cropMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateCropTypeMaster(CropTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropTypeId = model.CropTypeId;
            var cropMasterEntity = _agriContext.CropTypeMasters.FirstOrDefault(x => x.CropTypeId == CropTypeId);
            if (cropMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropMasterEntity.CropTypeName = model.CropTypeName;
                cropMasterEntity.Description = model.Description;
                cropMasterEntity.ChangedBy = model.ChangedBy;
                cropMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteCropTypeMaster(long CropTypeId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropTypeMasterModel model = new CropTypeMasterModel();
            var cropMasterEntity = _agriContext.CropTypeMasters.FirstOrDefault(x => x.CropTypeId == CropTypeId);
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

    }
}
