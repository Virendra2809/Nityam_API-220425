using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class AppUserMasterModel
    {
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AppPassword { get; set; }
        public DateTime? PasswordRenewDate { get; set; }
        public string UserPhoto { get; set; }

        // public IFormFile UserPhoto { get; set; }
        public string OldPassword { get; set; }
        public string UserPhotoPath { get; set; }
        public bool? IsActive { get; set; }=false;
        public bool? DeleteStatus { get; set; }
        public bool? IsCurrentlyLoggedIn { get; set; }
        public int? RoleId { get; set; }
        public string Guid { get; set; }
        public string UserAuthToken { get; set; }
        public string FirebaseUserToken { get; set; }
        public string CountryCode { get; set; }
        public string OtpforLogin { get; set; }
        [DefaultValue(false)]
        public bool? IsSelfAssessment { get; set; }
        public int? RegistrationNo { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class UserCountModel
    {
        public int UserCount { get; set; }
    }

    public class AppUserListModel
    {
        public int AppUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public bool DeleteStatus { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class AppUser
    {
        public int AppUserId { get; set; }

        
       // public bool? IsActive { get; set; }=false;
        public string OtpforLogin { get; set; }


    }


    public class QuestionModel
    {
        public int AppUserId { get; set; }
        [DefaultValue(false)]
        public bool? IsSelfAssessment { get; set; }
    }


    public class ActiveUser
    {
        public int ActiveUserCount { get; set; }

    }

    public class InActiveUser
    {
       
        public int InActiveUserCount { get; set; }

    }


    public class UploadPhoto
    {
        public int AppUserId { get; set; }

        public IFormFile UserPhoto { get; set; }
       
    }

    public class ChangePassword
    {
        public int AppUserId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }



    }
}
