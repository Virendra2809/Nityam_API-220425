using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class FirmDetail
    {
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public string FirmRegNumber { get; set; }
        public DateTime FirmRegDate { get; set; }
        public string FirmBranchName { get; set; }
        public string FirmOfficeAddress { get; set; }
        public string FirmLogo { get; set; }
        public string FirmPhoneNumber { get; set; }
        public string FirmFaxNumber { get; set; }
        public string FirmEmailIid { get; set; }
        public string MailPassword { get; set; }
        public bool IsFederation { get; set; }
        public string FirmConnectionPath { get; set; }
        public int? ParentFirmId { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
