using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IAppUserMasterService
    {
        AuthModel AuthenticateUserForMobile(string email, string password, ref ErrorResponseModel errorResponseModel);
        public MessageModel ForgotPasswordLinkForMobile(string email, ref ErrorResponseModel errorResponseModel);
        public MessageModel AddAppUser(AppUserMasterModel model , ref ErrorResponseModel errorResponseModel);

        public MessageModel UpdateAppUser(AppUserMasterModel model, ref ErrorResponseModel errorResponseModel);

        
        public MessageModel EditSelfAssessment(QuestionModel model, ref ErrorResponseModel errorResponseModel);
        List<FeedbackListModel1> GetFeedbackDetailsForMobile(long AppUserId, ref ErrorResponseModel errorResponseModel);
       ActiveUser  ActivateUserCount(ref ErrorResponseModel errorResponseModel);
       InActiveUser InActiveUserCount(ref ErrorResponseModel errorResponseModel);
        public MessageModel EditAppUser(AppUser model, ref ErrorResponseModel errorResponseModel);
        public MessageModel DeleteAppUser(long appuserId, ref ErrorResponseModel errorResponseModel);

         public ResponseMessage UploadPhoto(UploadPhoto upload);
        public ResponseMessage ChangePasswordForMobile(ChangePassword password,ref ErrorResponseModel errorResponseModel);
       
    }
}
