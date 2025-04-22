using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropInsectImageModel
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

    }
}
