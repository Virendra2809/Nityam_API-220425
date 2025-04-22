using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropMasterModel
    {
      
        public int CropId { get; set; }
        public int? CropTypeId { get; set; }
        public string CropTypeName { get; set; }
        public string CropName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string CropImage { get; set; }
        public int? CropDurMinDays { get; set; }
        public int? CropDurMaxDays { get; set; }

        public IFormFile CropFile { get; set; }
       // public int FarmerId { get; set; }

       }


    public class CropDetailsModel
    {
        public CropDetailsModel()
        {
            // this.CropStages = new List<CropStageDetailModel>();
            this.LandPreparation = new List<CropLandPreparationModel>();
            this.SeedCrop = new List<SeedCropDetailModel>();
            this.Irrigation = new List<CropIrrigationDetailsModel>();
            this.CropInsect = new List<CropInsectDetailsModel>();
            this.CropDisease = new List<CropDiseaseDetailsModel>();
        }

        public int CropId { get; set; }
        public int? CropTypeId { get; set; }
        public string CropTypeName { get; set; }
        public string CropName { get; set; }
        public string CropImage { get; set; }
        public string Description { get; set; }
        // public List<CropStageDetailModel> CropStages { get; set; }
        public List<CropLandPreparationModel> LandPreparation { get; set; }
        public List<SeedCropDetailModel> SeedCrop { get; set; }
        public List<CropIrrigationDetailsModel> Irrigation { get; set; }
        public List<CropDiseaseDetailsModel> CropDisease { get; set; }
        public List<CropInsectDetailsModel> CropInsect { get; set; }

    }




    public class SeedCropDetailModel
    {
        public int SeedCropId { get; set; }
        public string CropName { get; set; }
        public int? RowSpacing { get; set; }
        public int? PlantPopulatin { get; set; }
        public int? PlantSpacing { get; set; }

    }

    public class CropStageDetailModel
    {
        public int CropStageId { get; set; }
        public string CropStageName { get; set; }
        public int? SeqNo { get; set; }

    }


    public class CropIrrigationDetailsModel
    {
        public int? SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public int? CropStageId { get; set; }
        public string CropStageName { get; set; }
        public int IrrigationDay { get; set; }
        public int? SeqNo { get; set; }



    }

    public class CropLandPreparationModel
    {
        public CropLandPreparationModel()
        {
            this.LandPreparationImages = new List<LandPreparationImageModel>();

        }
        public int LandPreparationId { get; set; }
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public int SeqNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LandPreparationImageModel> LandPreparationImages { get; set; }


    }

}
