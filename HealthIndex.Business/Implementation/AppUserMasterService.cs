using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace HealthIndex.Business.Implementation
{
    public class AppUserMasterService : IAppUserMasterService
    {
        HealthIndexDbContext _healthindexdbcontext;
        EmailService emailSenderService = new EmailService();
        private readonly IEmailSenderService _emailSender;
        private ConfigurationsModel _url;
        private ITwilioSmsService _twilioSmsService;
        private readonly IAuthService _authService;
        public AppUserMasterService(HealthIndexDbContext healthindexdbcontext,
            IEmailSenderService emailSender, IOptions<ConfigurationsModel> hostName, ITwilioSmsService twilioSmsService, IAuthService authService)
        {
            _healthindexdbcontext = healthindexdbcontext;
            
            _emailSender = emailSender;
            this._url = hostName.Value;
            _twilioSmsService = twilioSmsService;
            _authService = authService;

        }
        public AuthModel AuthenticateUserForMobile(string email, string password, ref ErrorResponseModel errorResponseModel)
        {

            AuthModel authModel  = new AuthModel();
            try
            {
                errorResponseModel = new ErrorResponseModel();
                try
                {
                    var userEntity = (from user in _healthindexdbcontext.AppUserMasters
                                           join role in _healthindexdbcontext.RoleMasters on user.RoleId equals role.RoleId
                                        //   join plan in _healthindexdbcontext.SubscriptionPlans on user.PlanId equals plan.PlanId
                                           where user.EmailId == email &&
                                           EF.Functions.Collate(user.AppPassword, "SQL_Latin1_General_CP1_CS_AS") == password // Case-sensitive comparison
                                           select new
                                           {
                                               user.AppUserId,
                                               user.FirstName,
                                               user.LastName,
                                               user.EmailId,
                                               user.RoleId,
                                               role.RoleName,
                                               user.MobileNo,
                                               user.Guid,
                                               user.UserPhoto,
                                               user.ExpiryDate,
                                               user.PlanId,
                                               user.OrderId,
                                               user.IsActive,
                                            //   PlanName = plan.Name ?? "Default Plan Name", // Use default value if plan is null
                                              // Validity = plan.Validity ?? 0 // Use default value if Validity is null
                                           }).FirstOrDefault();
                   // var plantest = null;
                    var plantest = (SubscriptionPlan)null;

                    if (userEntity.PlanId != null)
                    {
                         plantest = _healthindexdbcontext.SubscriptionPlans
                               .FirstOrDefault(plan => plan.PlanId == userEntity.PlanId);
                       
                    }

                    string OrderStatus = "";
                    long? orderno = 0;

                    if(userEntity.OrderId != null )
                    {
                        var GetOrderStatus = (from status_ in _healthindexdbcontext.SubscriptionOrders
                                              where status_.AppUserId == userEntity.AppUserId && status_.OrderNo == userEntity.OrderId
                                              select new
                                              {
                                                  status_.OrderStatus,
                                              }).FirstOrDefault();
                        OrderStatus = GetOrderStatus.OrderStatus;
                        orderno = userEntity.OrderId;
                    }
                    _healthindexdbcontext.SubscriptionOrders
                              .FirstOrDefault(plan => plan.PlanId == userEntity.PlanId);

                    if (userEntity == null)
                    {
                        errorResponseModel.StatusCode = HttpStatusCode.OK;
                        errorResponseModel.Message = "User not found. Please enter valid credentials";
                        return null;
                    }

                    if(userEntity.PlanId != null)
                    {
                        authModel = new AuthModel
                        {

                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            EmailId = userEntity.EmailId,
                            AppUserId = userEntity.AppUserId,
                            RoleId = userEntity.RoleId,
                            RoleName = userEntity.RoleName,
                            MobileNo = userEntity.MobileNo,
                            IsActive = userEntity.IsActive,
                            Guid = userEntity.Guid,
                            UserPhoto = userEntity.UserPhoto,
                            ExpiryDate = Convert.ToDateTime(userEntity.ExpiryDate).ToString("dd-MM-yyyy"),
                            Name = plantest != null ? plantest.Name : "",
                            Validity = plantest != null ? plantest.Validity : 0,
                            OrderNo = orderno,
                            OrderStatus = OrderStatus,

                            //letest order no 

                            //OrderStatus



                        };
                    }

                    else
                    {
                        authModel = new AuthModel
                        {
                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            EmailId = userEntity.EmailId,
                            AppUserId = userEntity.AppUserId,
                            RoleId = userEntity.RoleId,
                            RoleName = userEntity.RoleName,
                            IsActive = userEntity.IsActive,
                            MobileNo = userEntity.MobileNo,
                            Guid = userEntity.Guid,
                            UserPhoto = userEntity.UserPhoto,
                            ExpiryDate = Convert.ToDateTime(userEntity.ExpiryDate).ToString("dd-MM-yyyy"),
                        };
                    }

                    return authModel;

                }
                catch (Exception ex)
                {
                    // Handle exceptions (log or rethrow, depending on your application's needs)
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    errorResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponseModel.Message = "An error occurred while processing the request.";
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }          
        }

        public MessageModel ForgotPasswordLinkForMobile(string email, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel messageModel = new MessageModel();
           
            var userEntity = _healthindexdbcontext.AppUserMasters.FirstOrDefault(x => x.EmailId == email);
            if (userEntity == null)
            {               
               messageModel. message = GlobalConstants.EmailNotFound;                
            }
            else
            {
                try
                {
                    StringBuilder strBody = new StringBuilder();                  
                    strBody.Append("<body>");
                    strBody.Append("Hello  " + userEntity.FirstName);
                    strBody.Append("<P>Your password for Health Index portal is - </P>");
                    strBody.Append("</body>" + userEntity.AppPassword);
                    var emailModel = new EmailModel();
                    emailModel.ToAddress = email;
                    emailModel.Body = strBody.ToString();
                    emailModel.isHtml = true;
                    emailModel.Subject = GlobalConstants.ForgotPassword;
                    if (!string.IsNullOrEmpty(emailModel.ToAddress))
                    { 
                        _emailSender.Execute(emailModel.ToAddress, emailModel.Subject, emailModel.Body);                       
                    }
                   messageModel.message = GlobalConstants.ForgotPasswordMessage;
                }
                catch (Exception ex)
                {

                }
            }
            return messageModel;
        }
    
        public MessageModel AddAppUser(AppUserMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
           MessageModel messageModel = new MessageModel();               
          
            var existingUser = new AppUserMaster();
             existingUser = _healthindexdbcontext.AppUserMasters
                             .FirstOrDefault(x => x.AppUserId == model.AppUserId);  
            // Now, userExists will be a boolean indicating whether a user with the specified criteria exists.

            if (existingUser != null)
            {
                existingUser.FirstName =  model.FirstName;
                existingUser.LastName = model.LastName;
                existingUser.MobileNo = model.MobileNo;
                if(model.EmailId != null)
                {
                    existingUser.EmailId = model.EmailId;
                }
               
                _healthindexdbcontext.SaveChanges();

                messageModel.message = GlobalConstants.AppUserUpdateSuccessfully;

                //update
            }
            else
            {
                var GetPlan = _healthindexdbcontext.SubscriptionPlans
                           .FirstOrDefault(x => x.Name == "SignUP Plan");

                //string userPhotoPath = UploadPhoto(model);
                var appEntity = new AppUserMaster();
                appEntity.FirstName = model.FirstName;
                appEntity.LastName = model.LastName;
                appEntity.EmailId = model.EmailId;
                appEntity.MobileNo = model.MobileNo;
                appEntity.AppPassword = model.AppPassword;
                appEntity.PasswordRenewDate = model.PasswordRenewDate;
                appEntity.OldPassword = model.OldPassword;
                appEntity.UserPhoto = model.UserPhotoPath;
                appEntity.IsActive = false;
                appEntity.DeleteStatus = false;
                appEntity.IsCurrentlyLoggedIn = model.IsCurrentlyLoggedIn;
                appEntity.RoleId = model.RoleId;
                appEntity.Guid = model.Guid;
                if(GetPlan != null)
                {
                    appEntity.PlanId = GetPlan.PlanId;
                }
                appEntity.CountryCode = model.CountryCode;
                appEntity.IsSelfAssessment = false;
                appEntity.CreatedDate = DateTime.Now;
                appEntity.ExpiryDate =appEntity.CreatedDate?.AddDays(14);

              //  _healthindexdbcontext.AppUserMasters.Add(appEntity);
                Random generator = new Random();
                //  string userName = appEntity.FirstName + " " + appEntity.LastName;
                StringBuilder strBody = new StringBuilder();
                // var siteUrl = _url.SiteUrl;
                strBody.Append("<body>");
                strBody.Append("<P>Your OTP for Login is - </P>");                            
                string otpCode = generator.Next(0, 999999).ToString("D6");
                string otp = _authService.CreateNewOTP((appEntity), otpCode);
                strBody.Append("</body>" + otpCode);
                var emailModel = new EmailModel();
                emailModel.ToAddress = appEntity.EmailId;
                emailModel.Body = strBody.ToString();
                emailModel.isHtml = true;
                emailModel.Subject = GlobalConstants.OTP;
                long AppUserSId = 0;

                SubscriptionOrder subscriptionOrder = new SubscriptionOrder
                {
                    AppUserId = AppUserSId,
                    OrderDate = DateTime.Now,
                    PlanId = GetPlan.PlanId,
                    ValidityDays = GetPlan.Validity,
                    Amount = Convert.ToDecimal(GetPlan.Amount),
                };

                _healthindexdbcontext.SubscriptionOrders.Add(subscriptionOrder);
                _healthindexdbcontext.SaveChanges();

                if (!string.IsNullOrEmpty(emailModel.ToAddress))
                {   
                    _emailSender.Execute(emailModel.ToAddress, emailModel.Subject, emailModel.Body);                   
                   // _healthindexdbcontext.SaveChanges();

                    var newuser = _healthindexdbcontext.AppUserMasters.Where(x => x.EmailId == model.EmailId).FirstOrDefault();

                    messageModel.message = GlobalConstants.AppUserAddSuccessfully;
                    messageModel.OtpforLogin = otpCode;
                    messageModel.AppUserId = newuser.AppUserId;
                     
                    messageModel.ExpiryDate = appEntity.ExpiryDate;
                    AppUserSId = newuser.AppUserId;
                }
            }
            return messageModel;
        }

        public MessageModel UpdateAppUser(AppUserMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel messageModel = new MessageModel();

            var existingUser = new AppUserMaster();

            existingUser = _healthindexdbcontext.AppUserMasters
                            .FirstOrDefault(x => x.AppUserId == model.AppUserId);
          
                existingUser.FirstName = model.FirstName;
                existingUser.LastName = model.LastName;
                existingUser.MobileNo = model.MobileNo;
               
                _healthindexdbcontext.SaveChanges();

                messageModel.message = GlobalConstants.AppUserUpdateSuccessfully;

            
            return messageModel;
        }

        public MessageModel EditSelfAssessment(QuestionModel model, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel objemessage = new MessageModel();//string.Empty;
            var appEntity = _healthindexdbcontext.AppUserMasters.FirstOrDefault(x => x.AppUserId == model.AppUserId);           
            if (appEntity == null)
            {
                objemessage.message = GlobalConstants.ExistingUserMessage;
            }
            else
            {
                appEntity.IsSelfAssessment = true;
                _healthindexdbcontext.SaveChanges();
                objemessage.message = GlobalConstants.AppUserUpdateSuccessfully;
            }
            return objemessage;
        }

        public List<FeedbackListModel1> GetFeedbackDetailsForMobile(long AppUserId, ref ErrorResponseModel errorResponseModel)
        {
            var feedbackdetailList = new List<FeedbackListModel1>();
            errorResponseModel = new ErrorResponseModel();
            var feedbackEntity = (from m in _healthindexdbcontext.FeedbackDetails
                                  join app in _healthindexdbcontext.AppUserMasters on m.UserId equals app.AppUserId                          
                                  where m.UserId == AppUserId
                                  select new
                                        {
                                             m.FeedbackId,
                                             m.UserId,
                                             m.Rating,
                                             m.Comments,
                                             m.Feedbackdate,
                                             app.AppUserId,
                                             app.FirstName,
                                             app.LastName,
                                             app.EmailId
                                          }).ToList();
            if (feedbackEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = " Feedback Details for mobile not found";
                return null;
            }

            feedbackEntity.ForEach(item =>
            {
                feedbackdetailList.Add(new FeedbackListModel1
                {
                    FeedbackId = item.FeedbackId,
                    UserId = item.UserId,
                    Rating = item.Rating,
                    Comments = item.Comments,
                    Feedbackdate = item.Feedbackdate.Value.ToString("dd-MM-yyyy HH:mm"),
                    AppUserId = item.AppUserId,
                    FirstName = item.FirstName,
                    LastName= item.LastName,
                    EmailId= item.EmailId,
                });
            });
            return feedbackdetailList;
        }

        public ActiveUser ActivateUserCount( ref ErrorResponseModel errorResponseModel)
        {
            ActiveUser activeCount = new ActiveUser();
            var count = _healthindexdbcontext.AppUserMasters.Where(x=>x.IsActive==true).ToList().Count();
            activeCount.ActiveUserCount = count;
            return activeCount;
            
        }

        public InActiveUser InActiveUserCount( ref ErrorResponseModel errorResponseModel)
        {
            InActiveUser inactiveCount = new InActiveUser();
            var count = _healthindexdbcontext.AppUserMasters.Where(x => x.IsActive == false).ToList().Count();
            inactiveCount.InActiveUserCount = count;
            return inactiveCount;
        }

        public MessageModel EditAppUser(AppUser model, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel objemessage = new MessageModel();

            var appuserEntity = _healthindexdbcontext.AppUserMasters.Where(x => x.AppUserId == model.AppUserId && x.OtpforLogin==model.OtpforLogin).FirstOrDefault();
            if (appuserEntity == null)
            {
                objemessage.message = "AppUser Already Exists..";
            }
            else
            {
                appuserEntity.IsActive = true ;
                _healthindexdbcontext.Update(appuserEntity);
                _healthindexdbcontext.SaveChanges();
                objemessage.AppUserId = model.AppUserId;
                objemessage.OtpforLogin = model.OtpforLogin;
                objemessage.message = "AppUser Active Successfully";
            }
            return objemessage;
        }

        public MessageModel DeleteAppUser(long appuserId, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel objmessage=new MessageModel();
            var appuserEntity = _healthindexdbcontext.AppUserMasters.FirstOrDefault(x => x.AppUserId==appuserId);

            if (appuserEntity != null)
            {
                appuserEntity.DeleteStatus = true;
                _healthindexdbcontext.SaveChanges();
               objmessage. message = "AppUser Deleted Successfully";
            }
            return objmessage;
        }

        public ResponseMessage UploadPhoto([FromForm]UploadPhoto upload)
        {
            ResponseMessage messagemodel = new ResponseMessage();
            if (upload.UserPhoto == null)
            {
                messagemodel.message= "The uploaded file is empty";
                return messagemodel;
            }
            else
            {
                string filePathtoSave = "";
                var existingUser = _healthindexdbcontext.AppUserMasters.Where(x => x.AppUserId == upload.AppUserId).FirstOrDefault();
                if (existingUser != null)
                {
                    var serverPath = AppDomain.CurrentDomain.BaseDirectory + "/Resource/UserImage/";
                    string extn = System.IO.Path.GetExtension(upload.UserPhoto.FileName);
                    var filePath = Path.Combine(serverPath + upload.AppUserId + extn);
                    new FileInfo(filePath).Directory?.Create();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        upload.UserPhoto.CopyToAsync(stream);
                    }
                     filePathtoSave = "/Resource/UserImage/" + upload.AppUserId + extn;

                    existingUser.UserPhoto = filePathtoSave;
                    messagemodel.message = "Profile Image Uploaded Successfully";
                    _healthindexdbcontext.SaveChanges();
                }
                return messagemodel;
            }

        }

        public ResponseMessage ChangePasswordForMobile(ChangePassword password, ref ErrorResponseModel errorResponseModel)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            string message = "";
            var userEntity = _healthindexdbcontext.AppUserMasters.Where(x => x.AppUserId == password.AppUserId 
            && EF.Functions.Collate(x.AppPassword, "SQL_Latin1_General_CP1_CS_AS") == password.currentPassword).FirstOrDefault();



            if (userEntity != null)
            {
                userEntity.AppPassword = password.newPassword;
                userEntity.OldPassword = password.currentPassword;
                _healthindexdbcontext.AppUserMasters.Update(userEntity);
                _healthindexdbcontext.SaveChanges();
                responseMessage.message = "Password Change Successfully";
            }
            else
            {
                responseMessage.message = "Please Check Current Password";
            }
            return responseMessage;
        }
    }
}

