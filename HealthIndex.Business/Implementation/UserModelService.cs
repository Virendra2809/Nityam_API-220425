using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Implementation
{
    public class UserModelService : IUserModelService
    {
        HealthIndexDbContext _healthindexdbcontext;
        EmailService emailSenderService = new EmailService();
        private readonly IEmailSenderService _emailSender;
        private ConfigurationsModel _url;
        private ITwilioSmsService _twilioSmsService;

        public UserModelService(HealthIndexDbContext healthindexdbcontext,
            IEmailSenderService emailSender, IOptions<ConfigurationsModel> hostName, ITwilioSmsService twilioSmsService)
        {
            _healthindexdbcontext = healthindexdbcontext;
            _emailSender = emailSender;
            this._url = hostName.Value;
            _twilioSmsService = twilioSmsService;

        }
       
        public string ForgotPasswordLink(string email, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            
            var userEntity = _healthindexdbcontext.UserMasters.FirstOrDefault(x => x.EmailId == email);
            if (userEntity == null)
            {               
                message = GlobalConstants.EmailNotFound;               
            }
            else
            {
                try
                {
                    //string subject = "Forgot password link sent on your email. Please check.";

                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("Hello  " + userEntity.FirstName);
                    strBody.Append("<P>Your password for Health Index portal is - </P>");
                    strBody.Append("</body>" + userEntity.UserPassword);
                    var emailModel = new EmailModel();
                    emailModel.ToAddress = email;
                    emailModel.Body = strBody.ToString();
                    emailModel.isHtml = true;
                    emailModel.Subject = GlobalConstants.ForgotPassword;
                    if (!string.IsNullOrEmpty(emailModel.ToAddress))
                    { 
                        _emailSender.Execute(emailModel.ToAddress, emailModel.Subject, emailModel.Body);
                    }

                    message = GlobalConstants.ForgotPasswordMessage;
                }
                catch (Exception ex)
                {

                }

            }
            return message;
        }

        public List<AppUserListModel> GetRegisteredUser()
        {
            var userList = new List<AppUserListModel>();
            var userEntity = (from user in _healthindexdbcontext.AppUserMasters
                              join
                              role in _healthindexdbcontext.RoleMasters
                              on user.RoleId equals role.RoleId
                              where user.DeleteStatus == false
                              select new
                              {
                                  user.AppUserId,
                                  user.FirstName,
                                  user.LastName,
                                  user.EmailId,
                                  user.DeleteStatus,
                                  role.RoleName,
                                  user.IsActive
                              }).ToList();
            if (userEntity == null)
            {
                return null;
            }
            foreach (var item in userEntity)
            {
                var model = new AppUserListModel();
                model.AppUserId = item.AppUserId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.EmailId = item.EmailId;
                model.DeleteStatus = (bool)item.DeleteStatus;
                model.RoleName = item.RoleName;
                model.IsActive = item.IsActive;
                userList.Add(model);
            }
            return userList;
        }

        public AdminUserModel GetAdminUserDetailsById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userEntity = _healthindexdbcontext.UserMasters.FirstOrDefault(x => x.UserId == userId && !x.DeleteStatus);
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not found";
                return null;
            }
            return new AdminUserModel
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                EmailId = userEntity.EmailId,
                UserPassword = userEntity.UserPassword,
                MobileNo = userEntity.MobileNo
            };
        }

        public string EditAdminUsers(AdminUserModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var userEntity = _healthindexdbcontext.UserMasters.Where(x => x.UserId == model.UserId && x.DeleteStatus==false).FirstOrDefault();
            if (userEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
                message = "User Already Exists..";
               
            }
            else
            {
                
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.MobileNo = model.MobileNo;
                userEntity.EmailId = model.EmailId;
                userEntity.UserPassword = model.UserPassword;
                userEntity.DeleteStatus=false;
               _healthindexdbcontext.SaveChanges();
                _healthindexdbcontext.Update(userEntity);
                message = GlobalConstants.UserUpdateSuccessfully;
            }
            return message;
            
        }

        public UserCountModel GetCount(ref ErrorResponseModel errorResponseModel)
        {          
            UserCountModel objUserCount = new UserCountModel();
            var count = _healthindexdbcontext.AppUserMasters.ToList().Count();
            objUserCount.UserCount = count;
            return objUserCount;
        }


    }
}
