using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class ProductService : IProductService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;

        public ProductService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }

        public List<ProductModel> GetAllProduct()
        {
            var errorResponseModel = new ErrorResponseModel();
            var productModelList = new List<ProductModel>();
            var productListEntity = (from product in _agriContext.Products
                                     join category in _agriContext.ProductCategoryMasters
                                     on product.ProductCategoryId equals category.ProductCategoryId

                                     join subcat in _agriContext.ProductSubCategoryMasters
                                     on product.SubCategoryId equals subcat.SubCategoryId

                                     join brand in _agriContext.ProductBrandMasters
                                     on product.ProductBrandId equals brand.ProductBrandId

                                     join unit in _agriContext.UnitofMeasurementMasters
                                     on product.UnitId equals unit.UnitId

                                     //join productImage in _agriContext.ProductImages
                                     //on product.ProductId equals productImage.ProductId


                                     where product.DeleteStatus == false
                                     select new
                                     {
                                         product.ProductId,
                                         product.ProductName,
                                         product.DiscountPrice,
                                         product.Quantity,
                                         product.ProductPrice,
                                         product.ProductCode,
                                         product.ProductDetails,
                                         product.ProductSpecification,
                                         product.Cgst,
                                         product.Sgst,
                                         product.Igst,
                                         product.IsActive,
                                         product.Rating,
                                         product.EnteredBy,
                                         product.EnteredDate,
                                         product.ChangedBy,
                                         product.ChangedDate,
                                         product.SeqNo,
                                         category.ProductCategoryId,
                                         category.ProductCategoryName,
                                         subcat.SubCategoryId,
                                         subcat.SubCategoryName,
                                         brand.ProductBrandId,
                                         brand.ProductBrandName,
                                         unit.UnitId,
                                         unit.UnitName,
                                        
                                     }).ToList();
            if (productListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in productListEntity)
            {
                var model = new ProductModel();
                model.ProductId = item.ProductId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.SubCategoryId = item.SubCategoryId;
                model.UnitId = item.UnitId;
                model.ProductBrandId = item.ProductBrandId;
                model.ProductName = item.ProductName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.SubCategoryName = item.SubCategoryName;
                model.ProductBrandName = item.ProductBrandName;
                model.UnitName = item.UnitName;
                model.Cgst = (decimal)item.Cgst;
                model.Sgst = (decimal)item.Sgst;
                model.Igst = (decimal)item.Igst;
                model.IsActive = item.IsActive;
                model.Rating = item.Rating;
                model.ProductCode = item.ProductCode;
                model.ProductDetails = item.ProductDetails;
                model.ProductPrice = item.ProductPrice;
                model.ProductSpecification = item.ProductSpecification;
                model.SeqNo = item.SeqNo;
                model.Quantity = item.Quantity;
                model.DiscountPrice = item.DiscountPrice;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;

                var productImageList = _agriContext.ProductImages
                                                           .Where(x => x.ProductId == item.ProductId).ToList();
                foreach (var itemImages in productImageList)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = itemImages.ProductImageId;
                    imgModel.ProductId = itemImages.ProductId;
                    imgModel.ProductImage1 = _configuration.HostName + itemImages.ProductImage1;



                    model.productImage.Add(imgModel);


                }

                if (productImageList.Count == 0)
                {
                    var imgModel = new ProductImageModel();

                    imgModel.ProductImageId = 1;
                    imgModel.ProductId = item.ProductId;
                    imgModel.ProductImage1 = "/ProductImage/no_image.png";
                    model.productImage.Add(imgModel);

                }


                productModelList.Add(model);
            }
            return productModelList;
        }


        public ProductModel GetProductById(long ProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var productListEntity = (from product in _agriContext.Products
                                     join category in _agriContext.ProductCategoryMasters
                                     on product.ProductCategoryId equals category.ProductCategoryId

                                     join subcat in _agriContext.ProductSubCategoryMasters
                                     on product.SubCategoryId equals subcat.SubCategoryId

                                     join brand in _agriContext.ProductBrandMasters
                                     on product.ProductBrandId equals brand.ProductBrandId

                                     join unit in _agriContext.UnitofMeasurementMasters
                                     on product.UnitId equals unit.UnitId

                                     //join productImage in _agriContext.ProductImages
                                     // on product.ProductId equals productImage.ProductImageId

                                     where product.DeleteStatus == false && product.ProductId == ProductId
                                     select new
                                     {
                                         product.ProductId,
                                         product.ProductName,
                                         product.DiscountPrice,
                                         product.Quantity,
                                         product.ProductPrice,
                                         product.ProductCode,
                                         product.ProductDetails,
                                         product.ProductSpecification,
                                         product.Cgst,
                                         product.Sgst,
                                         product.Igst,
                                         product.IsActive,
                                         product.Rating,
                                         product.EnteredBy,
                                         product.EnteredDate,
                                         product.ChangedBy,
                                         product.ChangedDate,
                                         product.SeqNo,
                                         category.ProductCategoryId,
                                         category.ProductCategoryName,
                                         subcat.SubCategoryId,
                                         subcat.SubCategoryName,
                                         brand.ProductBrandId,
                                         brand.ProductBrandName,
                                         unit.UnitId,
                                         unit.UnitName,

                                     }).FirstOrDefault();
            if (productListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            var model = new ProductModel();
            model.ProductId = productListEntity.ProductId;
            model.ProductCategoryId = productListEntity.ProductCategoryId;
            model.ProductBrandId = productListEntity.ProductBrandId;
            model.UnitId = productListEntity.UnitId;
            model.SubCategoryId = productListEntity.SubCategoryId;
            model.ProductName = productListEntity.ProductName;
            model.ProductCategoryName = productListEntity.ProductCategoryName;
            model.UnitName = productListEntity.UnitName;
            model.SubCategoryName = productListEntity.SubCategoryName;
            model.Cgst = (decimal)productListEntity.Cgst;
            model.Sgst = (decimal)productListEntity.Sgst;
            model.Igst = (decimal)productListEntity.Igst;
            model.IsActive = productListEntity.IsActive;
            model.Rating = productListEntity.Rating;
            model.Quantity = productListEntity.Quantity;
            model.ProductCode = productListEntity.ProductCode;
            model.ProductDetails = productListEntity.ProductDetails;
            model.ProductPrice = productListEntity.ProductPrice;
            model.ProductSpecification = productListEntity.ProductSpecification;
            model.SeqNo = productListEntity.SeqNo;
            model.DiscountPrice = productListEntity.DiscountPrice;
            model.EnteredBy = productListEntity.EnteredBy;
            model.EnteredDate = productListEntity.EnteredDate;
            model.ChangedBy = productListEntity.ChangedBy;
            model.ChangedDate = productListEntity.ChangedDate;
            var productImageList = _agriContext.ProductImages
                                                      .Where(x => x.ProductId == productListEntity.ProductId).ToList();
            foreach (var itemImages in productImageList)
            {
                var imgModel = new ProductImageModel();
                imgModel.ProductImageId =  itemImages.ProductImageId;
                imgModel.ProductId = itemImages.ProductId;
                imgModel.ProductImage1 = _configuration.HostName + itemImages.ProductImage1;

                model.productImage.Add(imgModel);
            }
            if (productImageList.Count == 0)
            {
                var imgModel = new ProductImageModel();

                imgModel.ProductImageId = 1;
                imgModel.ProductId = imgModel.ProductId;
                imgModel.ProductImage1 = "/ProductImage/no_image.png";
                model.productImage.Add(imgModel);

            }
            if (productListEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }
        public string AddProduct(ProductModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingproduct = _agriContext.Products.Any(x => x.SeqNo == model.SeqNo);
            if (existingproduct)
            {
                return null;
            }
            else
            {
                Product productListEntity = new Product();
                productListEntity.ProductCategoryId = model.ProductCategoryId;
                productListEntity.UnitId = model.UnitId;
                productListEntity.SubCategoryId = model.SubCategoryId;
                productListEntity.ProductBrandId = model.ProductBrandId;
                productListEntity.ProductName = model.ProductName;
                productListEntity.ProductCode = model.ProductCode;
                productListEntity.ProductDetails = model.ProductDetails;
                productListEntity.ProductPrice = model.ProductPrice;
                productListEntity.ProductSpecification = model.ProductSpecification;
                productListEntity.Cgst = model.Cgst;
                productListEntity.Sgst = model.Sgst;
                productListEntity.Igst = model.Igst;
                productListEntity.IsActive = model.IsActive;
                productListEntity.SeqNo = model.SeqNo;
                productListEntity.Quantity = model.Quantity;
                productListEntity.DiscountPrice = (decimal)model.DiscountPrice;
                productListEntity.Rating = model.Rating;
                productListEntity.EnteredBy = model.EnteredBy;
                productListEntity.EnteredDate = DateTime.Now;
                productListEntity.DeleteStatus = false;
                _agriContext.Products.Add(productListEntity);
                _agriContext.SaveChanges();
                message = (productListEntity.ProductId).ToString();
            }
            return message;
        }

        public bool UpdateProduct(ProductModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ProductId = model.ProductId;
            var productListEntity = _agriContext.Products.FirstOrDefault(x => x.ProductId == ProductId);
            if (productListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                productListEntity.ProductCategoryId = model.ProductCategoryId;
                productListEntity.UnitId = model.UnitId;
                productListEntity.SubCategoryId = model.SubCategoryId;
                productListEntity.ProductBrandId = model.ProductBrandId;
                productListEntity.ProductName = model.ProductName;
                productListEntity.ProductCode = model.ProductCode;
                productListEntity.ProductDetails = model.ProductDetails;
                productListEntity.ProductPrice = model.ProductPrice;
                productListEntity.ProductSpecification = model.ProductSpecification;
                productListEntity.Cgst = model.Cgst;
                productListEntity.Sgst = model.Sgst;
                productListEntity.Igst = model.Igst;
                productListEntity.IsActive = model.IsActive;
                productListEntity.SeqNo = model.SeqNo;
                productListEntity.Quantity = model.Quantity;
                productListEntity.DiscountPrice = (decimal)model.DiscountPrice;
                productListEntity.Rating = model.Rating;
                productListEntity.ChangedBy = model.ChangedBy;
                productListEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteProduct(long ProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ProductModel model = new ProductModel();
            var productListEntity = _agriContext.Products.FirstOrDefault(x => x.ProductId == ProductId);
            if (productListEntity != null)
            {
                productListEntity.DeleteStatus = true;
                productListEntity.ChangedBy = model.ChangedBy;
                productListEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        public List<ProductModel> GetProductBySubcategory(long SubCategoryId, CropParameters productsubParameters)
        {
            var productList = new List<ProductModel>();

            var productListEntity = (from product in _agriContext.Products
                                     join productSubCat in _agriContext.ProductSubCategoryMasters
                                     on product.SubCategoryId equals productSubCat.SubCategoryId
                                     where product.SubCategoryId == SubCategoryId && product.DeleteStatus == false && (productSubCat.HaschildCategory == true || productSubCat.HaschildCategory == false) && productSubCat.ParentSubCategoryId == 0
                                     join brand in _agriContext.ProductBrandMasters
                                     on product.ProductBrandId equals brand.ProductBrandId

                                     join category in _agriContext.ProductCategoryMasters
                                     on product.ProductCategoryId equals category.ProductCategoryId


                                     join unit in _agriContext.UnitofMeasurementMasters
                                     on product.UnitId equals unit.UnitId
                                     select new
                                     {
                                         product.ProductId,
                                         product.ProductName,
                                         product.DiscountPrice,
                                         product.Quantity,
                                         product.ProductPrice,
                                         product.ProductCode,
                                         product.ProductDetails,
                                         product.ProductSpecification,
                                         product.Cgst,
                                         product.Sgst,
                                         product.Igst,
                                         product.IsActive,
                                         product.Rating,
                                         product.EnteredBy,
                                         product.EnteredDate,
                                         product.ChangedBy,
                                         product.ChangedDate,
                                         product.SeqNo,
                                         category.ProductCategoryId,
                                         category.ProductCategoryName,
                                         productSubCat.SubCategoryId,
                                         productSubCat.SubCategoryName,
                                         brand.ProductBrandId,
                                         brand.ProductBrandName,
                                         unit.UnitId,
                                         unit.UnitName,


                                     }).Skip((productsubParameters.PageNumber - 1) * productsubParameters.PageSize)
        .Take(productsubParameters.PageSize)
        .ToList();
            foreach (var item in productListEntity)
            {
                var model = new ProductModel();
                model.ProductId = item.ProductId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.SubCategoryId = item.SubCategoryId;
                model.UnitId = item.UnitId;
                model.ProductBrandId = item.ProductBrandId;
                model.ProductName = item.ProductName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.SubCategoryName = item.SubCategoryName;
                model.ProductBrandName = item.ProductBrandName;
                model.UnitName = item.UnitName;
                model.Cgst = (decimal)item.Cgst;
                model.Sgst = (decimal)item.Sgst;
                model.Igst = (decimal)item.Igst;
                model.IsActive = item.IsActive;
                model.Rating = item.Rating;
                model.ProductCode = item.ProductCode;
                model.ProductDetails = item.ProductDetails;
                model.ProductPrice = item.ProductPrice;
                model.ProductSpecification = item.ProductSpecification;
                model.SeqNo = item.SeqNo;
                model.Quantity = item.Quantity;
                model.DiscountPrice = item.DiscountPrice;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                var productImageList = _agriContext.ProductImages
                                       .Where(x => x.ProductId == item.ProductId).ToList();
                foreach (var itemImages in productImageList)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = itemImages.ProductImageId;

                    imgModel.ProductId = itemImages.ProductId;
                    imgModel.ProductImage1 = _configuration.HostName + itemImages.ProductImage1;
                    model.productImage.Add(imgModel);
                }
                if (productImageList.Count == 0)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = 1;
                    imgModel.ProductId = item.ProductId;
                    imgModel.ProductImage1 = "/ProductImage/no_image.png";
                    model.productImage.Add(imgModel);

                }
                productList.Add(model);
            }
            if (productListEntity == null)
            {
                return null;
            }
            else
            {
                return productList;
            }
        }



        public List<ProductModel> GetProductBychildSubcategory(long SubCategoryId, CropParameters productchildParameters)
        {
            var productList = new List<ProductModel>();

            var productListEntity = (from product in _agriContext.Products
                                     join productSubCat in _agriContext.ProductSubCategoryMasters
                                     on product.SubCategoryId equals productSubCat.SubCategoryId
                                     where productSubCat.SubCategoryId == SubCategoryId && product.DeleteStatus == false && productSubCat.HaschildCategory == false && productSubCat.ParentSubCategoryId != 0
                                     join brand in _agriContext.ProductBrandMasters
                                     on product.ProductBrandId equals brand.ProductBrandId

                                     join category in _agriContext.ProductCategoryMasters
                                     on product.ProductCategoryId equals category.ProductCategoryId

                                     join unit in _agriContext.UnitofMeasurementMasters
                                     on product.UnitId equals unit.UnitId

                                     select new
                                     {
                                         product.ProductId,
                                         product.ProductName,
                                         product.DiscountPrice,
                                         product.Quantity,
                                         product.ProductPrice,
                                         product.ProductCode,
                                         product.ProductDetails,
                                         product.ProductSpecification,
                                         product.Cgst,
                                         product.Sgst,
                                         product.Igst,
                                         product.IsActive,
                                         product.Rating,
                                         product.EnteredBy,
                                         product.EnteredDate,
                                         product.ChangedBy,
                                         product.ChangedDate,
                                         product.SeqNo,
                                         category.ProductCategoryId,
                                         category.ProductCategoryName,
                                         productSubCat.SubCategoryId,
                                         productSubCat.SubCategoryName,
                                         brand.ProductBrandId,
                                         brand.ProductBrandName,
                                         unit.UnitId,
                                         unit.UnitName,

                                     }).Skip((productchildParameters.PageNumber - 1) * productchildParameters.PageSize)
        .Take(productchildParameters.PageSize)
        .ToList();

            foreach (var item in productListEntity)
            {
                var model = new ProductModel();
                model.ProductId = item.ProductId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.SubCategoryId = item.SubCategoryId;
                model.UnitId = item.UnitId;
                model.ProductBrandId = item.ProductBrandId;
                model.ProductName = item.ProductName;
                model.ProductCategoryName = item.ProductCategoryName;
                model.SubCategoryName = item.SubCategoryName;
                model.ProductBrandName = item.ProductBrandName;
                model.UnitName = item.UnitName;
                model.Cgst = (decimal)item.Cgst;
                model.Sgst = (decimal)item.Sgst;
                model.Igst = (decimal)item.Igst;
                model.IsActive = item.IsActive;
                model.Rating = item.Rating;
                model.ProductCode = item.ProductCode;
                model.ProductDetails = item.ProductDetails;
                model.ProductPrice = item.ProductPrice;
                model.ProductSpecification = item.ProductSpecification;
                model.SeqNo = item.SeqNo;
                model.Quantity = item.Quantity;
                model.DiscountPrice = item.DiscountPrice;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                var productImageList = _agriContext.ProductImages
                                        .Where(x => x.ProductId == item.ProductId).ToList();
                foreach (var itemImages in productImageList)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = itemImages.ProductImageId;
                    imgModel.ProductId = itemImages.ProductId;
                    imgModel.ProductImage1 = _configuration.HostName + itemImages.ProductImage1;
                    model.productImage.Add(imgModel);
                }

                if (productImageList.Count == 0)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = 1;
                    imgModel.ProductId = item.ProductId;
                    imgModel.ProductImage1 = "/ProductImage/no_image.png";
                    model.productImage.Add(imgModel);

                }
                productList.Add(model);
            }
            if (productListEntity == null)
            {
                return null;
            }
            else
            {
                return productList;
            }
        }



        public List<ProductModel> GetFilterAllProducts(ProductSearch searchModel)
        {

            var productList = new List<ProductModel>();
            var productListEntity = _agriContext.Products.Where(x => x.DeleteStatus == false && x.ProductName != null).Include(x => x.ProductImages).ToList();

            productListEntity = productListEntity.Where(x => x.ProductName.ToLower().Contains(searchModel.ProductName.ToLower())).ToList();

            foreach (var item in productListEntity)
            {
                var model = new ProductModel();
                model.ProductId = item.ProductId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.SubCategoryId = item.SubCategoryId;
                model.UnitId = item.UnitId;
                model.ProductBrandId = item.ProductBrandId;
                model.ProductName = item.ProductName;
                model.ProductCategoryName = item.ProductCategoryId.ToString();
                model.Cgst = (decimal)item.Cgst;
                model.Sgst = (decimal)item.Sgst;
                model.Igst = (decimal)item.Igst;
                model.IsActive = item.IsActive;
                model.Rating = item.Rating;
                model.ProductCode = item.ProductCode;
                model.ProductDetails = item.ProductDetails;
                model.ProductPrice = item.ProductPrice;
                model.ProductSpecification = item.ProductSpecification;
                model.SeqNo = item.SeqNo;
                model.Quantity = item.Quantity;
                model.DiscountPrice = item.DiscountPrice;
                var productImageList = _agriContext.ProductImages
                                       .Where(x => x.ProductId == item.ProductId).ToList();
                foreach (var itemImage in productImageList)
                {
                    var imgModel = new ProductImageModel();
                    imgModel.ProductImageId = itemImage.ProductImageId;
                    imgModel.ProductId = itemImage.ProductId;
                    imgModel.ProductImage1 = _configuration.HostName + itemImage.ProductImage1;

                    model.productImage.Add(imgModel);
                }

                if (productImageList.Count == 0)
                {
                    var imgModel = new ProductImageModel();

                    imgModel.ProductImageId = 1;
                    imgModel.ProductId = item.ProductId;
                    imgModel.ProductImage1 = "/ProductImage/no_image.png";
                    model.productImage.Add(imgModel);

                }
                productList.Add(model);
            }
            if (productListEntity == null)
            {
                return null;
            }
            else
            {
                return productList;
            }
        }


        public string DeleteImageFromDB(ProductModel model)
        {
            var imageEntityList = _agriContext.ProductImages.Where(x => x.ProductId == model.ProductId).ToList();

            foreach (var item in imageEntityList)
            {
                var imgEntity = _agriContext.ProductImages.FirstOrDefault(x => x.ProductImageId == item.ProductImageId);
                _agriContext.Remove(imgEntity);
            }
            _agriContext.SaveChanges();

            return "deleted.";
        }

        public string AddProductForWeb(ProductModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingproduct = _agriContext.Products.Any(x => x.ProductId == model.ProductId);
            if (existingproduct)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                Product productListEntity = new Product();
                productListEntity.ProductId = model.ProductId;
                productListEntity.ProductCategoryId = model.ProductCategoryId;
                productListEntity.UnitId = model.UnitId;
                productListEntity.SubCategoryId = model.SubCategoryId;
                productListEntity.ProductBrandId = model.ProductBrandId;
                productListEntity.ProductName = model.ProductName;
                productListEntity.ProductCode = model.ProductCode;
                productListEntity.ProductDetails = model.ProductDetails;
                productListEntity.ProductPrice = model.ProductPrice;
                productListEntity.ProductSpecification = model.ProductSpecification;
                productListEntity.Cgst = model.Cgst;
                productListEntity.Sgst = model.Sgst;
                productListEntity.Igst = model.Igst;
                productListEntity.IsActive = true;
                productListEntity.SeqNo = model.SeqNo;
                productListEntity.Quantity = model.Quantity;
                productListEntity.DiscountPrice = (decimal)model.DiscountPrice;
                productListEntity.Rating = model.Rating;
                productListEntity.EnteredBy = model.EnteredBy;
                productListEntity.EnteredDate = DateTime.Now;
                productListEntity.DeleteStatus = false;
                _agriContext.Products.Add(productListEntity);
                _agriContext.SaveChanges();
                foreach (var item in model.productImage)
                {
                    var productImageEntity = new ProductImage();

                    productImageEntity.ProductImageId = item.ProductImageId;
                    productImageEntity.ProductId = productListEntity.ProductId;
                    productImageEntity.ProductImage1 = item.ProductImage1;
                    productImageEntity.EnteredDate = DateTime.UtcNow;
                    productImageEntity.EnteredBy = model.EnteredBy;
                    productImageEntity.DeleteStatus = false;
                    _agriContext.Add(productImageEntity);

                }
                _agriContext.SaveChanges();

                message = (productListEntity.ProductId).ToString();
            }
            return message;
        }



        public bool UpdateProductForWeb(ProductModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ProductId = model.ProductId;
            var productListEntity = _agriContext.Products.FirstOrDefault(x => x.ProductId == ProductId);
            if (productListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                productListEntity.ProductId = model.ProductId;
                productListEntity.ProductCategoryId = model.ProductCategoryId;
                productListEntity.UnitId = model.UnitId;
                productListEntity.SubCategoryId = model.SubCategoryId;
                productListEntity.ProductBrandId = model.ProductBrandId;
                productListEntity.ProductName = model.ProductName;
                productListEntity.ProductCode = model.ProductCode;
                productListEntity.ProductDetails = model.ProductDetails;
                productListEntity.ProductPrice = model.ProductPrice;
                productListEntity.ProductSpecification = model.ProductSpecification;
                productListEntity.Cgst = model.Cgst;
                productListEntity.Sgst = model.Sgst;
                productListEntity.Igst = model.Igst;
                productListEntity.IsActive = true;
                productListEntity.SeqNo = model.SeqNo;
                productListEntity.Quantity = model.Quantity;
                productListEntity.DiscountPrice = (decimal)model.DiscountPrice;
                productListEntity.Rating = model.Rating;
                productListEntity.ChangedBy = model.ChangedBy;
                productListEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();

                if (model.productImage.Count > 0)
                {
                    foreach (var item in model.productImage)
                    {
                        var productImageEntity = new ProductImage();

                        productImageEntity.ProductImageId = item.ProductImageId;
                        productImageEntity.ProductId = productListEntity.ProductId;
                        productImageEntity.ProductImage1 = item.ProductImage1;
                        productImageEntity.EnteredDate = DateTime.UtcNow;
                        productImageEntity.EnteredBy = model.EnteredBy;
                        productImageEntity.DeleteStatus = false;
                        _agriContext.Add(productImageEntity);
                    }
                    _agriContext.SaveChanges();

                }
                return true;
            }
        }

        
    }
}
