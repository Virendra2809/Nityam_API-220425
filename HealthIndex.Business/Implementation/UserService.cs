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
    public class UserService : IUserService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        private readonly IEmailService _emailSender;
        IConfiguration _configuration;
        public UserService(AgtonomicsAgriCultureDbContext agriContext, IEmailService emailSender, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public List<UserModel> GetAllUser()
        {
            var errorResponseModel = new ErrorResponseModel();
            var userModelList = new List<UserModel>();
            var userListEntity = (from user in _agriContext.UserMasters
                                  join role in _agriContext.RoleMasters
                                  on user.RoleId equals role.RoleId                                 
                                  select new
                                  {
                                      user.UserId,
                                      user.FirstName,
                                      user.LastName,
                                      user.EmailId,
                                      user.MobileNo,
                                      role.RoleId,
                                      role.RoleName,                                     

                                  }
                                  ).ToList();
            if (userListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in userListEntity)
            {
                var model = new UserModel();
                model.UserId = item.UserId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.Email = item.EmailId;
                model.Phone = item.MobileNo;
                model.RoleName = item.RoleName;              
                userModelList.Add(model);
            }
            return userModelList;

        }
        public string AddUser(UserModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var existingUser = _agriContext.UserMasters.Any(x => x.EmailId == model.Email);
            if (existingUser)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var userEntity = new UserMaster();
                userEntity.UserName = model.Username;
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.UserPassword = model.Password;
                userEntity.MobileNo = model.Phone;
                userEntity.EmailId = model.Email;                
                userEntity.EnteredBy = 1;
                userEntity.RoleId = model.RoleId;
                userEntity.EnteredDate = DateTime.Now;
                _agriContext.UserMasters.Add(userEntity);
                _agriContext.SaveChanges();
                try
                {

                    StringBuilder strBody = new StringBuilder();
                    var siteUrl = _configuration.GetSection("VerifyUserAccountLink");
                    strBody.Append("<body>");
                    strBody.Append("<P>Click below link to verify your Account</P>");
                    strBody.Append("<h2><a href='" + siteUrl + "login?UserId=" + userEntity.UserId + "'>Click here to redirect</a></h2>");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailModel();
                    emailSenderModel.ToAddress = userEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = GlobalConstants.AccountVerifySubject;
                    emailSenderModel.sentStatus = true; ;
                    _emailSender.SendEmailAsync(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                }
                catch (Exception ex)
                {

                }
                _agriContext.SaveChanges();
                message = GlobalConstants.ActivationLinkMessage;
            }
            return message;
        }

        public UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userEntity = _agriContext.UserMasters.FirstOrDefault(x => x.UserId == userId && !x.DeleteStatus);
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new UserModel
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Phone = userEntity.MobileNo,
                Email = userEntity.EmailId,                
                UserId = userEntity.UserId,
                Password = userEntity.UserPassword,
            };

        }
        public bool ActivateUser(UserModel model, ref ErrorResponseModel errorResponseModel)
        {
            var userId = Convert.ToInt32(model.UserId);
            var userEntity = _agriContext.UserMasters.FirstOrDefault(x => x.UserId == userId);
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                userEntity.IsUserActivated = true;
                userEntity.ChangedBy = (int?)model.UserId;
                userEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }
    }
}
