using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class NutrientDetailModel
    {
        public int NutrientDetailId { get; set; }
        public int CropId { get; set; }
        public string CropName { get; set; }
        public int? SeasonId { get; set; }
        public string SeasonName { get; set; }
        public int? SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public int? NutrientId { get; set; }
        public string NutrientsName { get; set; }
        public decimal? QuantityInKg { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? Isactive { get; set; }
        public bool? DeleteStatus { get; set; }


    }
}
