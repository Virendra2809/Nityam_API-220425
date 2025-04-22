using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FarmerMasterModel
    {
        public int FarmerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string? FirmIds { get; set; }
        public int? LanguageId { get; set; }
        public bool DeleteStatus { get; set; }
        public string DeviceToken { get; set; }
        public string FarmerImage { get; set; }
        public IFormFile FarmerImageFile { get; set; }


    }
    public class FarmerAddModel
    {
        public int FarmerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string? FirmIds { get; set; }
        public int? LanguageId { get; set; }
        public bool DeleteStatus { get; set; }
        public string DeviceToken { get; set; }
        public string FarmerImage { get; set; }
       // public IFormFile FarmerImageFile { get; set; }


    }



    public class FarmerDeviceTokenModel
    {
        public int FarmerId { get; set; }
        public string DeviceToken { get; set; }


    }
}
