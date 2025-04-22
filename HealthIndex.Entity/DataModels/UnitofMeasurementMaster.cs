using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class UnitofMeasurementMaster
    {
        public UnitofMeasurementMaster()
        {
            AgriProductPriceDetails = new HashSet<AgriProductPriceDetail>();
            NutrientDetails = new HashSet<NutrientDetail>();
            Products = new HashSet<Product>();
            SeedProductMasters = new HashSet<SeedProductMaster>();
        }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitAlias { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<AgriProductPriceDetail> AgriProductPriceDetails { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<SeedProductMaster> SeedProductMasters { get; set; }
    }
}
