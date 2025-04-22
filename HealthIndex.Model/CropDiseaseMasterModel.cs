using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropDiseaseMasterModel
    {
        public  CropDiseaseMasterModel()
        {
            this.DiseaseImages= new List<CropDiseaseImageModel>();
            this.DiseaseSysmptoms = new List<CropDiseaseSymptomModel>();
            this.DiseaseManagement = new List<CropDiseaseManagementModel>();

        }

        public int CropDiseaseId { get; set; }
        public string CropDiseaseName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }

        public List<CropDiseaseImageModel> DiseaseImages { get; set; }
        public List<CropDiseaseSymptomModel> DiseaseSysmptoms { get; set; }
        public List<CropDiseaseManagementModel> DiseaseManagement { get; set; }



    }


    public class CropDisease
    {
        public CropDisease()
        {
            this.DiseaseImages = new List<CropDiseaseImageModel>();
        }

        public int CropDiseaseId { get; set; }
        public string CropDiseaseName { get; set; }
        public string Description { get; set; }
        public List<CropDiseaseImageModel> DiseaseImages { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string Symptoms { get; set; }

    }

}
