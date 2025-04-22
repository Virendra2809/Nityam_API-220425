using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Linq;
using System.Net;

namespace HealthIndex.Business.Implementation
{
    public class AuthService : IAuthService
    {
        HealthIndexDbContext _HealthIndexDbContext;
        public AuthService(HealthIndexDbContext healthIndexDbContext)
        {
            _HealthIndexDbContext = healthIndexDbContext;

        }




        public AuthorModel AuthenticateUser(string email, string password, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            
            var userEntity = (from user in _HealthIndexDbContext.UserMasters
                              join role in _HealthIndexDbContext.RoleMasters
                              on user.RoleId equals role.RoleId
                              where user.EmailId == email && ((user.UserPassword).Equals(password)) && user.DeleteStatus==false
                              select new
                              {
                                  user.UserId,
                                  user.UserName,
                                  user.FirstName,
                                  user.LastName,
                                  user.EmailId,
                                  role.RoleId,
                                  role.RoleName,
                              }).FirstOrDefault();

            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
                return null;
            }
            return new AuthorModel
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                EmailId = userEntity.EmailId,
                UserId = userEntity.UserId,
                UserName = userEntity.UserName,
                RoleId = userEntity.RoleId,
                Role = userEntity.RoleName,
            };
        }

        public AuthModel AuthenticateUser(string MobileNo, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userExist = _HealthIndexDbContext.UserMasters.FirstOrDefault(x => x.MobileNo == MobileNo && x.IsUserActivated == true && x.DeleteStatus == false);
            if (userExist == null)
            {
                
                errorResponseModel.Message = "Please Enter Valid Mobile Number";
                return null;
            }
            var userEntity = (from user in _HealthIndexDbContext.UserMasters
                              join role in _HealthIndexDbContext.RoleMasters
                              on user.RoleId equals role.RoleId
                              where user.UserId == userExist.UserId
                              select new
                              {
                                  user.UserId,
                                  user.UserName,
                                  user.FirstName,
                                  user.LastName,
                                  role.RoleId,
                                  role.RoleName,
                              }).FirstOrDefault();

            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
                return null;
            }
            return new AuthModel
            {
               
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,              
                RoleId = userEntity.RoleId,
               
            };
        }

        public string CreateNewOTP(AppUserMaster model,  string otp)
        {
            string message = "";
            var otpEntity = new AppUserMaster();
            if (model.EmailId == null)
            {
                message = "error";
                return message;
            }
            else
            {
                var existingRecord = _HealthIndexDbContext.AppUserMasters.Where(m => m.EmailId ==model.EmailId ).Distinct().FirstOrDefault();
                if (existingRecord != null)
                {
                    existingRecord.OtpforLogin = otp;
                    _HealthIndexDbContext.SaveChanges();
                }
                else
                {
                    otpEntity.OtpforLogin = otp;
                    otpEntity.AppUserId = model.AppUserId;
                    otpEntity.FirstName = model.FirstName;
                    otpEntity.LastName = model.LastName;
                    otpEntity.EmailId = model.EmailId;
                    otpEntity.MobileNo = model.MobileNo;
                    otpEntity.AppPassword = model.AppPassword;
                    otpEntity.PasswordRenewDate = model.PasswordRenewDate;
                    otpEntity.OldPassword = model.OldPassword;
                    otpEntity.UserPhoto = model.UserPhoto;
                    otpEntity.IsActive = false;
                    otpEntity.DeleteStatus = false;
                    otpEntity.IsCurrentlyLoggedIn = model.IsCurrentlyLoggedIn;
                    otpEntity.RoleId = model.RoleId;
                    otpEntity.Guid = model.Guid;
                    otpEntity.PlanId = model.PlanId;
                    otpEntity.CountryCode = model.CountryCode;
                    otpEntity.IsSelfAssessment = false;
                    otpEntity.CreatedDate = DateTime.Now;
                    otpEntity.ExpiryDate = otpEntity.CreatedDate?.AddDays(14);
                    _HealthIndexDbContext.AppUserMasters.Add(otpEntity);
                   
                }
                message = "record added.";
                return message;
            }
        }
    }
    }
