using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientDeficencySymptom
    {
        public int NutrientDeficencySymptomId { get; set; }
        public int? NutrientDeficencyId { get; set; }
        public string Symptom { get; set; }

        public virtual NutrientDeficencyDetail NutrientDeficency { get; set; }
    }
}
