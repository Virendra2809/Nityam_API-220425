using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public partial  class CropInsectMasterModel
    {

        public CropInsectMasterModel()
        {
            this.InsectImage = new List<CropInsectImageModel>();
            this.InsectSysmptoms = new List<CropInsectSymptomModel>();
            this.InsectManagement = new List<CropInsectManagementModel>();

        }



        public int CropInsectId { get; set; }
        public string CropInsectName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string TechnicalName { get; set; }
        public string Symptoms { get; set; }
        public string Identification { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }

        public List<CropInsectImageModel> InsectImage { get; set; }
        public List<CropInsectSymptomModel> InsectSysmptoms { get; set; }
        public List<CropInsectManagementModel> InsectManagement { get; set; }


    }



    public class CropInsectSymptomModel
    {
         public int CropInsectSymptomId { get; set; }
        public int? CropInsectId { get; set; }
        public string Symptom { get; set; }
    }
}
