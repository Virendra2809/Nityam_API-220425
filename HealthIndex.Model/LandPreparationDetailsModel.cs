using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class LandPreparationDetailsModel
    {
        public LandPreparationDetailsModel()
        {
            this.LandPreparationImages = new List<LandPreparationImageModel>();

        }
        public int LandPreparationId { get; set; }
        public int CropId { get; set; }
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public string CropName { get; set; }
        public int SeqNo { get; set; }
       // public int[] SeqNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LandPreparationImageModel> LandPreparationImages { get; set; }

    }


    public class LandPreparationModel
    {
        public LandPreparationModel()
        {

            this.LandPreparationImages = new List<LandPreparationImageModel>();
            this.LandDetails = new List<LandDetailModel>();
           
        }
        public int LandPreparationId { get; set; }
        public int CropId { get; set; }
        public string CropName { get; set; }
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public List<LandPreparationImageModel> LandPreparationImages { get; set; }
        public List<LandDetailModel> LandDetails { get; set; }
     

    }

    public class LandDetailModel {
        
        public int SeqNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
