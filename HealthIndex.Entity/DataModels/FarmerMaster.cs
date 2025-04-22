using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class FarmerMaster
    {
        public FarmerMaster()
        {
            ExpensesDetails = new HashSet<ExpensesDetail>();
            FarmerSelectedCrops = new HashSet<FarmerSelectedCrop>();
            PostCommentDetails = new HashSet<PostCommentDetail>();
        }

        public int FarmerId { get; set; }
        public string FirmIds { get; set; }
        public int? SeedCropId { get; set; }
        public int? LanguageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public bool DeleteStatus { get; set; }
        public string DeviceToken { get; set; }
        public string FarmerImage { get; set; }

        public virtual LanguageMaster Language { get; set; }
        public virtual SeedCropMaster SeedCrop { get; set; }
        public virtual ICollection<ExpensesDetail> ExpensesDetails { get; set; }
        public virtual ICollection<FarmerSelectedCrop> FarmerSelectedCrops { get; set; }
        public virtual ICollection<PostCommentDetail> PostCommentDetails { get; set; }
    }
}
