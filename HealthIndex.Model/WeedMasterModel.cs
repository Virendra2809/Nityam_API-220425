using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public  class WeedMasterModel
    {
        public WeedMasterModel()
        {
            this.Weeddetail = new List<WeedDetailModel>();
            this.WeedMasterImages = new List<WeedImageModel>();

        }


        

        public int WeedId { get; set; }
        public string WeedName { get; set; }
        public string TechnicalName { get; set; }
        public string Identification { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }

        public List<WeedDetailModel> Weeddetail { get; set; }
        public List<WeedImageModel> WeedMasterImages { get; set; }
    }
}
