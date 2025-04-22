using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class NutrientDeficencySymptomModel
    {
        public int NutrientDeficencySymptomId { get; set; }
        public int? NutrientDeficencyId { get; set; }
        public string Symptom { get; set; }
    }
}
