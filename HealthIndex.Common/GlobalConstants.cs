using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Common
{
    public static class GlobalConstants
    {
        public const string AuthKey = "THIS IS KEY 12345";
        public const string NotFoundMessage = "Not Found. ";
        public const string UserNotFoundMessage = "User not  found. Please enter valid credentials.";
        public const string InvalidRequest = "Invalid request, please verify details.";
        public const string Status500Message = "Something went wrong!";
        public const string Status503Message = "Service  not available for this user.";
        public const string ExistingUserMessage = "User already exists.";
        public const string AccountVerifySubject = "Akshaya Agri Account Verification";
        public const string ActivationLinkMessage = "Activation link is sent to your email address.Please check your inbox to activate account.";
        public const string APIInfoTitle = "Agtonomics AgriCulture API";
        public const string ExistingName = "Name already exists.";
        public const string ExistingConsultant = "ExistingConsultant"; 
        public const string ExistingSequenceNumber = "Sequence number already exists.";
        public const string RecordSaveMessage = "Record Save Successfully. ";
        public const string ConsultantSavedSuccessfully = "Consultant Saved Successfully";
        public const string RecordUpdateMessage = "Record Update Successfully. ";
        public const string RecordDeleteMessage = "Record Delete Successfully. ";
        public const string EmailSendMesage = "Email Send successfuly.";
        public const string ForgotPassword = "Health Index Forgot Password Link";
        public const string PasswordChangeMessage = "Changed password sucesfully.";
        public const string EmailNotFound = "Email invalid. Plese check.";
        public const string ForgotPasswordMessage = "We have sent your password on your email id Please check";
        public const string AddIncidentMessage = "Incident saved successfuly.";
        public const string ExistingIncidentMessage = "Incident already exists.";
        public const string UserUpdateSuccessfully = "User Update Successfully";
        public const string ConsultantDetailsUpdateSuccessfully = "ConsultantDetails Update Successfully";
        public const string AppUserAddSuccessfully = "AppUser Add Successfully";
        public const string AppUserUpdateSuccessfully = "AppUser Update Successfully";
        public const string OTP = " Health Index - OTP ";
        public const string QuestionsListnotfound = "Questions List not found";
        public const string QuestionSubscriptionSavedSuccessfully="Question Subscription Saved Successfully";
        public const string ExistingFeedback = "Existing Feedback";
        public const string FeedbackSavedSuccessfully = "Feedback Saved Successfully";
        
        public const string PlanSavedSuccessfully = "Plan Saved Successfully";
        public const string DuplicatePromoCode = "PromoCode Already exists";


        public const string PlanUpdateSuccessfully = "Plan Update Successfully";


        public const string FeedbackreplySavedSuccessfully = "Feedback Reply Saved Successfully";

        public const int Status101Get = 101;
        public const int Status102Insert =102;
        public const int Status103Update = 103;
        public const int Status104Delete = 104;
        public const int Status105AlreadyExist = 105;
        public const int Status106InternalServerError = 106;
        public const int Status107BadRequest = 107;
        public const int Status108Unauthorized = 1108;
        public const int Status109NotFound = 109;
        public const int Status200NotAcceptable = 200;
    }
}
