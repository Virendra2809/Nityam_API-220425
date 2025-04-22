using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class WeedManagementMethodService: IWeedMgmtMethod
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        // IConfiguration _configuration;
        private ConfigurationModel _configuration;

        public WeedManagementMethodService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName
)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;

        }
        public List<WeedMgmtMethodModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var weedMethodModelList = new List<WeedMgmtMethodModel>();
            var weedCategoryMethodListEntity = (from WeedMgmtMethod in _agriContext.WeedMgmtMethods

                                               join WeedManagementCategory in _agriContext.WeedManagementCategories
                                               on WeedMgmtMethod.WeedMgmtCategoryId equals WeedManagementCategory.WeedMgmtCategoryId
                                                where WeedMgmtMethod.DeleteStatus==false
                                                select new
                                                {
                                                    WeedMgmtMethod.WeedMgmtMethodId,
                                                    WeedMgmtMethod.WeedMgmtMethodName,
                                                    WeedMgmtMethod.MethodImageName,
                                                    WeedMgmtMethod.ImageUrl,
                                                    WeedManagementCategory.WeedMgmtCategoryId,
                                                    WeedManagementCategory.WeedMgmtCategoryName,
                                                    WeedMgmtMethod.EnteredBy,
                                                    WeedMgmtMethod.EnteredDate,
                                                    WeedMgmtMethod.ChangedBy,
                                                    WeedMgmtMethod.ChangedDate,
                                                    WeedMgmtMethod.DeleteStatus,
                                                  
                                                }
                                  ).ToList();
            if (weedCategoryMethodListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in weedCategoryMethodListEntity)
            {
                var model = new WeedMgmtMethodModel();
                model.WeedMgmtMethodId = item.WeedMgmtMethodId;
                model.WeedMgmtMethodName = item.WeedMgmtMethodName;
                model.MethodImageName =item.MethodImageName;
                model.ImageUrl = _configuration.HostName + item.ImageUrl;
                model.WeedMgmtCategoryId = item.WeedMgmtCategoryId;
                model.WeedMgmtCategoryName = item.WeedMgmtCategoryName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;


                weedMethodModelList.Add(model);
            }
            return weedMethodModelList;
        }

        public WeedMgmtMethodModel GetById(long WeedMgmtMethodId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var weedCategoryMethodListEntity = (from WeedMgmtMethod in _agriContext.WeedMgmtMethods

                                                join WeedManagementCategory in _agriContext.WeedManagementCategories
                                                on WeedMgmtMethod.WeedMgmtCategoryId equals WeedManagementCategory.WeedMgmtCategoryId
                                                where WeedMgmtMethod.DeleteStatus==false && WeedMgmtMethod.WeedMgmtMethodId==WeedMgmtMethodId
                                                select new
                                                {
                                                    WeedMgmtMethod.WeedMgmtMethodId,
                                                    WeedMgmtMethod.WeedMgmtMethodName,
                                                    WeedMgmtMethod.MethodImageName,
                                                    WeedMgmtMethod.ImageUrl,
                                                    WeedManagementCategory.WeedMgmtCategoryId,
                                                    WeedManagementCategory.WeedMgmtCategoryName,
                                                    WeedMgmtMethod.EnteredBy,
                                                    WeedMgmtMethod.EnteredDate,
                                                    WeedMgmtMethod.ChangedBy,
                                                    WeedMgmtMethod.ChangedDate,
                                                    WeedMgmtMethod.DeleteStatus,

                                                }
                                   ).FirstOrDefault();
            if (weedCategoryMethodListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new WeedMgmtMethodModel
            {
                WeedMgmtMethodId = weedCategoryMethodListEntity.WeedMgmtMethodId,
                WeedMgmtMethodName = weedCategoryMethodListEntity.WeedMgmtMethodName,
                MethodImageName =   weedCategoryMethodListEntity.MethodImageName,
                ImageUrl = _configuration.HostName + weedCategoryMethodListEntity.ImageUrl,
                WeedMgmtCategoryId = weedCategoryMethodListEntity.WeedMgmtCategoryId,
                WeedMgmtCategoryName = weedCategoryMethodListEntity.WeedMgmtCategoryName,
                EnteredBy = weedCategoryMethodListEntity.EnteredBy,
                EnteredDate = weedCategoryMethodListEntity.EnteredDate,
                ChangedBy = weedCategoryMethodListEntity.ChangedBy,
                ChangedDate = weedCategoryMethodListEntity.ChangedDate
            };
        }

        public string Add(WeedMgmtMethodModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.WeedMgmtMethods.Any(x => x.WeedMgmtMethodId == model.WeedMgmtMethodId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                WeedMgmtMethod weedMethodEntity = new WeedMgmtMethod();
                weedMethodEntity.WeedMgmtMethodId = model.WeedMgmtMethodId;
                weedMethodEntity.WeedMgmtMethodName = model.WeedMgmtMethodName;
                weedMethodEntity.WeedMgmtCategoryId = model.WeedMgmtCategoryId;
                weedMethodEntity.EnteredBy = model.EnteredBy;
                weedMethodEntity.EnteredDate = DateTime.Now;
                weedMethodEntity.DeleteStatus = false;
                weedMethodEntity.MethodImageName = model.MethodImageName;
                weedMethodEntity.ImageUrl = model.ImageUrl;
                _agriContext.WeedMgmtMethods.Add(weedMethodEntity);
                _agriContext.SaveChanges();
                
                message = "Added Successfully";
            }
            return message;
        }

        public bool Update(WeedMgmtMethodModel model, ref ErrorResponseModel errorResponseModel)
        {
            var WeedMgmtMethodId = model.WeedMgmtMethodId;
            var weedMethodEntity = _agriContext.WeedMgmtMethods.FirstOrDefault(x => x.WeedMgmtMethodId == WeedMgmtMethodId);
            if (weedMethodEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                weedMethodEntity.WeedMgmtMethodId = model.WeedMgmtMethodId;
                weedMethodEntity.WeedMgmtMethodName = model.WeedMgmtMethodName;
                weedMethodEntity.WeedMgmtCategoryId = model.WeedMgmtCategoryId;
                weedMethodEntity.MethodImageName = model.MethodImageName;
                weedMethodEntity.ImageUrl = model.ImageUrl;
                weedMethodEntity.ChangedBy = model.ChangedBy;
                weedMethodEntity.ChangedDate = DateTime.Now;
                weedMethodEntity.DeleteStatus = false;

                _agriContext.SaveChanges();
                return true;
            }


        }

        public string Delete(long WeedMgmtMethodId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            WeedMgmtMethodModel model = new WeedMgmtMethodModel();
            var weedCategoryMasterEntity = _agriContext.WeedMgmtMethods.FirstOrDefault(x => x.WeedMgmtMethodId == WeedMgmtMethodId);
            if (weedCategoryMasterEntity != null)
            {
                weedCategoryMasterEntity.DeleteStatus = true;
                weedCategoryMasterEntity.ChangedBy = model.ChangedBy;
                weedCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        public string DeleteImageFromDB(WeedMgmtMethodModel model)
        {
            var imageEntityList = _agriContext.WeedMgmtMethods.FirstOrDefault(x => x.WeedMgmtMethodId == model.WeedMgmtMethodId);
            imageEntityList.MethodImageName = "";
            _agriContext.SaveChanges();

            return "deleted.";
        }


    }
}
