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
    public class SeedSubCategoryMasterService : ISeedSubCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public SeedSubCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration) 
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<SeedSubCategoryMasterModel> GetAllSeedSubCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var seedSubCategoryMasterModelList = new List<SeedSubCategoryMasterModel>();
            var seedSubCategoryMasterListEntity = (from seedSubCategory in _agriContext.SeedSubCategoryMasters
                                                   join seedCategory in _agriContext.SeedCategoryMasters
                                                   on seedSubCategory.SeedCategoryId equals seedCategory.SeedCategoryId
                                                   where seedSubCategory.DeleteStatus == false
                                                   select new {
                                                       seedSubCategory.SeedSubCategoryId,
                                                       seedSubCategory.SeedSubCategoryName,
                                                       seedSubCategory.Description,     
                                                       seedSubCategory.EnteredBy,
                                                       seedSubCategory.EnteredDate,
                                                       seedSubCategory.ChangedBy,
                                                       seedSubCategory.ChangedDate,
                                                       seedCategory.SeedCategoryId,
                                                       seedCategory.SeedCategoryName
                                                   }).ToList();
            if (seedSubCategoryMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in seedSubCategoryMasterListEntity)
            {
                var model = new SeedSubCategoryMasterModel();
                model.SeedSubCategoryId = item.SeedSubCategoryId;
                model.SeedCategoryId = item.SeedCategoryId;
                model.SeedCategoryName = item.SeedCategoryName;
                model.SeedSubCategoryName = item.SeedSubCategoryName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                seedSubCategoryMasterModelList.Add(model);
            }
            return seedSubCategoryMasterModelList;
        }

        public SeedSubCategoryMasterModel GetSeedSubCategoryMasterById(long SeedSubCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedSubCategoryMasterEntity = (from seedSubCategory in _agriContext.SeedSubCategoryMasters
                                               join seedCategory in _agriContext.SeedCategoryMasters
                                               on seedSubCategory.SeedCategoryId equals seedCategory.SeedCategoryId
                                               where seedSubCategory.SeedSubCategoryId==SeedSubCategoryId && seedSubCategory.DeleteStatus == false
                                               select new
                                               {
                                                   seedSubCategory.SeedSubCategoryId,
                                                   seedSubCategory.SeedSubCategoryName,
                                                   seedSubCategory.Description,
                                                   seedSubCategory.EnteredBy,
                                                   seedSubCategory.EnteredDate,
                                                   seedSubCategory.ChangedBy,
                                                   seedSubCategory.ChangedDate,
                                                   seedCategory.SeedCategoryId,
                                                   seedCategory.SeedCategoryName
                                               }).FirstOrDefault();
            if (seedSubCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedSubCategoryMasterModel
            {
                SeedSubCategoryId=seedSubCategoryMasterEntity.SeedSubCategoryId,
                SeedSubCategoryName = seedSubCategoryMasterEntity.SeedSubCategoryName,
                SeedCategoryId=seedSubCategoryMasterEntity.SeedCategoryId,
                SeedCategoryName=seedSubCategoryMasterEntity.SeedCategoryName,
                Description = seedSubCategoryMasterEntity.Description,
                EnteredBy = seedSubCategoryMasterEntity.EnteredBy,
                EnteredDate=seedSubCategoryMasterEntity.EnteredDate,
                ChangedBy = seedSubCategoryMasterEntity.ChangedBy,
                ChangedDate=seedSubCategoryMasterEntity.ChangedDate,
            };
        }

        public string AddSeedSubCategoryMaster(SeedSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedSubCategory = _agriContext.SeedSubCategoryMasters.Any(x => x.SeedSubCategoryId == model.SeedSubCategoryId);
            if (existingSeedSubCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                SeedSubCategoryMaster seedSubdCategoryMasterEntity = new SeedSubCategoryMaster();
                seedSubdCategoryMasterEntity.SeedSubCategoryName = model.SeedSubCategoryName;
                seedSubdCategoryMasterEntity.SeedCategoryId = model.SeedCategoryId;
                seedSubdCategoryMasterEntity.Description = model.Description;
                seedSubdCategoryMasterEntity.EnteredBy = model.EnteredBy;
                seedSubdCategoryMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.SeedSubCategoryMasters.Add(seedSubdCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedSubCategoryMaster(SeedSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var seedSubCategoryId = model.SeedSubCategoryId;
            var seedSubCategoryMasterEntity = _agriContext.SeedSubCategoryMasters.FirstOrDefault(x => x.SeedSubCategoryId == seedSubCategoryId);
            if (seedSubCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedSubCategoryMasterEntity.SeedSubCategoryName = model.SeedSubCategoryName;
                seedSubCategoryMasterEntity.SeedCategoryId = model.SeedCategoryId;
                seedSubCategoryMasterEntity.Description = model.Description;
                seedSubCategoryMasterEntity.ChangedBy = model.ChangedBy;
                seedSubCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteSeedSubCategoryMaster(long SeedSubCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedSubCategoryMasterModel model = new SeedSubCategoryMasterModel();
            var seedSubCategoryMasterEntity = _agriContext.SeedSubCategoryMasters.FirstOrDefault(x => x.SeedSubCategoryId == SeedSubCategoryId);
            if (seedSubCategoryMasterEntity != null)
            {
                seedSubCategoryMasterEntity.DeleteStatus = true;
                seedSubCategoryMasterEntity.ChangedBy = model.ChangedBy;
                seedSubCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
