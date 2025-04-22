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
    public class ProductBrandMasterService : IProductBrandMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ProductBrandMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }  

        public List<ProductBrandMasterModel> GetAllProductBrandMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var subBrandMasterModelList = new List<ProductBrandMasterModel>();
            var subBandMasterListEntity = (from subBrand in _agriContext.ProductBrandMasters
                                               join category in _agriContext.ProductCategoryMasters
                                               on subBrand.ProductCategoryId equals category.ProductCategoryId

                                               where subBrand.DeleteStatus == false
                                               select new
                                               {
                                                   subBrand.ProductBrandId,
                                                   subBrand.ProductBrandName,
                                                   subBrand.Description,
                                                   subBrand.SeqNo,
                                                   subBrand.EnteredBy,
                                                   subBrand.EnteredDate,
                                                   subBrand.ChangedBy,
                                                   subBrand.ChangedDate,
                                                   category.ProductCategoryId,
                                                   category.ProductCategoryName
                                               }).ToList();
            if (subBandMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in subBandMasterListEntity)
            {
                var model = new ProductBrandMasterModel();
                model.ProductBrandId = item.ProductBrandId;
                model.ProductBrandName = item.ProductBrandName;
                model.ProductCategoryId = item.ProductCategoryId;
                model.ProductCategoryName = item.ProductCategoryName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                subBrandMasterModelList.Add(model);
            }
            return subBrandMasterModelList;
        }

        public ProductBrandMasterModel GetProductBrandMasterById(long ProductBrandId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var subBandMasterEntity = (from subBrand in _agriContext.ProductBrandMasters
                                           join category in _agriContext.ProductCategoryMasters
                                           on subBrand.ProductCategoryId equals category.ProductCategoryId

                                           where subBrand.DeleteStatus == false && subBrand.ProductBrandId==ProductBrandId
                                           select new
                                           {
                                               subBrand.ProductBrandId,
                                               subBrand.ProductBrandName,
                                               subBrand.Description,
                                               subBrand.SeqNo,
                                               subBrand.EnteredBy,
                                               subBrand.EnteredDate,
                                               subBrand.ChangedBy,
                                               subBrand.ChangedDate,
                                               category.ProductCategoryId,
                                               category.ProductCategoryName
                                           }).FirstOrDefault();
            if (subBandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ProductBrandMasterModel
            {
                ProductBrandId = subBandMasterEntity.ProductBrandId,
                ProductBrandName = subBandMasterEntity.ProductBrandName,
                ProductCategoryId = subBandMasterEntity.ProductCategoryId,
                ProductCategoryName = subBandMasterEntity.ProductCategoryName,
                Description = subBandMasterEntity.Description,
                SeqNo = subBandMasterEntity.SeqNo,
                EnteredBy = subBandMasterEntity.EnteredBy,
                EnteredDate = subBandMasterEntity.EnteredDate,
                ChangedBy = subBandMasterEntity.ChangedBy,
                ChangedDate = subBandMasterEntity.ChangedDate
            };
        }

        public string AddProductBrandMaster(ProductBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSubBrand = _agriContext.ProductBrandMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingSubBrand)
            {
                return null;
            }
            else
            {
                ProductBrandMaster subBrnadMasterEntity = new ProductBrandMaster();
                subBrnadMasterEntity.ProductBrandName = model.ProductBrandName;
                subBrnadMasterEntity.ProductCategoryId = model.ProductCategoryId;
                subBrnadMasterEntity.Description = model.Description;
                subBrnadMasterEntity.SeqNo = model.SeqNo;
                subBrnadMasterEntity.EnteredBy = model.EnteredBy;
                subBrnadMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.ProductBrandMasters.Add(subBrnadMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateProductBrandMaster(ProductBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ProductBrandId = model.ProductBrandId;
            var subBrnadMasterEntity = _agriContext.ProductBrandMasters.FirstOrDefault(x => x.ProductBrandId == ProductBrandId);
            if (subBrnadMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                subBrnadMasterEntity.ProductBrandName = model.ProductBrandName;
                subBrnadMasterEntity.ProductCategoryId = model.ProductCategoryId;
                subBrnadMasterEntity.Description = model.Description;
                subBrnadMasterEntity.SeqNo = model.SeqNo;
                subBrnadMasterEntity.ChangedBy = model.ChangedBy;
                subBrnadMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteProductBrandMaster(long ProductBrandId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ProductBrandMasterModel model = new ProductBrandMasterModel();
            var subBrnadMasterEntity = _agriContext.ProductBrandMasters.FirstOrDefault(x => x.ProductBrandId == ProductBrandId);
            if (subBrnadMasterEntity != null)
            {
                subBrnadMasterEntity.DeleteStatus = true;
                subBrnadMasterEntity.ChangedBy = model.ChangedBy;
                subBrnadMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

         public List<MenuProductCategoryModel> BrandList()
        {
            var model = new List<MenuProductCategoryModel>();
            var categoeyEntityList = _agriContext.ProductCategoryMasters.Where(x => x.DeleteStatus == false).ToList();

            if (categoeyEntityList == null)
            {
                return model;
            }
            else
            {

                foreach (var itemCat in categoeyEntityList)
                {
                    var catModel = new MenuProductCategoryModel();

                    var brandEntity = _agriContext.ProductBrandMasters
                                                 .Where(x => x.ProductCategoryId == itemCat.ProductCategoryId  && !x.DeleteStatus)
                                                 .ToList();
                    catModel.ProductCategoryId = itemCat.ProductCategoryId;
                    catModel.ProductCategoryName = itemCat.ProductCategoryName;

                    foreach (var item in brandEntity)
                    {
                        var subCatModel = new MenuProductBrandModel();

                        subCatModel.ProductBrandId = item.ProductBrandId;
                        subCatModel.ProductBrandName = item.ProductBrandName;
                        catModel.productBrandList.Add(subCatModel);
                    }
                    model.Add(catModel);
                }
                return model;
            }

        }

    }
}
