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
    public class ProductImageService : IProductImageService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ProductImageService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        } 
        public List<ProductImagesModel> GetAllProductImages()
        {
            var errorResponseModel = new ErrorResponseModel();
            var productImagesModelList = new List<ProductImagesModel>();
            var productImagesListEntity = (from img in _agriContext.ProductImages
                                                  join product in _agriContext.Products
                                                  on img.ProductId equals product.ProductId
                                                  where img.DeleteStatus == false
                                                  select new
                                                  {
                                                      img.ProductImageId,
                                                      img.ProductImage1,
                                                      img.IsActive,
                                                      img.EnteredBy,
                                                      img.EnteredDate,
                                                      img.ChangedBy,
                                                      img.ChangedDate,
                                                      product.ProductId,
                                                      product.ProductName 
                                                  }).ToList();
            if (productImagesListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in productImagesListEntity)
            {
                var model = new ProductImagesModel();
                model.ProductImageId = item.ProductImageId;
                model.ProductId = item.ProductId;
                model.ProductImage1 = item.ProductImage1;
                model.ProductName = item.ProductName;
                model.IsActive = (bool)item.IsActive;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                productImagesModelList.Add(model);
            }
            return productImagesModelList;
        }

        public ProductImagesModel GetProductImagesById(long ProductImagesId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var productImagesListEntity = (from img in _agriContext.ProductImages
                                           join product in _agriContext.Products
                                           on img.ProductId equals product.ProductId
                                           where img.DeleteStatus == false && img.ProductImageId==ProductImagesId
                                           select new
                                           {
                                               img.ProductImageId,
                                               img.ProductImage1,
                                               img.IsActive,
                                               img.EnteredBy,
                                               img.EnteredDate,
                                               img.ChangedBy,
                                               img.ChangedDate,
                                               product.ProductId,
                                               product.ProductName
                                           }).FirstOrDefault();
            if (productImagesListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ProductImagesModel
            {
                ProductImageId = productImagesListEntity.ProductImageId,
                ProductId = productImagesListEntity.ProductId,
                ProductImage1 = productImagesListEntity.ProductImage1,
                ProductName = productImagesListEntity.ProductName,
                IsActive = (bool)productImagesListEntity.IsActive,
                EnteredBy = productImagesListEntity.EnteredBy,
                EnteredDate = productImagesListEntity.EnteredDate,
                ChangedBy = productImagesListEntity.ChangedBy,
                ChangedDate = productImagesListEntity.ChangedDate
            };
        }

        public string AddProductImages(ProductImagesModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingroductImages = _agriContext.ProductImages.Any(x => x.ProductImageId == model.ProductImageId);
            if (existingroductImages)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                ProductImage productImagesListEntity = new ProductImage();
                productImagesListEntity.ProductImage1 = model.ProductImage1;
                productImagesListEntity.ProductId = model.ProductId;
                productImagesListEntity.IsActive = true;
                productImagesListEntity.EnteredBy = model.EnteredBy;
                productImagesListEntity.EnteredDate = DateTime.Now;
                productImagesListEntity.DeleteStatus = false;
                _agriContext.ProductImages.Add(productImagesListEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateProductImages(ProductImagesModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ProductImageId = model.ProductImageId;
            var productImagesListEntity = _agriContext.ProductImages.FirstOrDefault(x => x.ProductImageId == ProductImageId);
            if (productImagesListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                productImagesListEntity.ProductImage1 = model.ProductImage1;
                productImagesListEntity.ProductId = model.ProductId;
                productImagesListEntity.IsActive = model.IsActive;
                productImagesListEntity.ChangedBy = model.ChangedBy;
                productImagesListEntity.ChangedDate = DateTime.Now;
                productImagesListEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteProductImages(long ProductImageId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ProductImagesModel model = new ProductImagesModel();
            var productImagesListEntity = _agriContext.ProductImages.FirstOrDefault(x => x.ProductImageId == ProductImageId);
            if (productImagesListEntity != null)
            {
                productImagesListEntity.DeleteStatus = true;
                productImagesListEntity.ChangedBy = model.EnteredBy;
                productImagesListEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
