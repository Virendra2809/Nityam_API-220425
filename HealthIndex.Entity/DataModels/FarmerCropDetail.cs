using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class FarmerCropDetail
    {
        public FarmerCropDetail()
        {
            SoilTestReports = new HashSet<SoilTestReport>();
        }

        public int FarmerCropDetailId { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? SowingDate { get; set; }
        public decimal? SowingAreaInHect { get; set; }
        public decimal? SowingAreaInAcre { get; set; }
        public decimal? SowingAreaInAres { get; set; }
        public int? SoilTypeId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? FarmerSelectedCropId { get; set; }
        public string FarmName { get; set; }

        public virtual SoilTypeMaster SoilType { get; set; }
        public virtual ICollection<SoilTestReport> SoilTestReports { get; set; }
    }
}
