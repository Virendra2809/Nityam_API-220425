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
    public class FirmDetailService : IFirmDetail
    {
        AgtonomicsAgriCultureDbContext _agriContext;

        IConfiguration _configuration;

        public object FirmDetail { get; private set; }

        public object Value => throw new NotImplementedException();

        object IFirmDetail.Value => throw new NotImplementedException();

        public FirmDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<FirmdetailModel> GetAllFirm()
        {
            var errorResponseModel = new ErrorResponseModel();
            var FirmdetailModelList = new List<FirmdetailModel>();
            var FirmListEntity = (from FirmDetails in _agriContext.FirmDetails
                                  join LanguageMaster in _agriContext.LanguageMasters
                                  on FirmDetails.LanguageIds equals LanguageMaster.LanguageId.ToString()
                                  where FirmDetails.DeleteStatus == false

                                  join ModuleMaster in _agriContext.ModuleMasters
                                  on FirmDetails.ModuleIds equals ModuleMaster.ModuleId.ToString()
                                  where FirmDetails.DeleteStatus == false

                                  select new
                                  {
                                      FirmDetails.FirmId,
                                      FirmDetails.FirmName,
                                      FirmDetails.FirmNameMarathi,
                                      FirmDetails.FirmRegNumber,
                                      FirmDetails.FirmRegDate,
                                      FirmDetails.FirmBranchName,
                                      FirmDetails.FirmBranchNameMarathi,
                                      FirmDetails.FirmOfficeAddress,
                                      FirmDetails.FirmOfficeAddressMarathi,
                                      FirmDetails.FirmLogo,
                                      FirmDetails.FirmPhoneNumber,
                                      FirmDetails.FirmFaxNumber,
                                      FirmDetails.FirmEmailIid,
                                      FirmDetails.MailPassword,
                                      FirmDetails.IsFederation,
                                      FirmDetails.FirmConnectionPath,
                                      LanguageMaster.LanguageId,
                                      LanguageMaster.LanguageName,
                                      FirmDetails.ParentFirmId,
                                      ModuleMaster.ModuleId,
                                      ModuleMaster.ModuleName,
                                      FirmDetails.UserLimit,
                                      FirmDetails.EnteredBy,
                                      FirmDetails.EnteredDate,
                                      FirmDetails.ChangedBy,
                                      FirmDetails.ChangedDate,
                                      FirmDetails.DeleteStatus,
                                      FirmDetails.IsNeedToBeSingleTerminalLogin,
                                      FirmDetails.DatabaseBackupPath,
                                      FirmDetails.IsDateOverlap,
                                      FirmDetails.ApplicationLockDate,
                                      FirmDetails.IsLockApplication,
                                      FirmDetails.IsSyncStaring

                                  }
                                  ).ToList();
            if (FirmListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in FirmListEntity)
            {
                var model = new FirmdetailModel();
                model.FirmId = item.FirmId;
                model.FirmName = item.FirmName;
                model.FirmNameMarathi = item.FirmNameMarathi;
                model.FirmRegNumber = item.FirmRegNumber;
                model.FirmRegDate = item.FirmRegDate;
                model.FirmBranchName = item.FirmBranchName;
                model.FirmBranchNameMarathi = item.FirmBranchNameMarathi;
                model.FirmOfficeAddress = item.FirmBranchNameMarathi;
                model.FirmOfficeAddressMarathi = item.FirmBranchNameMarathi;
                model.FirmLogo = item.FirmLogo;
                model.FirmPhoneNumber = item.FirmPhoneNumber;
                model.FirmFaxNumber = item.FirmFaxNumber;
                model.FirmEmailIid = item.FirmEmailIid;
                model.MailPassword = item.MailPassword;
                model.IsFederation = item.IsFederation;
                model.FirmConnectionPath = item.FirmConnectionPath;
                model.LanguageIds = Convert.ToString(item.LanguageId);
                model.LanguageName = item.LanguageName;
                model.ParentFirmId = item.ParentFirmId;
                model.ModuleIds = Convert.ToString(item.ModuleId);
                model.ModuleName = item.ModuleName;
                model.UserLimit = item.UserLimit;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.IsNeedToBeSingleTerminalLogin = item.IsNeedToBeSingleTerminalLogin ?? false;
                model.DatabaseBackupPath = item.DatabaseBackupPath;
                model.IsDateOverlap = item.IsDateOverlap;
                model.ApplicationLockDate = Convert.ToDateTime(item.ApplicationLockDate);
                model.IsLockApplication = item.IsLockApplication;
                model.IsSyncStaring = item.IsSyncStaring;

                FirmdetailModelList.Add(model);
            }
            return FirmdetailModelList;

        }

        public string AddFirm(FirmdetailModel model, ref ErrorResponseModel errorResponseModel)
        {

            var message = string.Empty;

            var existingFirm = _agriContext.FirmDetails.Any(x => x.FirmEmailIid == model.FirmEmailIid);
            if (existingFirm)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var FirmEntity = new FirmDetail();

                FirmEntity.FirmName = model.FirmName;
                FirmEntity.FirmNameMarathi = model.FirmNameMarathi;
                FirmEntity.FirmRegNumber = model.FirmRegNumber;
                FirmEntity.FirmRegDate = model.FirmRegDate;
                FirmEntity.FirmBranchName = model.FirmBranchName;
                FirmEntity.FirmBranchNameMarathi = model.FirmBranchNameMarathi;
                FirmEntity.FirmOfficeAddress = model.FirmBranchNameMarathi;
                FirmEntity.FirmOfficeAddressMarathi = model.FirmBranchNameMarathi;
                FirmEntity.FirmLogo = model.FirmLogo;
                FirmEntity.FirmPhoneNumber = model.FirmPhoneNumber;
                FirmEntity.FirmFaxNumber = model.FirmFaxNumber;
                FirmEntity.FirmEmailIid = model.FirmEmailIid;
                FirmEntity.MailPassword = model.MailPassword;
                FirmEntity.IsFederation = model.IsFederation;
                FirmEntity.FirmConnectionPath = model.FirmConnectionPath;
                FirmEntity.LanguageIds = model.LanguageIds;
                FirmEntity.ParentFirmId = model.ParentFirmId;
                FirmEntity.ModuleIds = model.ModuleIds;
                FirmEntity.UserLimit = model.UserLimit;
                FirmEntity.EnteredBy = model.EnteredBy;
                FirmEntity.EnteredDate = DateTime.Now;
                FirmEntity.ChangedBy = model.ChangedBy;
                FirmEntity.ChangedDate = DateTime.Now;
                FirmEntity.DeleteStatus = model.DeleteStatus;
                FirmEntity.IsNeedToBeSingleTerminalLogin = model.IsNeedToBeSingleTerminalLogin;
                FirmEntity.DatabaseBackupPath = model.DatabaseBackupPath;
                FirmEntity.IsDateOverlap = model.IsDateOverlap;
                FirmEntity.ApplicationLockDate = Convert.ToDateTime(model.ApplicationLockDate);
                FirmEntity.IsLockApplication = model.IsLockApplication;
                FirmEntity.IsSyncStaring = model.IsSyncStaring;

                _agriContext.FirmDetails.Add(FirmEntity);
                _agriContext.SaveChanges();
                message = "firmDetail added";
                
            }
            return message;
        }


        public bool Put(FirmdetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var FirmId = Convert.ToInt32(model.FirmId);
            var FirmEntity = _agriContext.FirmDetails.FirstOrDefault(x => x.FirmId == FirmId);
            if (FirmEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                FirmEntity.FirmName = model.FirmName;
                FirmEntity.FirmNameMarathi = model.FirmNameMarathi;
                FirmEntity.FirmRegNumber = model.FirmRegNumber;
                FirmEntity.FirmRegDate = model.FirmRegDate;
                FirmEntity.FirmBranchName = model.FirmBranchName;
                FirmEntity.FirmBranchNameMarathi = model.FirmBranchNameMarathi;
                FirmEntity.FirmOfficeAddress = model.FirmBranchNameMarathi;
                FirmEntity.FirmOfficeAddressMarathi = model.FirmBranchNameMarathi;
                FirmEntity.FirmLogo = model.FirmLogo;
                FirmEntity.FirmPhoneNumber = model.FirmPhoneNumber;
                FirmEntity.FirmFaxNumber = model.FirmFaxNumber;
                FirmEntity.FirmEmailIid = model.FirmEmailIid;
                FirmEntity.MailPassword = model.MailPassword;
                FirmEntity.IsFederation = model.IsFederation;
                FirmEntity.FirmConnectionPath = model.FirmConnectionPath;
                FirmEntity.LanguageIds = model.LanguageIds;
                FirmEntity.ParentFirmId = model.ParentFirmId;
                FirmEntity.ModuleIds = model.ModuleIds;
                FirmEntity.UserLimit = model.UserLimit;
                FirmEntity.ChangedBy = model.ChangedBy;
                FirmEntity.ChangedDate = DateTime.Now; ;
                FirmEntity.DeleteStatus = model.DeleteStatus;
                FirmEntity.IsNeedToBeSingleTerminalLogin = model.IsNeedToBeSingleTerminalLogin;
                model.DatabaseBackupPath = model.DatabaseBackupPath;
                FirmEntity.IsDateOverlap = model.IsDateOverlap;
                FirmEntity.ApplicationLockDate = Convert.ToDateTime(model.ApplicationLockDate);
                FirmEntity.IsLockApplication = model.IsLockApplication;
                FirmEntity.IsSyncStaring = model.IsSyncStaring;
             
               
                _agriContext.SaveChanges();
                return true;
            }
        }

        FirmdetailModel IFirmDetail.GetById(long FirmId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var firmEntity = (from FirmDetails in _agriContext.FirmDetails
                              join LanguageMaster in _agriContext.LanguageMasters
                              on FirmDetails.LanguageIds equals LanguageMaster.LanguageId.ToString()
                              where FirmDetails.DeleteStatus == false && FirmDetails.FirmId == FirmId

                              join ModuleMaster in _agriContext.ModuleMasters
                                 on FirmDetails.ModuleIds equals ModuleMaster.ModuleId.ToString()
                              where FirmDetails.DeleteStatus == false && FirmDetails.FirmId == FirmId

                              select new
                              {
                                  FirmDetails.FirmId,
                                  FirmDetails.FirmName,
                                  FirmDetails.FirmNameMarathi,
                                  FirmDetails.FirmRegNumber,
                                  FirmDetails.FirmRegDate,
                                  FirmDetails.FirmBranchName,
                                  FirmDetails.FirmBranchNameMarathi,
                                  FirmDetails.FirmOfficeAddress,
                                  FirmDetails.FirmOfficeAddressMarathi,
                                  FirmDetails.FirmLogo,
                                  FirmDetails.FirmPhoneNumber,
                                  FirmDetails.FirmFaxNumber,
                                  FirmDetails.FirmEmailIid,
                                  FirmDetails.MailPassword,
                                  FirmDetails.IsFederation,
                                  FirmDetails.FirmConnectionPath,
                                  LanguageMaster.LanguageId,
                                  LanguageMaster.LanguageName,
                                  FirmDetails.ParentFirmId,
                                  ModuleMaster.ModuleId,
                                  ModuleMaster.ModuleName,
                                  FirmDetails.UserLimit,
                                  FirmDetails.EnteredBy,
                                  FirmDetails.EnteredDate,
                                  FirmDetails.ChangedBy,
                                  FirmDetails.ChangedDate,
                                  FirmDetails.DeleteStatus,
                                  FirmDetails.IsNeedToBeSingleTerminalLogin,
                                  FirmDetails.DatabaseBackupPath,
                                  FirmDetails.IsDateOverlap,
                                  FirmDetails.ApplicationLockDate,
                                  FirmDetails.IsLockApplication,
                                  FirmDetails.IsSyncStaring

                              }).FirstOrDefault(); 
            if (firmEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new FirmdetailModel
            {

                FirmId = firmEntity.FirmId,
                FirmName = firmEntity.FirmName,
                FirmNameMarathi = firmEntity.FirmNameMarathi,
                FirmRegNumber = firmEntity.FirmRegNumber,
                FirmRegDate = firmEntity.FirmRegDate,
                FirmBranchName = firmEntity.FirmBranchName,
                FirmBranchNameMarathi = firmEntity.FirmBranchNameMarathi,
                FirmOfficeAddress = firmEntity.FirmBranchNameMarathi,
                FirmOfficeAddressMarathi = firmEntity.FirmBranchNameMarathi,
                FirmLogo = firmEntity.FirmLogo,
                FirmPhoneNumber = firmEntity.FirmPhoneNumber,
                FirmFaxNumber = firmEntity.FirmFaxNumber,
                FirmEmailIid = firmEntity.FirmEmailIid,
                MailPassword = firmEntity.MailPassword,
                IsFederation = firmEntity.IsFederation,
                FirmConnectionPath = firmEntity.FirmConnectionPath,
                LanguageIds = Convert.ToString(firmEntity.LanguageId),
                LanguageName=firmEntity.LanguageName,
                ParentFirmId = firmEntity.ParentFirmId,
                ModuleIds = Convert.ToString(firmEntity.ModuleId),
                ModuleName=firmEntity.ModuleName,
                UserLimit = firmEntity.UserLimit,
                EnteredBy = firmEntity.EnteredBy,
                EnteredDate = firmEntity.EnteredDate,
                ChangedBy = firmEntity.ChangedBy,
                ChangedDate = firmEntity.ChangedDate,
                DeleteStatus = firmEntity.DeleteStatus,
                IsNeedToBeSingleTerminalLogin = firmEntity.IsNeedToBeSingleTerminalLogin ?? false,
                DatabaseBackupPath = firmEntity.DatabaseBackupPath,
                IsDateOverlap = firmEntity.IsDateOverlap,
                ApplicationLockDate = Convert.ToDateTime(firmEntity.ApplicationLockDate),
                IsLockApplication = firmEntity.IsLockApplication,
                IsSyncStaring = firmEntity.IsSyncStaring
            };

        }

        public string Delete(FirmdetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var firmEntity = _agriContext.FirmDetails.FirstOrDefault(x => x.FirmId == model.FirmId && !x.DeleteStatus);
            if (firmEntity != null)
            {
                firmEntity.DeleteStatus =model.DeleteStatus;
                firmEntity.ChangedBy = model.EnteredBy;
                firmEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                Message = "Firmdetails  Deleted Successfully";
            }
            return Message;
        }

        public string Delete(long FirmId, ref ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }
    }
}


