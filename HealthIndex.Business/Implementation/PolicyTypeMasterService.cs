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
    public class PolicyTypeMasterService : IPolicyTypeMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public PolicyTypeMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<PolicyTypeMasterModel> GetAllPolicyTypeMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var policyTypeModelList = new List<PolicyTypeMasterModel>();
            var policyTypeListEntity = _agriContext.PolicyTypeMasters.Where(x => x.DeleteStatus == false).ToList();
            if (policyTypeListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in policyTypeListEntity)
            {
                var model = new PolicyTypeMasterModel();
                model.PolicyTypeId = item.PolicyTypeId;
                model.PolicyTypeName = item.PolicyTypeName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                policyTypeModelList.Add(model);
            }
            return policyTypeModelList;
        }

        public PolicyTypeMasterModel GetPolicyTypeMasterById(long PolicyTypeId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var policyTypeEntity = _agriContext.PolicyTypeMasters.FirstOrDefault(x => x.PolicyTypeId == PolicyTypeId && !x.DeleteStatus);
            if (policyTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new PolicyTypeMasterModel
            {
                PolicyTypeName = policyTypeEntity.PolicyTypeName,
                Description=policyTypeEntity.Description,
                EnteredBy = policyTypeEntity.EnteredBy,
                EnteredDate = policyTypeEntity.EnteredDate,
                ChangedBy = policyTypeEntity.ChangedBy,
                ChangedDate = policyTypeEntity.ChangedDate
            };
        }

        public string AddPolicyTypeMaster(PolicyTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingPolicyType = _agriContext.PolicyTypeMasters.Any(x => x.PolicyTypeId == model.PolicyTypeId);
            if (existingPolicyType)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                PolicyTypeMaster policyTyppeEntity = new PolicyTypeMaster();
                policyTyppeEntity.PolicyTypeName = model.PolicyTypeName;
                policyTyppeEntity.Description = model.Description;
                policyTyppeEntity.EnteredBy = model.EnteredBy;
                policyTyppeEntity.EnteredDate = DateTime.Now;
                _agriContext.PolicyTypeMasters.Add(policyTyppeEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdatePolicyTypeMaster(PolicyTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var policyTypeId = model.PolicyTypeId;
            var policyTypeEntity = _agriContext.PolicyTypeMasters.FirstOrDefault(x => x.PolicyTypeId == policyTypeId);
            if (policyTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                policyTypeEntity.PolicyTypeName = model.PolicyTypeName;
                policyTypeEntity.Description = model.Description;
                policyTypeEntity.ChangedBy = model.ChangedBy;
                policyTypeEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeletePolicyTypeMaster(long PolicyTypeId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PolicyTypeMasterModel model = new PolicyTypeMasterModel();
            var policyTypeEntity = _agriContext.PolicyTypeMasters.FirstOrDefault(x => x.PolicyTypeId == PolicyTypeId);
            if (policyTypeEntity != null)
            {
                policyTypeEntity.DeleteStatus = true;
                policyTypeEntity.ChangedBy = model.EnteredBy;
                policyTypeEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }      
    }
}
