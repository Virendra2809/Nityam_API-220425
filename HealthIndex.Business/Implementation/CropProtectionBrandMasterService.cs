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
    public class CropProtectionBrandMasterService : ICropProtectionBrandMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropProtectionBrandMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }
        public List<CropProtectionBrandMasterModel> GetAllCropProtectionBrandMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropProtectionBrandModelList = new List<CropProtectionBrandMasterModel>();
            var cropProtectionBrandMasterListEntity = _agriContext.CropProtectionBrandMasters.Where(x => x.DeleteStatus == false).ToList();
            if (cropProtectionBrandMasterListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropProtectionBrandMasterListEntity)
            {
                var model = new CropProtectionBrandMasterModel();
                model.CropProtectionBrandId = item.CropProtectionBrandId;
                model.CropProtectionBrandName = item.CropProtectionBrandName;
                model.Description = item.Description;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate =item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                cropProtectionBrandModelList.Add(model);
            }
            return cropProtectionBrandModelList;
        }

        public CropProtectionBrandMasterModel GetCropProtectionBrandMasterById(long CropProtectionBrandId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropProtectionMasterEntity = _agriContext.CropProtectionBrandMasters.FirstOrDefault(x => x.CropProtectionBrandId == CropProtectionBrandId && !x.DeleteStatus);
            if (cropProtectionMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropProtectionBrandMasterModel
            {
                CropProtectionBrandName = cropProtectionMasterEntity.CropProtectionBrandName,
                Description = cropProtectionMasterEntity.Description,
                SeqNo = (int)cropProtectionMasterEntity.SeqNo,
                EnteredBy = cropProtectionMasterEntity.EnteredBy,
                EnteredDate = cropProtectionMasterEntity.EnteredDate,
                ChangedBy = cropProtectionMasterEntity.ChangedBy,
                ChangedDate = cropProtectionMasterEntity.ChangedDate,
            };
        }

        public string AddCropProtectionBrandMaster(CropProtectionBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCropProtectionBrand = _agriContext.CropProtectionBrandMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingCropProtectionBrand)
            {
                return null;
            }
            else
            {
                CropProtectionBrandMaster cropProtectionMasterEntity = new CropProtectionBrandMaster();
                cropProtectionMasterEntity.CropProtectionBrandName = model.CropProtectionBrandName;
                cropProtectionMasterEntity.Description = model.Description;
                cropProtectionMasterEntity.SeqNo = model.SeqNo;
                cropProtectionMasterEntity.EnteredBy = model.EnteredBy;
                cropProtectionMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.CropProtectionBrandMasters.Add(cropProtectionMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateCropProtectionBrandMaster(CropProtectionBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var cropProtectionBrandId = model.CropProtectionBrandId;
            var cropProtectionMasterEntity = _agriContext.CropProtectionBrandMasters.FirstOrDefault(x => x.CropProtectionBrandId == cropProtectionBrandId);
            if (cropProtectionMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropProtectionMasterEntity.CropProtectionBrandName = model.CropProtectionBrandName;
                cropProtectionMasterEntity.Description = model.Description;
                cropProtectionMasterEntity.SeqNo = model.SeqNo;
                cropProtectionMasterEntity.ChangedBy = model.ChangedBy;
                cropProtectionMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteCropProtectionBrandMasterr(long CropProtectionBrandId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedBrandMasterModel model = new SeedBrandMasterModel();
            var cropProtectionMasterEntity = _agriContext.CropProtectionBrandMasters.FirstOrDefault(x => x.CropProtectionBrandId == CropProtectionBrandId);
            if (cropProtectionMasterEntity != null)
            {
                cropProtectionMasterEntity.DeleteStatus = true;
                cropProtectionMasterEntity.ChangedBy = model.ChangedBy;
                cropProtectionMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }
}
