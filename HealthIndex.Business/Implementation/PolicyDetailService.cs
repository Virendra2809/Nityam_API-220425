using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class PolicyDetailService : IPolicyDetailService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;
        private INotificationService _notificationService;
        
        public PolicyDetailService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName,INotificationService notificationService)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;
            _notificationService = notificationService;
           

        }

        public List<PolicyDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var PolicyDetailModelList = new List<PolicyDetailModel>();
            var PolicyListEntity = (from Policy in _agriContext.PolicyDetails
                                  join category in _agriContext.PolicyCategoryMasters
                                  on Policy.PolicyCategoryId equals category.PolicyCategoryId
                                  where Policy.IsActive == true

                                  select new
                                  {
                                      Policy.PolicyId,
                                      category.PolicyCategoryId,
                                      Policy.StartDate,
                                      Policy.EndDate,
                                      category.PolicyCategoryName,
                                      Policy.Description,
                                      Policy.PolicyUrl,
                                      Policy.PolicyTitle,
                                      Policy.SeqNo
                                  }).ToList();
            if (PolicyListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in PolicyListEntity)
            {
                var model = new PolicyDetailModel();
                model.PolicyId = item.PolicyId;
                model.PolicyCategoryId = item.PolicyCategoryId;
                model.PolicyCategoryName = item.PolicyCategoryName;
                model.PolicyTitle = item.PolicyTitle;
                model.PolicyUrl = item.PolicyUrl;
                model.StartDate = item.StartDate;
                model.PolicyUrl = item.PolicyUrl;
                model.EndDate = item.EndDate;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                PolicyDetailModelList.Add(model);
            }
            return PolicyDetailModelList;
        }

         public PolicyDetailModel GetById(long PolicyId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var PolicyListEntity = (from Policy in _agriContext.PolicyDetails
                                     join category in _agriContext.PolicyCategoryMasters
                                     on Policy.PolicyCategoryId equals category.PolicyCategoryId
                                      where Policy.IsActive == true && Policy.PolicyId == PolicyId

                                  select new
                                  {
                                      Policy.PolicyId,
                                      category.PolicyCategoryId,
                                      Policy.StartDate,
                                      Policy.EndDate,
                                      category.PolicyCategoryName,
                                      Policy.Description,
                                      Policy.PolicyUrl,
                                      Policy.PolicyTitle,
                                      Policy.SeqNo
                                         }).FirstOrDefault();

            if (PolicyListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new PolicyDetailModel();

            model.PolicyId = PolicyListEntity.PolicyId;
            model.PolicyCategoryId = PolicyListEntity.PolicyCategoryId;
            model.PolicyCategoryName = PolicyListEntity.PolicyCategoryName;
            model.PolicyTitle = PolicyListEntity.PolicyTitle;
            model.PolicyUrl = PolicyListEntity.PolicyUrl;
            model.StartDate = PolicyListEntity.StartDate;
            model.PolicyUrl = PolicyListEntity.PolicyUrl;
            model.EndDate = PolicyListEntity.EndDate;
            model.Description = PolicyListEntity.Description;
            model.SeqNo = PolicyListEntity.SeqNo;

            if (PolicyListEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }


        public string Add(PolicyDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.PolicyDetails.Any(x => x.PolicyId == model.PolicyId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {

                var PolicyEntity = new PolicyDetail();
                PolicyEntity.PolicyId = model.PolicyId;
                PolicyEntity.PolicyCategoryId = model.PolicyCategoryId;
                PolicyEntity.Description = model.Description;
                PolicyEntity.PolicyTitle = model.PolicyTitle;
                PolicyEntity.StartDate = model.StartDate;
                PolicyEntity.EndDate = model.EndDate;
                PolicyEntity.IsActive = true;
                PolicyEntity.PolicyUrl = model.PolicyUrl;
                PolicyEntity.EnteredBy = model.EnteredBy;
                PolicyEntity.EnteredDate = DateTime.Today;
                PolicyEntity.SeqNo = model.SeqNo;
                var farmerlist= _agriContext.FarmerMasters.Where(x => x.DeleteStatus == false && x.DeviceToken != null).ToList();
                foreach (var item in farmerlist)
                {
                    _notificationService.SendNotification(item.DeviceToken,0, model.PolicyTitle, true, model.Description, "Policy");

                }
                _agriContext.PolicyDetails.Add(PolicyEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
                }
            return message;
        }

        public bool Put(PolicyDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var PolicyId = model.PolicyId;
            var PolicyEntity = _agriContext.PolicyDetails.FirstOrDefault(x => x.PolicyId == PolicyId);
            if (PolicyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                PolicyEntity.PolicyId = model.PolicyId;
                PolicyEntity.PolicyCategoryId = model.PolicyCategoryId;
                PolicyEntity.PolicyTitle = model.PolicyTitle;
                PolicyEntity.StartDate = model.StartDate;
                PolicyEntity.EndDate = model.EndDate;
                PolicyEntity.IsActive = true;
                PolicyEntity.PolicyUrl = model.PolicyUrl;
                PolicyEntity.ChangedBy = model.ChangedBy;
                PolicyEntity.ChangedDate = DateTime.Today;
                PolicyEntity.Description = model.Description;
                PolicyEntity.SeqNo = model.SeqNo;
                _agriContext.SaveChanges();
            }
                var farmerlist = _agriContext.FarmerMasters.Where(x => x.DeleteStatus == false && x.DeviceToken != null).ToList();
                foreach (var item in farmerlist)
                {
                    _notificationService.SendNotification(item.DeviceToken, 0, model.PolicyTitle, true, model.Description, "Policy");
                }
            
            return true;
            }
    
        public string Delete(long PolicyId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            PolicyDetailModel model = new PolicyDetailModel();
            var PolicyEntity = _agriContext.PolicyDetails.FirstOrDefault(x => x.PolicyId == PolicyId);
            if (PolicyEntity != null)
            {
                PolicyEntity.IsActive = false;
                PolicyEntity.ChangedBy = model.ChangedBy;
                PolicyEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public List<PolicyDetailModel> GetPolicyByCategory(long CategoryId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var PolicyDetailModelList = new List<PolicyDetailModel>();
            var PolicyListEntity = (from Policy in _agriContext.PolicyDetails
                                  join category in _agriContext.PolicyCategoryMasters
                                  on Policy.PolicyCategoryId equals category.PolicyCategoryId
                                  where Policy.IsActive == true && Policy.PolicyCategoryId == CategoryId
                                  select new
                                  {
                                      Policy.PolicyId,
                                      category.PolicyCategoryId,
                                      Policy.StartDate,
                                      Policy.EndDate,
                                      category.PolicyCategoryName,
                                      Policy.Description,
                                      Policy.PolicyUrl,
                                      Policy.PolicyTitle,
                                      Policy.SeqNo

                                  }).ToList();
            if (PolicyListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in PolicyListEntity)
            {
                var model = new PolicyDetailModel();
                model.PolicyId = item.PolicyId;
                model.PolicyCategoryId = item.PolicyCategoryId;
                model.PolicyCategoryName = item.PolicyCategoryName;
                model.PolicyTitle = item.PolicyTitle;
                model.PolicyUrl = item.PolicyUrl;
                model.StartDate = item.StartDate;
                model.PolicyUrl = item.PolicyUrl;
                model.EndDate = item.EndDate;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                PolicyDetailModelList.Add(model);
            }
            return PolicyDetailModelList;
        }
    }
}



