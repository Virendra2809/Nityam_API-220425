using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FertCalculationFormulaModel
    {
        public int FormulaId { get; set; }
        public int CropId { get; set; }
        public int SoilTypeId { get; set; }
        public decimal ConstantForN1 { get; set; }
        public decimal ConstantForN2 { get; set; }
        public decimal PercentageOfNinUrea { get; set; }
        public decimal ConstantForP1 { get; set; }
        public decimal ConstantForP2 { get; set; }
        public decimal PercentageOfPinUrea { get; set; }
        public decimal ConstantForK1 { get; set; }
        public decimal ConstantForK2 { get; set; }
        public decimal PercentageOfKinUrea { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }
        public string CropName { get; set; }
        public string SoilName { get; set; }


    }
}
