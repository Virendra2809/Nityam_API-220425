using System;

namespace HealthIndex.Model
{
    public class AuthModel
    {
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
        public string? Name { get; set; }
        public int? Validity { get; set; }
        public string Guid { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string ExpiryDate { get; set; }
        public long? OrderNo { get; set; }
        public string? OrderStatus { get; set; }
        public int daysRemaining { get; set; }

    }
    public class AuthorModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoto { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string OldPassword { get; set; }
        public DateTime? PasswordRenewDate { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool? IsCurrentlyLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsUserActivated { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public string DeviceUuid { get; set; }
    }
}
