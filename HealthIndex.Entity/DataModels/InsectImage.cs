using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class InsectImage
    {
        public int InsectImageId { get; set; }
        public int? CropInsectId { get; set; }
        public string ImageName { get; set; }
        public string InsectImage1 { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? IsActive { get; set; }

        public virtual CropInsectMaster CropInsect { get; set; }
    }
}
