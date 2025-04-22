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
    public class ProductSubCategoryMasterService : IProductSubCategoryMasterService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ProductSubCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<ProductSubCategoryMasterModel> GetAllProductSubCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var subCategoryMasterModelList = new List<ProductSubCategoryMasterModel>();
            var subCategoryMasterListEntity = (from subCategory in _agriContext.ProductSubCategoryMasters
                                               join category in _agriContext.ProductCategoryMasters
                                               on subCategory.ProductCategoryId equals category.ProductCategoryId

                                               where subCategory.DeleteStatus == false
                                               select new
                                               {
                                                   subCategory.SubCategoryId,
                                                   subCategory.SubCategoryName,
                                                   subCategory.ParentSubCategoryId,
                                                   subCategory.HaschildCategory,
                                                   subCategory.Description,
                                                   subCategory.IsActive,
                                                   subCategory.SeqNo,
                                                   subCategory.EnteredBy,
                                                   subCategory.EnteredDate,
                                                   subCategory.ChangedBy,
                                                   subCategory.ChangedDate,
                                                   category.ProductCategoryId,
                                                   category.ProductCategoryName
                                               }).ToList();
            if (subCategoryMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in subCategoryMasterListEntity)
            {
                var model = new ProductSubCategoryMasterModel();
                model.SubCategoryId = item.SubCategoryId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.ParentSubCategoryId = item.ParentSubCategoryId;
                model.hasChildcategory = (bool)item.HaschildCategory;
                model.SubCategoryName = item.SubCategoryName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.IsActive = item.IsActive;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                subCategoryMasterModelList.Add(model);
            }
            return subCategoryMasterModelList;
        }



        public List<ProductSubCategoryMasterModel> GetParentCategoryList()
        {
            var errorResponseModel = new ErrorResponseModel();
            var subCategoryMasterModelList = new List<ProductSubCategoryMasterModel>();
            var subCategoryMasterListEntity = (from subCategory in _agriContext.ProductSubCategoryMasters
                                               join category in _agriContext.ProductCategoryMasters
                                               on subCategory.ProductCategoryId equals category.ProductCategoryId
                                               where subCategory.DeleteStatus==false && (subCategory.HaschildCategory == true || subCategory.HaschildCategory == false) && subCategory.ParentSubCategoryId == 0
                                              // where subCategory.DeleteStatus == false
                                               select new
                                               {
                                                   subCategory.SubCategoryId,
                                                   subCategory.SubCategoryName,
                                                   subCategory.ParentSubCategoryId,
                                                   subCategory.HaschildCategory,
                                                   subCategory.Description,
                                                   subCategory.IsActive,
                                                   subCategory.SeqNo,
                                                   subCategory.EnteredBy,
                                                   subCategory.EnteredDate,
                                                   subCategory.ChangedBy,
                                                   subCategory.ChangedDate,
                                                   category.ProductCategoryId,
                                                   category.ProductCategoryName
                                               }).ToList();
            if (subCategoryMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in subCategoryMasterListEntity)
            {
                var model = new ProductSubCategoryMasterModel();
                model.SubCategoryId = item.SubCategoryId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.ParentSubCategoryId = item.ParentSubCategoryId;
                model.hasChildcategory = (bool)item.HaschildCategory;
                model.SubCategoryName = item.SubCategoryName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.IsActive = item.IsActive;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                subCategoryMasterModelList.Add(model);
            }
            return subCategoryMasterModelList;
        }




        public ProductSubCategoryMasterModel GetProductSubCategoryMasterById(long SubCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var subCategoryMasterEntity = (from subCategory in _agriContext.ProductSubCategoryMasters
                                               join category in _agriContext.ProductCategoryMasters
                                               on subCategory.ProductCategoryId equals category.ProductCategoryId

                                               where subCategory.DeleteStatus == false && subCategory.SubCategoryId == SubCategoryId
                                               select new
                                               {
                                                   subCategory.SubCategoryId,
                                                   subCategory.SubCategoryName,
                                                   subCategory.ParentSubCategoryId,
                                                   subCategory.HaschildCategory,
                                                   subCategory.Description,
                                                   subCategory.IsActive,
                                                   subCategory.SeqNo,
                                                   subCategory.EnteredBy,
                                                   subCategory.EnteredDate,
                                                   subCategory.ChangedBy,
                                                   subCategory.ChangedDate,
                                                   category.ProductCategoryId,
                                                   category.ProductCategoryName
                                               }).FirstOrDefault();
            if (subCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ProductSubCategoryMasterModel
            {
                SubCategoryId = subCategoryMasterEntity.SubCategoryId,
                ProductCategoryId = subCategoryMasterEntity.ProductCategoryId,
                ParentSubCategoryId = subCategoryMasterEntity.ParentSubCategoryId,
                hasChildcategory= (bool)subCategoryMasterEntity.HaschildCategory,
                SubCategoryName = subCategoryMasterEntity.SubCategoryName,
                ProductCategoryName = subCategoryMasterEntity.ProductCategoryName,
                Description = subCategoryMasterEntity.Description,
                SeqNo = subCategoryMasterEntity.SeqNo,
                IsActive = subCategoryMasterEntity.IsActive,
                EnteredBy = subCategoryMasterEntity.EnteredBy,
                EnteredDate = subCategoryMasterEntity.EnteredDate,
                ChangedBy = subCategoryMasterEntity.ChangedBy,
                ChangedDate = subCategoryMasterEntity.ChangedDate
            };
        }

        public string AddProductSubCategoryMaster(ProductSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSubCategory = _agriContext.ProductSubCategoryMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingSubCategory)
            {
                return null;
            }
            else
            {
                ProductSubCategoryMaster subCategoryMasterEntity = new ProductSubCategoryMaster();
                subCategoryMasterEntity.SubCategoryName = model.SubCategoryName;
                subCategoryMasterEntity.ProductCategoryId = model.ProductCategoryId;
                subCategoryMasterEntity.ParentSubCategoryId = model.ParentSubCategoryId;
                subCategoryMasterEntity.HaschildCategory = model.hasChildcategory;
                subCategoryMasterEntity.Description = model.Description;
                subCategoryMasterEntity.SeqNo = model.SeqNo;
                subCategoryMasterEntity.IsActive = true;
                subCategoryMasterEntity.EnteredBy = model.EnteredBy;
                subCategoryMasterEntity.EnteredDate = DateTime.Now;
                subCategoryMasterEntity.DeleteStatus = false;
                //if (subCategoryMasterEntity.ParentSubCategoryId == 0)
                //{
                //    subCategoryMasterEntity.HaschildCategory = false;
                //}
                //else
                //{
                //    subCategoryMasterEntity.HaschildCategory = true;

                //}
                _agriContext.ProductSubCategoryMasters.Add(subCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateProductSubCategoryMaster(ProductSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var SubCategoryId = model.SubCategoryId;
            var subCategoryMasterEntity = _agriContext.ProductSubCategoryMasters.FirstOrDefault(x => x.SubCategoryId == SubCategoryId);
            if (subCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                subCategoryMasterEntity.SubCategoryName = model.SubCategoryName;
                subCategoryMasterEntity.ProductCategoryId = model.ProductCategoryId;
                subCategoryMasterEntity.ParentSubCategoryId = model.ParentSubCategoryId;
                subCategoryMasterEntity.HaschildCategory = model.hasChildcategory;
                subCategoryMasterEntity.Description = model.Description;
                subCategoryMasterEntity.SeqNo = model.SeqNo;
                subCategoryMasterEntity.IsActive = true;
                subCategoryMasterEntity.ChangedBy = model.ChangedBy;
                subCategoryMasterEntity.ChangedDate = DateTime.Now;
                subCategoryMasterEntity.DeleteStatus = false;
                //if (subCategoryMasterEntity.ParentSubCategoryId == 0)
                //{
                //    subCategoryMasterEntity.HaschildCategory = false;
                //}
                //else
                //{
                //    subCategoryMasterEntity.HaschildCategory = true;

                //}
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteProductSubCategoryMaster(long SubCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ProductSubCategoryMasterModel model = new ProductSubCategoryMasterModel();
            var subCategoryMasterEntity = _agriContext.ProductSubCategoryMasters.FirstOrDefault(x => x.SubCategoryId == SubCategoryId);
            if (subCategoryMasterEntity != null)
            {
                subCategoryMasterEntity.DeleteStatus = true;
                subCategoryMasterEntity.ChangedBy = model.ChangedBy;
                subCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }
}
