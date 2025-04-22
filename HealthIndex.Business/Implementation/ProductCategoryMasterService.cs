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
    public class ProductCategoryMasterService : IProductCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ProductCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<ProductCategoryMasterModel> GetAllProductCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var productCategoryModelList = new List<ProductCategoryMasterModel>();
            var productCategoryListEntity = _agriContext.ProductCategoryMasters.Where(x => x.DeleteStatus == false).ToList();
            if (productCategoryListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in productCategoryListEntity)
            {
                var model = new ProductCategoryMasterModel();
                model.ProductCategoryId = item.ProductCategoryId;
                model.ProductCategoryName = item.ProductCategoryName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                productCategoryModelList.Add(model);
            }
            return productCategoryModelList;
        }

        public ProductCategoryMasterModel GetProductCategoryMasterById(long ProductCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var productCategoryEntity = _agriContext.ProductCategoryMasters.FirstOrDefault(x => x.ProductCategoryId == ProductCategoryId && x.DeleteStatus==false);
            if (productCategoryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ProductCategoryMasterModel
            {
                ProductCategoryName = productCategoryEntity.ProductCategoryName,
                Description = productCategoryEntity.Description,
                EnteredBy = productCategoryEntity.EnteredBy,
                EnteredDate = productCategoryEntity.EnteredDate,
                ChangedBy = productCategoryEntity.ChangedBy,
                ChangedDate = productCategoryEntity.ChangedDate
            };
        }

        public string AddProductCategoryMaster(ProductCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingProductCategory = _agriContext.ProductCategoryMasters.Any(x => x.ProductCategoryId == model.ProductCategoryId);
            if (existingProductCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                ProductCategoryMaster productCategoryEntity = new ProductCategoryMaster();
                productCategoryEntity.ProductCategoryName = model.ProductCategoryName;
                productCategoryEntity.Description = model.Description;
                productCategoryEntity.EnteredBy = model.EnteredBy;
                productCategoryEntity.EnteredDate = DateTime.Now;
                productCategoryEntity.DeleteStatus = false;
                _agriContext.ProductCategoryMasters.Add(productCategoryEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateProductCategoryMaster(ProductCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ProductCategoryId = model.ProductCategoryId;
            var productCategoryEntity = _agriContext.ProductCategoryMasters.FirstOrDefault(x => x.ProductCategoryId == ProductCategoryId);
            if (productCategoryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                productCategoryEntity.ProductCategoryName = model.ProductCategoryName;
                productCategoryEntity.Description = model.Description;
                productCategoryEntity.ChangedBy = model.ChangedBy;
                productCategoryEntity.ChangedDate = DateTime.Now;
                productCategoryEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteProductCategoryMaster(long ProductCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ProductCategoryMasterModel model = new ProductCategoryMasterModel();
            var productCategoryEntity = _agriContext.ProductCategoryMasters.FirstOrDefault(x => x.ProductCategoryId == ProductCategoryId);
            if (productCategoryEntity != null)
            {
                productCategoryEntity.DeleteStatus = true;
                productCategoryEntity.ChangedBy = model.EnteredBy;
                productCategoryEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }



      

    }
}
