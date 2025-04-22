using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CropDiseaseDetailModel
    {
        public int CropDiseaseDetailId { get; set; }
        public int? CropId { get; set; }
        public int[] CropDiseaseId { get; set; }
        public int CropDiseaseId1 { get; set; }
        public string CropDiseaseName { get; set; }
        public string CropName { get; set; }


    }



    public class CropDiseaseDetailsModel
    {
        public CropDiseaseDetailsModel()
        {

            this.DiseaseImages = new List<CropDiseaseImagesModel>();
            this.DiseaseSysmptoms = new List<CropDiseaseSymptomModel>();
            this.DiseaseManagement = new List<CropDiseaseManagementModel>();

        }
        public int CropDiseaseId { get; set; }
        public string CropDiseaseName { get; set; }
        
        public List<CropDiseaseImagesModel> DiseaseImages { get; set; }
        public List<CropDiseaseSymptomModel> DiseaseSysmptoms { get; set; }
        public List<CropDiseaseManagementModel> DiseaseManagement { get; set; }


    }


    public class CropDiseaseImagesModel
    {
        public int CropDiseaseImageId { get; set; }
        public int? CropDiseaseId { get; set; }
        public string ImageName { get; set; }
        public string DiseaseImage { get; set; }
    }

}
