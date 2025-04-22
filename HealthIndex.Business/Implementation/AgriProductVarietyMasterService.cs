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
    public class AgriProductVarietyMasterService : IAgriProductVarietyMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public AgriProductVarietyMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }
        public List<AgriProductVarietyMasterModel> GetAllAgriProductVarietyMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var varietyModelList = new List<AgriProductVarietyMasterModel>();
            var varietyListEntity = _agriContext.AgriProductVarietyMasters.Where(x => x.DeleteStatus == false).ToList();
            if (varietyListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in varietyListEntity)
            {
                var model = new AgriProductVarietyMasterModel();
                model.VarietyId = item.VarietyId;
                model.VarietyName = item.VarietyName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate =item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                varietyModelList.Add(model);
            }
            return varietyModelList;
        }

        public AgriProductVarietyMasterModel GetAgriAgriProductVarietyMasterById(long VarietyId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var varietyEntity = _agriContext.AgriProductVarietyMasters.FirstOrDefault(x => x.VarietyId == VarietyId && !x.DeleteStatus);
            if (varietyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new AgriProductVarietyMasterModel
            {
                VarietyName = varietyEntity.VarietyName,
                EnteredBy = varietyEntity.EnteredBy,
                EnteredDate = varietyEntity.EnteredDate,
                ChangedBy = varietyEntity.ChangedBy,
                ChangedDate = varietyEntity.ChangedDate,
            };
        }

        public string AddAgriProductVarietyMaster(AgriProductVarietyMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingVariety = _agriContext.AgriProductVarietyMasters.Any(x => x.VarietyId == model.VarietyId);
            if (existingVariety)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                AgriProductVarietyMaster varietyEntity = new AgriProductVarietyMaster();
                varietyEntity.VarietyName = model.VarietyName;
                varietyEntity.EnteredBy = model.EnteredBy;
                varietyEntity.EnteredDate = DateTime.Now;
                _agriContext.AgriProductVarietyMasters.Add(varietyEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateAgriProductVarietyMaster(AgriProductVarietyMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var varietyId = model.VarietyId;
            var varietyEntity = _agriContext.AgriProductVarietyMasters.FirstOrDefault(x => x.VarietyId == varietyId);
            if (varietyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                varietyEntity.VarietyName = model.VarietyName;
                varietyEntity.ChangedBy = model.ChangedBy;
                varietyEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteAgriProductVarietyMaster(long VarietyId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            AgriProductVarietyMasterModel model = new AgriProductVarietyMasterModel();
            var varietyEntity = _agriContext.AgriProductVarietyMasters.FirstOrDefault(x => x.VarietyId == VarietyId);
            if (varietyEntity != null)
            {
                varietyEntity.DeleteStatus = true;
                varietyEntity.ChangedBy = model.ChangedBy;
                varietyEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }      
       
    }
}
