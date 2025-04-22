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
using Product = StartUpX.Model.MenuProduct;

namespace StartUpX.Business.Implementation
{
    public class SeedProductMasterService : ISeedProductMasterService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public SeedProductMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<SeedProductMasterModel> GetAllSeedProductMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var seedproductMasterModelList = new List<SeedProductMasterModel>();
            var seedProductMasterListEntity = (from seedProduct in _agriContext.SeedProductMasters
                                               join seedBrand in _agriContext.SeedBrandMasters
                                               on seedProduct.SeedBrandId equals seedBrand.SeedBrandId

                                               join seedCrop in _agriContext.SeedCropMasters
                                               on seedProduct.SeedCropId equals seedCrop.SeedCropId

                                               join unitofMeasurment in _agriContext.UnitofMeasurementMasters
                                               on seedProduct.UnitId equals unitofMeasurment.UnitId

                                               join product in _agriContext.ProductCategoryMasters
                                               on seedProduct.ProductCategoryId equals product.ProductCategoryId

                                               where seedProduct.DeleteStatus == false
                                               select new
                                               {
                                                   seedProduct.SeedProductId,
                                                   seedProduct.SeedQuantity,
                                                   seedProduct.Amount,
                                                   seedProduct.DiscountAmount,
                                                   seedProduct.EnteredBy,
                                                   seedProduct.EnteredDate,
                                                   seedProduct.ChangedBy,
                                                   seedProduct.ChangedDate,
                                                   seedProduct.SequenceNo,
                                                   seedBrand.SeedBrandId,
                                                   seedBrand.SeedBrandName,
                                                   seedCrop.SeedCropId,
                                                   seedCrop.SeedCropName,
                                                   unitofMeasurment.UnitId,
                                                   unitofMeasurment.UnitName,
                                                   product.ProductCategoryId,
                                                   product.ProductCategoryName
                                               }).ToList();
            if (seedProductMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in seedProductMasterListEntity)
            {
                var model = new SeedProductMasterModel();
                model.SeedProductId = item.SeedProductId;
                model.SeedBrandId = item.SeedBrandId;
                model.SeedBrandName = item.SeedBrandName;
                model.SeedCropId = item.SeedCropId;
                model.ProductCategoryId = item.ProductCategoryId;
                model.ProductCategoryName = item.ProductCategoryName;
                model.SeedCropName = item.SeedCropName;
                model.UnitId = item.UnitId;
                model.UnitName = item.UnitName;
                model.SequenceNo = item.SequenceNo;
                model.SeedQuantity = (int)item.SeedQuantity;
                model.Amount = item.Amount;
                model.DiscountAmount = item.DiscountAmount;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                seedproductMasterModelList.Add(model);
            }
            return seedproductMasterModelList;
        }

        public SeedProductMasterModel GetSeedProductMasterById(long SeedProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedProductMasterEntity = (from seedProduct in _agriContext.SeedProductMasters
                                           join seedBrand in _agriContext.SeedBrandMasters
                                           on seedProduct.SeedBrandId equals seedBrand.SeedBrandId

                                           join seedCrop in _agriContext.SeedCropMasters
                                           on seedProduct.SeedCropId equals seedCrop.SeedCropId

                                           join unitofMeasurment in _agriContext.UnitofMeasurementMasters
                                           on seedProduct.UnitId equals unitofMeasurment.UnitId

                                           join product in _agriContext.ProductCategoryMasters
                                           on seedProduct.ProductCategoryId equals product.ProductCategoryId

                                           where seedProduct.SeedProductId == SeedProductId && seedProduct.DeleteStatus == false
                                           select new
                                           {
                                               seedProduct.SeedProductId,
                                               seedProduct.SeedQuantity,
                                               seedProduct.Amount,
                                               seedProduct.DiscountAmount,
                                               seedProduct.EnteredBy,
                                               seedProduct.EnteredDate,
                                               seedProduct.ChangedBy,
                                               seedProduct.ChangedDate,
                                               seedProduct.SequenceNo,
                                               seedBrand.SeedBrandId,
                                               seedBrand.SeedBrandName,
                                               seedCrop.SeedCropId,
                                               seedCrop.SeedCropName,
                                               unitofMeasurment.UnitId,
                                               unitofMeasurment.UnitName,
                                               product.ProductCategoryId,
                                               product.ProductCategoryName
                                           }).FirstOrDefault();
            if (seedProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedProductMasterModel
            {
                SeedProductId = seedProductMasterEntity.SeedProductId,
                SeedBrandId = seedProductMasterEntity.SeedBrandId,
                SeedBrandName = seedProductMasterEntity.SeedBrandName,
                SeedCropId = seedProductMasterEntity.SeedCropId,
                SeedCropName = seedProductMasterEntity.SeedCropName,
                UnitId = seedProductMasterEntity.UnitId,
                UnitName = seedProductMasterEntity.UnitName,
                ProductCategoryId = seedProductMasterEntity.ProductCategoryId,
                ProductCategoryName = seedProductMasterEntity.ProductCategoryName,
                SeedQuantity = (int)seedProductMasterEntity.SeedQuantity,
                SequenceNo = seedProductMasterEntity.SequenceNo,
                Amount = seedProductMasterEntity.Amount,
                DiscountAmount = seedProductMasterEntity.DiscountAmount,
                EnteredBy = seedProductMasterEntity.EnteredBy,
                EnteredDate = seedProductMasterEntity.EnteredDate,
                ChangedBy = seedProductMasterEntity.ChangedBy,
                ChangedDate = seedProductMasterEntity.ChangedDate
            };
        }

        public string AddSeedProductMaster(SeedProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedProductCategory = _agriContext.SeedProductMasters.Any(x => x.SequenceNo == model.SequenceNo);
            if (existingSeedProductCategory)
            {
                return null;
            }
            else
            {
                SeedProductMaster seedProductMasterEntity = new SeedProductMaster();
                seedProductMasterEntity.SeedBrandId = model.SeedBrandId;
                seedProductMasterEntity.SeedCropId = model.SeedCropId;
                seedProductMasterEntity.UnitId = model.UnitId;
                seedProductMasterEntity.ProductCategoryId = model.ProductCategoryId;
                seedProductMasterEntity.SeedQuantity = model.SeedQuantity;
                seedProductMasterEntity.SequenceNo = model.SequenceNo;
                seedProductMasterEntity.Amount = model.Amount;
                seedProductMasterEntity.DiscountAmount = model.DiscountAmount;
                seedProductMasterEntity.EnteredBy = model.EnteredBy;
                seedProductMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.SeedProductMasters.Add(seedProductMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedProductMaster(SeedProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var seedProductId = model.SeedProductId;
            var seedProductMasterEntity = _agriContext.SeedProductMasters.FirstOrDefault(x => x.SeedProductId == seedProductId);
            if (seedProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedProductMasterEntity.SeedBrandId = model.SeedBrandId;
                seedProductMasterEntity.SeedCropId = model.SeedCropId;
                seedProductMasterEntity.UnitId = model.UnitId;
                seedProductMasterEntity.ProductCategoryId = model.ProductCategoryId;
                seedProductMasterEntity.SeedQuantity = model.SeedQuantity;
                seedProductMasterEntity.SequenceNo = model.SequenceNo;
                seedProductMasterEntity.Amount = model.Amount;
                seedProductMasterEntity.DiscountAmount = model.DiscountAmount;
                seedProductMasterEntity.ChangedBy = model.ChangedBy;
                seedProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteSeedProductMaster(long SeedProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedProductMasterModel model = new SeedProductMasterModel();
            var seedProductMasterEntity = _agriContext.SeedProductMasters.FirstOrDefault(x => x.SeedProductId == SeedProductId);
            if (seedProductMasterEntity != null)
            {
                seedProductMasterEntity.DeleteStatus = true;
                seedProductMasterEntity.ChangedBy = model.ChangedBy;
                seedProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public List<MenuProductCategory> CategoryList()
        {
            var model = new List<MenuProductCategory>();
            var categoeyEntityList = _agriContext.ProductCategoryMasters.Where(x => x.DeleteStatus == false).ToList();

            if (categoeyEntityList == null)
            {
                return model;
            }
            else
            {

                foreach (var itemCat in categoeyEntityList)
                {
                    var catModel = new MenuProductCategory();
                    catModel.ProductCategoryId = itemCat.ProductCategoryId;
                    catModel.ProductCategoryName = itemCat.ProductCategoryName;



                    
                    var subCategoryEntity = _agriContext.ProductSubCategoryMasters
                                                 .Where(x => x.ProductCategoryId == itemCat.ProductCategoryId &&  x.ParentSubCategoryId==0 && x.DeleteStatus==false)
                                                 .ToList();


                    if (subCategoryEntity.Count > 0)
                    {
                        foreach (var itemsSubCate in subCategoryEntity)
                        {
                            var subCatModel = new MenuProductSubCategory();
                            subCatModel.SubCategoryId = itemsSubCate.SubCategoryId;
                            subCatModel.SubCategoryName = itemsSubCate.SubCategoryName;


                            var childCategoryEntityList = _agriContext.ProductSubCategoryMasters
                                                                     .Where(x => x.ParentSubCategoryId == itemsSubCate.SubCategoryId && x.DeleteStatus==false)
                                                                     .ToList();
                            foreach (var itemsChild in childCategoryEntityList)
                            {
                                var childCatModel = new MenuProductSubChildCategory();
                                childCatModel.SubChildCategoryId = itemsChild.SubCategoryId;
                                childCatModel.SubChildCategoryName = itemsChild.SubCategoryName;
                                childCatModel.hasChildcategory = (bool)itemsChild.HaschildCategory;
                                subCatModel.subChildCategoryList.Add(childCatModel);
                                var childschildCategoryEntityList = _agriContext.ProductSubCategoryMasters
                                                                  .Where(x => x.ParentSubCategoryId == itemsChild.SubCategoryId  && itemsChild.HaschildCategory==true  && x.DeleteStatus == false)
                                                                  .ToList();
                                foreach (var itemChild in childschildCategoryEntityList)
                                {

                                    var childschildCatModel = new MenuProductSubChildschildCategory();
                                    childschildCatModel.SubChildCategoryId = itemChild.SubCategoryId;
                                    childschildCatModel.SubChildCategoryName = itemChild.SubCategoryName;
                                    childCatModel.ChildCategoryList.Add(childschildCatModel);
                                }
                            }
                            catModel.subCategoryList.Add(subCatModel);
                        }
                    }

                    model.Add(catModel);
                }
                return model;
            }

        }

        }
    }
