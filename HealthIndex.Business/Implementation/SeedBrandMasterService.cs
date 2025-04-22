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
    public class SeedBrandMasterService : ISeedBrandMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public SeedBrandMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }
        public List<SeedBrandMasterModel> GetAllSeedBrandMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var SeedBrandModelList = new List<SeedBrandMasterModel>();
            var SeedBrandMasterListEntity = _agriContext.SeedBrandMasters.Where(x => x.DeleteStatus == false).ToList();
            if (SeedBrandMasterListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in SeedBrandMasterListEntity)
            {
                var model = new SeedBrandMasterModel();
                model.SeedBrandId = item.SeedBrandId;
                model.SeedBrandName = item.SeedBrandName;
                model.Description = item.Description;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                SeedBrandModelList.Add(model);
            }
            return SeedBrandModelList;
        }
       
        public SeedBrandMasterModel GetSeedBrandMasterById(long SeedBrandId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedBrandMasterEntity = _agriContext.SeedBrandMasters.FirstOrDefault(x => x.SeedBrandId == SeedBrandId && !x.DeleteStatus);
            if (seedBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedBrandMasterModel
            {
                SeedBrandName = seedBrandMasterEntity.SeedBrandName,
                Description = seedBrandMasterEntity.Description,
                SeqNo = (int)seedBrandMasterEntity.SeqNo,
                EnteredBy = seedBrandMasterEntity.EnteredBy,
                EnteredDate = seedBrandMasterEntity.EnteredDate,
                ChangedBy = seedBrandMasterEntity.ChangedBy,
                ChangedDate = seedBrandMasterEntity.ChangedDate
            };
        }

        public string AddSeedBrandMaster(SeedBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedBrand = _agriContext.SeedBrandMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingSeedBrand)
            {
                return null;
            }
            else
            {
                SeedBrandMaster seedBrandMasterEntity = new SeedBrandMaster();
                seedBrandMasterEntity.SeedBrandName = model.SeedBrandName;
                seedBrandMasterEntity.Description = model.Description;
                seedBrandMasterEntity.SeqNo = model.SeqNo;
                seedBrandMasterEntity.EnteredBy = model.EnteredBy;
                seedBrandMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.SeedBrandMasters.Add(seedBrandMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedBrandMaster(SeedBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var seedBrandId = model.SeedBrandId;
            var seedBrandMasterEntity = _agriContext.SeedBrandMasters.FirstOrDefault(x => x.SeedBrandId == seedBrandId);
            if (seedBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedBrandMasterEntity.SeedBrandName = model.SeedBrandName;
                seedBrandMasterEntity.Description = model.Description;
                seedBrandMasterEntity.SeqNo = model.SeqNo;
                seedBrandMasterEntity.ChangedBy = model.ChangedBy;
                seedBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }            
        }

        public string DeleteSeedBrandMaster(long SeedBrandId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeedBrandMasterModel model = new SeedBrandMasterModel();
            var seedBrandMasterEntity = _agriContext.SeedBrandMasters.FirstOrDefault(x => x.SeedBrandId == SeedBrandId);
            if (seedBrandMasterEntity != null)
            {
                seedBrandMasterEntity.DeleteStatus = true;
                seedBrandMasterEntity.ChangedBy = model.ChangedBy;
                seedBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}
