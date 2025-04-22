using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public  class NutrientDeficencyDetailModel
    {
        public NutrientDeficencyDetailModel()
        {
            this.NutrientImages = new List<NutrientDeficencyImageModel>();
            this.NutrientDeficencySymptom = new List<NutrientDeficencySymptomModel>();
            this.NutrientDeficencyManagement = new List<NutrientDeficencyManagementmodel>();

        }
        public int NutrientDeficencyId { get; set; }
        public int NutrientId { get; set; }
        public string NutrientsName { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }

        public List<NutrientDeficencyImageModel> NutrientImages { get; set; }
        public List<NutrientDeficencySymptomModel> NutrientDeficencySymptom { get; set; }
        public List<NutrientDeficencyManagementmodel> NutrientDeficencyManagement { get; set; }

    }
}
