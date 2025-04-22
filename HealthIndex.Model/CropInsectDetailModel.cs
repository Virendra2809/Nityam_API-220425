using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
      public class CropInsectDetailModel
    {
        public int CropInsectDetailId { get; set; }
        public int? CropId { get; set; }
        public int[] CropInsectId { get; set; }
        public int? CropInsectId1 { get; set; }
        public string CropName { get; set; }
        public string CropInsectname { get; set; }
    }




    public class CropInsectDetailsModel
    {
        public CropInsectDetailsModel()
        {
            this.InsectImage = new List<CropInsectImagesModel>();
            this.InsectSysmptoms = new List<CropInsectSymptomModel>();
            this.InsectManagement = new List<CropInsectManagementModel>();


        }

        public int CropInsectDetailId { get; set; } 
        public int? CropInsectId { get; set; }
        public string CropInsectname { get; set; }
        public string Description { get; set; }
        public string?TechnicalName { get; set; }
        public string Symptoms { get; set; }
        public string? Identification { get; set; }
        public List<CropInsectImagesModel> InsectImage { get; set; }
        public List<CropInsectSymptomModel> InsectSysmptoms { get; set; }
        public List<CropInsectManagementModel> InsectManagement { get; set; }

    }
    public class CropInsectImagesModel
    {
        public int InsectImageId { get; set; }
        public int? CropInsectId { get; set; }
        public string ImageName { get; set; }
        public string InsectImage1 { get; set; }
    }

}
