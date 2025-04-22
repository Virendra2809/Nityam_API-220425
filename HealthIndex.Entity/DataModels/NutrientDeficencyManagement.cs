using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class NutrientDeficencyManagement
    {
        public int NutrientDeficencyManagementId { get; set; }
        public int? NutrientDeficencyId { get; set; }
        public string NutrientManagement { get; set; }

        public virtual NutrientDeficencyDetail NutrientDeficency { get; set; }
    }
}
