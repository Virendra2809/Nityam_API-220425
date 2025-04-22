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
    public class CropNutritionProductMasterService : ICropNutritionProductMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropNutritionProductMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropNutritionProductMasterModel> GetAllCropNutritionProductMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropNutritionProductModelList = new List<CropNutritionProductMasterModel>();
            var cropNutritionProductListEntity = (from crop in _agriContext.CropNutritionProductMasters

                                                  join nutrituionBrand in _agriContext.CropNutritionBrandMasters
                                                  on crop.CropNutritionBrandId equals nutrituionBrand.CropNutritionBrandId

                                                  join unit in _agriContext.UnitofMeasurementMasters
                                                  on crop.UnitId equals unit.UnitId

                                                  join product in _agriContext.ProductCategoryMasters
                                                  on crop.ProductCategoryId equals product.ProductCategoryId

                                                  where crop.DeleteStatus == false
                                                  select new
                                                  {
                                                      crop.CropNutritionProductId,
                                                      crop.CropNutritionProductName,
                                                      crop.Quantity,
                                                      crop.Amount,
                                                      crop.DiscountAmount,
                                                      crop.SequenceNo,
                                                      crop.EnteredBy,
                                                      crop.EnteredDate,
                                                      crop.ChangedBy,
                                                      crop.ChangedDate,
                                                      unit.UnitId,
                                                      unit.UnitName,
                                                      nutrituionBrand.CropNutritionBrandId,
                                                      nutrituionBrand.CropNutritionBrandName,
                                                      product.ProductCategoryId,
                                                      product.ProductCategoryName
                                                  }).ToList();
            if (cropNutritionProductListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropNutritionProductListEntity)
            {
                var model = new CropNutritionProductMasterModel();
                model.CropNutritionProductId = item.CropNutritionProductId;
                model.CropNutritionProductName = item.CropNutritionProductName;
                model.CropNutritionBrandId = item.CropNutritionBrandId;
                model.UnitId = item.UnitId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.CropNutritionBrandName = item.CropNutritionBrandName;
                model.UnitName = item.UnitName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.Quantity = item.Quantity;
                model.Amount = item.Amount;
                model.DiscountAmount = item.DiscountAmount;
                model.SequenceNo = item.SequenceNo;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                cropNutritionProductModelList.Add(model);
            }
            return cropNutritionProductModelList;
        }

        public CropNutritionProductMasterModel GetCropNutritionProductMasterById(long CropNutritionProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropNutritionProductMasterEntity = (from crop in _agriContext.CropNutritionProductMasters
                                                    join nutrituionBrand in _agriContext.CropProtectionBrandMasters
                                                    on crop.CropNutritionBrandId equals nutrituionBrand.CropProtectionBrandId

                                                    join unit in _agriContext.UnitofMeasurementMasters
                                                    on crop.UnitId equals unit.UnitId

                                                    join product in _agriContext.ProductCategoryMasters
                                                    on crop.ProductCategoryId equals product.ProductCategoryId

                                                    where crop.DeleteStatus == false && crop.CropNutritionProductId == CropNutritionProductId
                                                    select new
                                                    {
                                                        crop.CropNutritionProductId,
                                                        crop.CropNutritionProductName,
                                                        crop.Quantity,
                                                        crop.Amount,
                                                        crop.DiscountAmount,
                                                        crop.SequenceNo,
                                                        crop.EnteredBy,
                                                        crop.EnteredDate,
                                                        crop.ChangedBy,
                                                        crop.ChangedDate,
                                                        unit.UnitId,
                                                        unit.UnitName,
                                                        nutrituionBrand.CropProtectionBrandId,
                                                        nutrituionBrand.CropProtectionBrandName,
                                                        crop.ProductCategoryId,
                                                        product.ProductCategoryName
                                                    }).FirstOrDefault();
            if (cropNutritionProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropNutritionProductMasterModel
            {
                CropNutritionProductId = cropNutritionProductMasterEntity.CropNutritionProductId,
                CropNutritionProductName = cropNutritionProductMasterEntity.CropNutritionProductName,
                CropNutritionBrandId = cropNutritionProductMasterEntity.CropProtectionBrandId,
                UnitId = cropNutritionProductMasterEntity.UnitId,
                ProductCategoryId = cropNutritionProductMasterEntity.ProductCategoryId,
                CropNutritionBrandName = cropNutritionProductMasterEntity.CropProtectionBrandName,
                UnitName = cropNutritionProductMasterEntity.UnitName,
                ProductCategoryName = cropNutritionProductMasterEntity.ProductCategoryName,
                Quantity = cropNutritionProductMasterEntity.Quantity,
                Amount = cropNutritionProductMasterEntity.Amount,
                DiscountAmount = cropNutritionProductMasterEntity.DiscountAmount,
                SequenceNo=cropNutritionProductMasterEntity.SequenceNo,
                EnteredBy = cropNutritionProductMasterEntity.EnteredBy,
                EnteredDate = cropNutritionProductMasterEntity.EnteredDate,
                ChangedBy = cropNutritionProductMasterEntity.ChangedBy,
                ChangedDate = cropNutritionProductMasterEntity.ChangedDate
            };
        }

        public string AddCropNutritionProductMaster(CropNutritionProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingNutritionProduct = _agriContext.CropNutritionProductMasters.Any(x => x.SequenceNo == model.SequenceNo);
            if (existingNutritionProduct)
            {
                message=GlobalConstants.ExistingSequenceNumber;
            }
            else
            {
                CropNutritionProductMaster cropNutritionProductMasterEntity = new CropNutritionProductMaster();
                cropNutritionProductMasterEntity.CropNutritionProductName = model.CropNutritionProductName;
                cropNutritionProductMasterEntity.CropNutritionBrandId = model.CropNutritionBrandId;
                cropNutritionProductMasterEntity.UnitId = model.UnitId;
                cropNutritionProductMasterEntity.ProductCategoryId = model.ProductCategoryId;
                cropNutritionProductMasterEntity.Quantity = model.Quantity;
                cropNutritionProductMasterEntity.Amount = model.Amount;
                cropNutritionProductMasterEntity.DiscountAmount = model.DiscountAmount;
                cropNutritionProductMasterEntity.SequenceNo = model.SequenceNo;
                cropNutritionProductMasterEntity.EnteredBy = model.EnteredBy;
                cropNutritionProductMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.CropNutritionProductMasters.Add(cropNutritionProductMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateCropNutritionProductMaster(CropNutritionProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropNutritionProductId = model.CropNutritionProductId;
            var cropNutritionProductMasterEntity = _agriContext.CropNutritionProductMasters.FirstOrDefault(x => x.CropNutritionProductId == CropNutritionProductId);
            if (cropNutritionProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropNutritionProductMasterEntity.CropNutritionProductName = model.CropNutritionProductName;
                cropNutritionProductMasterEntity.CropNutritionBrandId = model.CropNutritionBrandId;
                cropNutritionProductMasterEntity.UnitId = model.UnitId;
                cropNutritionProductMasterEntity.ProductCategoryId = model.ProductCategoryId;
                cropNutritionProductMasterEntity.Quantity = model.Quantity;
                cropNutritionProductMasterEntity.Amount = model.Amount;
                cropNutritionProductMasterEntity.DiscountAmount = model.DiscountAmount;
                cropNutritionProductMasterEntity.SequenceNo = model.SequenceNo;
                cropNutritionProductMasterEntity.ChangedBy = model.ChangedBy;
                cropNutritionProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteCropNutritionProductMaster(long CropNutritionProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropNutritionProductMasterModel model = new CropNutritionProductMasterModel();
            var cropNutritionProductMasterEntity = _agriContext.CropNutritionProductMasters.FirstOrDefault(x => x.CropNutritionProductId == CropNutritionProductId);
            if (cropNutritionProductMasterEntity != null)
            {
                cropNutritionProductMasterEntity.DeleteStatus = true;
                cropNutritionProductMasterEntity.ChangedBy = model.ChangedBy;
                cropNutritionProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }

}
