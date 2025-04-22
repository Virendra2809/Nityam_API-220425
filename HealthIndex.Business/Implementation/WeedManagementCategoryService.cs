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
    public class WeedManagementCategoryService: IWeedManagementCategory
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public WeedManagementCategoryService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }
        public List<WeedManagementCategoryModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var weedCategoryModelList = new List<WeedManagementCategoryModel>();
            var weedCategoryListEntity = _agriContext.WeedManagementCategories.Where(x => x.DeleteStatus == false).ToList();
            if (weedCategoryListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in weedCategoryListEntity)
            {
                var model = new WeedManagementCategoryModel();
                model.WeedMgmtCategoryId = item.WeedMgmtCategoryId;
                model.WeedMgmtCategoryName = item.WeedMgmtCategoryName;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                weedCategoryModelList.Add(model);
            }
            return weedCategoryModelList;
        }

        public WeedManagementCategoryModel GetById(long WeedMgmtCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var weedCategoryEntity = _agriContext.WeedManagementCategories.FirstOrDefault(x => x.WeedMgmtCategoryId == WeedMgmtCategoryId && x.DeleteStatus==false);
            if (weedCategoryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new WeedManagementCategoryModel
            {
                WeedMgmtCategoryId = weedCategoryEntity.WeedMgmtCategoryId,
                WeedMgmtCategoryName = weedCategoryEntity.WeedMgmtCategoryName,
                SeqNo = (int)weedCategoryEntity.SeqNo,
                EnteredBy = weedCategoryEntity.EnteredBy,
                EnteredDate = weedCategoryEntity.EnteredDate,
                ChangedBy = weedCategoryEntity.ChangedBy,
                ChangedDate = weedCategoryEntity.ChangedDate
            };
        }

        public string Add(WeedManagementCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.WeedManagementCategories.Any(x => x.SeqNo == model.SeqNo);
            if (existing)
            {
                // message = GlobalConstants.NotFoundMessage;
                return null;
            }
            else
            {
                WeedManagementCategory weedMasterEntity = new WeedManagementCategory();
                weedMasterEntity.WeedMgmtCategoryId = model.WeedMgmtCategoryId;
                weedMasterEntity.WeedMgmtCategoryName = model.WeedMgmtCategoryName;
                weedMasterEntity.SeqNo = model.SeqNo;
                weedMasterEntity.EnteredBy = model.EnteredBy;
                weedMasterEntity.EnteredDate = DateTime.Now;
                weedMasterEntity.DeleteStatus = false;
                _agriContext.WeedManagementCategories.Add(weedMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool Update(WeedManagementCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var WeedMgmtCategoryId = model.WeedMgmtCategoryId;
            var weedCategoryMasterEntity = _agriContext.WeedManagementCategories.FirstOrDefault(x => x.WeedMgmtCategoryId == WeedMgmtCategoryId);
            if (weedCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                weedCategoryMasterEntity.WeedMgmtCategoryId = model.WeedMgmtCategoryId;
                weedCategoryMasterEntity.WeedMgmtCategoryName = model.WeedMgmtCategoryName;
                weedCategoryMasterEntity.SeqNo = model.SeqNo;
                weedCategoryMasterEntity.ChangedBy = model.ChangedBy;
                weedCategoryMasterEntity.ChangedDate = DateTime.Now;
                weedCategoryMasterEntity.DeleteStatus = false;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long WeedMgmtCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            WeedManagementCategoryModel model = new WeedManagementCategoryModel();
            var weedCategoryMasterEntity = _agriContext.WeedManagementCategories.FirstOrDefault(x => x.WeedMgmtCategoryId == WeedMgmtCategoryId);
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

    }
}

