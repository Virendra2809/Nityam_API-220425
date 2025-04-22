using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class AppUserMaster
    {
        public AppUserMaster()
        {
            AppDataBackups = new HashSet<AppDataBackup>();
            FeedbackDetails = new HashSet<FeedbackDetail>();
            QuestionSubscriptions = new HashSet<QuestionSubscription>();
        }

        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AppPassword { get; set; }
        public DateTime? PasswordRenewDate { get; set; }
        public string OldPassword { get; set; }
        public string UserPhoto { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? IsCurrentlyLoggedIn { get; set; }
        public int? RoleId { get; set; }
        public string Guid { get; set; }
        public string CountryCode { get; set; }
        public string UserAuthToken { get; set; }
        public string FirebaseUserToken { get; set; }
        public string OtpforLogin { get; set; }
        public bool? IsSelfAssessment { get; set; }
        public int? RegistrationNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? PlanId { get; set; }
        public long? OrderId { get; set; }

        public virtual ICollection<AppDataBackup> AppDataBackups { get; set; }
        public virtual ICollection<FeedbackDetail> FeedbackDetails { get; set; }
        public virtual ICollection<QuestionSubscription> QuestionSubscriptions { get; set; }
    }
}
