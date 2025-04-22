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
    public class SeedCropMasterService : ISeedCropMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;
        public SeedCropMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }
        public List<SeedCropMasterModel> GetAllSeedCropMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var seedCropMasterModelList = new List<SeedCropMasterModel>();
            var seedCropMasterModelListEntity = (from seedCropMaster in _agriContext.SeedCropMasters
                                                   join seedSubCategory in _agriContext.SeedSubCategoryMasters
                                                   on seedCropMaster.SeedSubCategoryId equals seedSubCategory.SeedSubCategoryId

                                                  join Crop in _agriContext.CropMasters
                                                  on seedCropMaster.CropId equals Crop.CropId

                                                 where seedCropMaster.DeleteStatus == false
                                                   select new
                                                   {
                                                       seedCropMaster.SeedCropId,
                                                       seedCropMaster.SeedCropName,
                                                       seedCropMaster.Description,
                                                       seedCropMaster.EnteredBy,
                                                       seedCropMaster.EnteredDate,
                                                       seedCropMaster.ChangedBy,
                                                       seedCropMaster.ChangedDate,
                                                       seedCropMaster.DeleteStatus,
                                                       seedSubCategory.SeedSubCategoryId,
                                                       seedSubCategory.SeedSubCategoryName,
                                                       Crop.CropId,
                                                       Crop.CropName
                                                   }).ToList();
            if (seedCropMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in seedCropMasterModelListEntity)
            {
                var model = new SeedCropMasterModel();
                model.SeedCropId = item.SeedCropId;
                model.SeedSubCategoryId = item.SeedSubCategoryId;
                model.SeedSubCategoryName = item.SeedSubCategoryName;
                model.SeedCropName = item.SeedCropName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                seedCropMasterModelList.Add(model);
            }
            return seedCropMasterModelList;
        }

        public SeedCropMasterModel GetSeedCropMasterById(long SeedCropId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedCropMasterEntity = (from seedCropMaster in _agriContext.SeedCropMasters
                                        join seedSubCategory in _agriContext.SeedSubCategoryMasters
                                        on seedCropMaster.SeedSubCategoryId equals seedSubCategory.SeedSubCategoryId
                                        join Crop in _agriContext.CropMasters
                                         on seedCropMaster.CropId equals Crop.CropId

                                        where seedCropMaster.SeedCropId==SeedCropId && seedCropMaster.DeleteStatus == false
                                        select new
                                        {
                                            seedCropMaster.SeedCropId,
                                            seedCropMaster.SeedCropName,
                                            seedCropMaster.Description,
                                            seedCropMaster.EnteredBy,
                                            seedCropMaster.EnteredDate,
                                            seedCropMaster.ChangedBy,
                                            seedCropMaster.ChangedDate,
                                            seedCropMaster.DeleteStatus,
                                            seedSubCategory.SeedSubCategoryId,
                                            seedSubCategory.SeedSubCategoryName,
                                            Crop.CropId,
                                            Crop.CropName
                                        }).FirstOrDefault();
            if (seedCropMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedCropMasterModel
            {
                SeedCropId = seedCropMasterEntity.SeedCropId,
                SeedSubCategoryId = seedCropMasterEntity.SeedSubCategoryId,
                SeedSubCategoryName=seedCropMasterEntity.SeedSubCategoryName,
                SeedCropName = seedCropMasterEntity.SeedCropName,
                CropId=seedCropMasterEntity.CropId,
                CropName=seedCropMasterEntity.CropName,
                Description = seedCropMasterEntity.Description,
                EnteredBy = seedCropMasterEntity.EnteredBy,
                EnteredDate = seedCropMasterEntity.EnteredDate,
                ChangedBy = seedCropMasterEntity.ChangedBy,
                ChangedDate = seedCropMasterEntity.ChangedDate,
                DeleteStatus = seedCropMasterEntity.DeleteStatus
            };
        }

        public string AddSeedCropMaster(SeedCropMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedCropCategory = _agriContext.SeedCropMasters.Any(x => x.SeedCropId == model.SeedCropId);
            if (existingSeedCropCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                SeedCropMaster seedCropMasterEntity = new SeedCropMaster();
                seedCropMasterEntity.SeedSubCategoryId = model.SeedSubCategoryId;
                seedCropMasterEntity.SeedCropName = model.SeedCropName;
                seedCropMasterEntity.CropId = model.CropId;
                seedCropMasterEntity.SeedSubCategoryId = model.SeedSubCategoryId;
                seedCropMasterEntity.Description = model.Description;
                seedCropMasterEntity.EnteredBy = model.EnteredBy;
                seedCropMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.SeedCropMasters.Add(seedCropMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedCropMaster(SeedCropMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var seedCropId = model.SeedCropId;
            var seedCropMasterEntity = _agriContext.SeedCropMasters.FirstOrDefault(x => x.SeedCropId == seedCropId);
            if (seedCropMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedCropMasterEntity.SeedSubCategoryId = model.SeedSubCategoryId;
                seedCropMasterEntity.SeedCropName = model.SeedCropName;
                seedCropMasterEntity.SeedSubCategoryId = model.SeedSubCategoryId;
                seedCropMasterEntity.CropId = model.CropId;
                seedCropMasterEntity.Description = model.Description;
                seedCropMasterEntity.ChangedBy = model.ChangedBy;
                seedCropMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteSeedCropMaster(long SeedCropId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedCropMasterModel model = new SeedCropMasterModel();
            var seedCropMasterEntity = _agriContext.SeedCropMasters.FirstOrDefault(x => x.SeedCropId == SeedCropId);
            if (seedCropMasterEntity != null)
            {
                seedCropMasterEntity.DeleteStatus = true;
                seedCropMasterEntity.ChangedBy = model.EnteredBy;
                seedCropMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
