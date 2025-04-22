using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientDeficencyDetail
    {
        public NutrientDeficencyDetail()
        {
            NutrientDeficencyImages = new HashSet<NutrientDeficencyImage>();
            NutrientDeficencyManagements = new HashSet<NutrientDeficencyManagement>();
            NutrientDeficencySymptoms = new HashSet<NutrientDeficencySymptom>();
        }

        public int NutrientDeficencyId { get; set; }
        public int NutrientId { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CropId { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual NutrientMaster Nutrient { get; set; }
        public virtual ICollection<NutrientDeficencyImage> NutrientDeficencyImages { get; set; }
        public virtual ICollection<NutrientDeficencyManagement> NutrientDeficencyManagements { get; set; }
        public virtual ICollection<NutrientDeficencySymptom> NutrientDeficencySymptoms { get; set; }
    }
}
