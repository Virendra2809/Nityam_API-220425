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
    public class PolicyCategoryMasterService : IPolicyCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public PolicyCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<PolicyCategoryMasterModel> GetAllPolicyCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var policyCategoryMasterModelList = new List<PolicyCategoryMasterModel>();
            var policyCategoryMasterListEntity = (from policyCategory in _agriContext.PolicyCategoryMasters
                                               join policyType in _agriContext.PolicyTypeMasters
                                               on policyCategory.PolicyTypeId equals policyType.PolicyTypeId
                                               where policyCategory.DeleteStatus == false
                                               select new
                                               {
                                                   policyCategory.PolicyCategoryId,
                                                   policyCategory.PolicyCategoryName,
                                                   policyCategory.EnteredBy,
                                                   policyCategory.EnteredDate,
                                                   policyCategory.ChangedBy,
                                                   policyCategory.ChangedDate,
                                                   policyType.PolicyTypeId,
                                                   policyType.PolicyTypeName
                                               }).ToList();
            if (policyCategoryMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in policyCategoryMasterListEntity)
            {
                var model = new PolicyCategoryMasterModel();
                model.PolicyCategoryId = item.PolicyCategoryId;
                model.PolicyCategoryName = item.PolicyCategoryName;
                model.PolicyTypeId = item.PolicyTypeId;
                model.PolicyTypeName = item.PolicyTypeName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                policyCategoryMasterModelList.Add(model);
            }
            return policyCategoryMasterModelList;
        }

        public PolicyCategoryMasterModel GetPolicyCategoryeMasterById(long PolicyCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var policyCategoryMasterEntity = (from policyCategory in _agriContext.PolicyCategoryMasters
                                                  join policyType in _agriContext.PolicyTypeMasters
                                                  on policyCategory.PolicyTypeId equals policyType.PolicyTypeId
                                                  where policyCategory.DeleteStatus == false && policyCategory.PolicyCategoryId==PolicyCategoryId
                                                  select new
                                                  {
                                                      policyCategory.PolicyCategoryId,
                                                      policyCategory.PolicyCategoryName,
                                                      policyCategory.EnteredBy,
                                                      policyCategory.EnteredDate,
                                                      policyCategory.ChangedBy,
                                                      policyCategory.ChangedDate,
                                                      policyType.PolicyTypeId,
                                                      policyType.PolicyTypeName
                                                  }).FirstOrDefault();
            if (policyCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            return new PolicyCategoryMasterModel
            {
                PolicyCategoryId = policyCategoryMasterEntity.PolicyCategoryId,
                PolicyCategoryName = policyCategoryMasterEntity.PolicyCategoryName,
                PolicyTypeId = policyCategoryMasterEntity.PolicyTypeId,
                PolicyTypeName = policyCategoryMasterEntity.PolicyTypeName,
                EnteredBy = policyCategoryMasterEntity.EnteredBy,
                EnteredDate = policyCategoryMasterEntity.EnteredDate,
                ChangedBy = policyCategoryMasterEntity.ChangedBy,
                ChangedDate = policyCategoryMasterEntity.ChangedDate
            };
        }

        public string AddPolicyCategoryMaster(PolicyCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingPolicyCategory = _agriContext.PolicyCategoryMasters.Any(x => x.PolicyCategoryId == model.PolicyCategoryId);
            if (existingPolicyCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                PolicyCategoryMaster policyCategoryMasterEntity = new PolicyCategoryMaster();
                policyCategoryMasterEntity.PolicyCategoryName = model.PolicyCategoryName;
                policyCategoryMasterEntity.PolicyTypeId = model.PolicyTypeId;
                policyCategoryMasterEntity.EnteredBy = model.EnteredBy;
                policyCategoryMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.PolicyCategoryMasters.Add(policyCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdatePolicyCategoryMaster(PolicyCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var policyCategoryId = model.PolicyCategoryId;
            var policyCategoryMasterEntity = _agriContext.PolicyCategoryMasters.FirstOrDefault(x => x.PolicyCategoryId == policyCategoryId);
            if (policyCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                policyCategoryMasterEntity.PolicyCategoryName = model.PolicyCategoryName;
                policyCategoryMasterEntity.PolicyTypeId = model.PolicyTypeId;
                policyCategoryMasterEntity.ChangedBy = model.ChangedBy;
                policyCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }
        
        public string DeletePolicyCategoryMaster(long PolicyCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PolicyCategoryMasterModel model = new PolicyCategoryMasterModel();
            var policyCategoryMasterEntity = _agriContext.PolicyCategoryMasters.FirstOrDefault(x => x.PolicyCategoryId == PolicyCategoryId);
            if (policyCategoryMasterEntity != null)
            {
                policyCategoryMasterEntity.DeleteStatus = true;
                policyCategoryMasterEntity.ChangedBy = model.EnteredBy;
                policyCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }       
    }
}
