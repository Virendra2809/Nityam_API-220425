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
    public class SeedCategoryMasterService : ISeedCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;
        public SeedCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<SeedCategoryMasterModel> GetAllSeedCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var SeedCategoryModelList = new List<SeedCategoryMasterModel>();
            var SeedCategoryModelListEntity = _agriContext.SeedCategoryMasters.Where(x => x.DeleteStatus == false).ToList();
            if (SeedCategoryModelListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in SeedCategoryModelListEntity)
            {
                var model = new SeedCategoryMasterModel();
                model.SeedCategoryId = item.SeedCategoryId;
                model.SeedCategoryName = item.SeedCategoryName;
                model.Description = item.Description;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                SeedCategoryModelList.Add(model);
            }
            return SeedCategoryModelList;
        }

        public SeedCategoryMasterModel GetSeedCategoryMasterById(long SeedCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedCategoryMasterEntity = _agriContext.SeedCategoryMasters.FirstOrDefault(x => x.SeedCategoryId == SeedCategoryId && !x.DeleteStatus);
            if (seedCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedCategoryMasterModel
            {
                SeedCategoryName = seedCategoryMasterEntity.SeedCategoryName,
                Description = seedCategoryMasterEntity.Description,
                SeqNo =(int)seedCategoryMasterEntity.SeqNo,
                EnteredBy = seedCategoryMasterEntity.EnteredBy,
                EnteredDate = seedCategoryMasterEntity.EnteredDate,
                ChangedBy = seedCategoryMasterEntity.ChangedBy,
                ChangedDate = seedCategoryMasterEntity.ChangedDate
            };
        }

        public string AddSeedCategoryMaster(SeedCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedCategory = _agriContext.SeedCategoryMasters.Any(x => x.SeedCategoryName == model.SeedCategoryName && x.DeleteStatus==false);
            var existingSequence = _agriContext.SeedCategoryMasters.Any(x => x.SeqNo == model.SeqNo && x.DeleteStatus==false);
            if (existingSeedCategory)
            {
                message = GlobalConstants.ExistingName;
            }  
            else if( existingSequence)
            {
                message = GlobalConstants.ExistingSequenceNumber;
            }
            else
            {
                SeedCategoryMaster seedCategoryMasterEntity = new SeedCategoryMaster();
                seedCategoryMasterEntity.SeedCategoryName = model.SeedCategoryName;
                seedCategoryMasterEntity.Description = model.Description;
                seedCategoryMasterEntity.SeqNo = model.SeqNo;
                seedCategoryMasterEntity.EnteredBy = model.EnteredBy;
                seedCategoryMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.SeedCategoryMasters.Add(seedCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedCategoryMaster(SeedCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var seedCategoryId = model.SeedCategoryId;
            var seedCategoryMasterEntity = _agriContext.SeedCategoryMasters.FirstOrDefault(x => x.SeedCategoryId == seedCategoryId);
            if (seedCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedCategoryMasterEntity.SeedCategoryName = model.SeedCategoryName;
                seedCategoryMasterEntity.Description = model.Description;
                seedCategoryMasterEntity.SeqNo = model.SeqNo;
                seedCategoryMasterEntity.ChangedBy = model.ChangedBy;
                seedCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        string ISeedCategoryMasterService.DeleteSeedCategoryMaster(long SeedCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedCategoryMasterModel model = new SeedCategoryMasterModel();
            var seedCategoryMasterEntity = _agriContext.SeedCategoryMasters.FirstOrDefault(x => x.SeedCategoryId == SeedCategoryId);
            if (seedCategoryMasterEntity != null)
            {
                seedCategoryMasterEntity.DeleteStatus = true;
                seedCategoryMasterEntity.ChangedBy = model.ChangedBy;
                seedCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }                
    }
}



