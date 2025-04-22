using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FertilizerCalculatorModel
    {
        public int CropId { get; set; }
        public string CropName { get; set; }
        public decimal FarmArea { get; set; }
        public decimal Yeild { get; set; }
        public decimal Nitrogen { get; set; }
        public decimal Phosphorous { get; set; }
        public decimal Potassium { get; set; }

     }

    public class FertilizerCalculatorResultModel
    {
        public decimal RecommendationOfUreaForN { get; set; }        
        public decimal RecommendationOfUreaForP { get; set; }
        public decimal RecommendationOfUreaForK { get; set; }
        
    }
}
