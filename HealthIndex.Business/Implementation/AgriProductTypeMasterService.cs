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
    public class AgriProductTypeMasterService : IAgriProductTypeMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public AgriProductTypeMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }
        public List<AgriProductTypeMasterModel> GetAllAgriProductTypeMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var agriProductTypeModelList = new List<AgriProductTypeMasterModel>();
            var agriProductTypeListEntity = _agriContext.AgriProductTypeMasters.Where(x => x.DeleteStatus == false).ToList();
            if (agriProductTypeListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in agriProductTypeListEntity)
            {
                var model = new AgriProductTypeMasterModel();
                model.AgriProductTypeId = item.AgriProductTypeId;
                model.AgriProductTypeName = item.AgriProductTypeName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                agriProductTypeModelList.Add(model);
            }
            return agriProductTypeModelList;
        }

        public AgriProductTypeMasterModel GetAgriProductTypeMasterById(long AgriProductTypeId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var agriProductTypeEntity = _agriContext.AgriProductTypeMasters.FirstOrDefault(x => x.AgriProductTypeId == AgriProductTypeId && !x.DeleteStatus);
            if (agriProductTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new AgriProductTypeMasterModel
            {
                AgriProductTypeName = agriProductTypeEntity.AgriProductTypeName,
                EnteredBy = agriProductTypeEntity.EnteredBy,
                EnteredDate = agriProductTypeEntity.EnteredDate,
                ChangedBy = agriProductTypeEntity.ChangedBy,
                ChangedDate = agriProductTypeEntity.ChangedDate,
            };
        }

        public string AddAgriProductTypeMaster(AgriProductTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingAgriProductType = _agriContext.AgriProductTypeMasters.Any(x => x.AgriProductTypeId == model.AgriProductTypeId);
            if (existingAgriProductType)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                AgriProductTypeMaster agriProductTyppeEntity = new AgriProductTypeMaster();
                agriProductTyppeEntity.AgriProductTypeName = model.AgriProductTypeName;
                agriProductTyppeEntity.EnteredBy = model.EnteredBy;
                agriProductTyppeEntity.EnteredDate = DateTime.Now;
                _agriContext.AgriProductTypeMasters.Add(agriProductTyppeEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateAgriProductTypeMaster(AgriProductTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var agriProductTypeId = model.AgriProductTypeId;
            var agriProductTypeEntity = _agriContext.AgriProductTypeMasters.FirstOrDefault(x => x.AgriProductTypeId == agriProductTypeId);
            if (agriProductTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                agriProductTypeEntity.AgriProductTypeName = model.AgriProductTypeName;
                agriProductTypeEntity.ChangedBy = model.ChangedBy;
                agriProductTypeEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteAgriProductTypeMaster(long AgriProductTypeId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            AgriProductTypeMasterModel model = new AgriProductTypeMasterModel();
            var agriProducttypeEntity = _agriContext.AgriProductTypeMasters.FirstOrDefault(x => x.AgriProductTypeId == AgriProductTypeId);
            if (agriProducttypeEntity != null)
            {
                agriProducttypeEntity.DeleteStatus = true;
                agriProducttypeEntity.ChangedBy = model.ChangedBy;
                agriProducttypeEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }
}
