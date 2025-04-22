using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class SeasonMaster
    {
        public SeasonMaster()
        {
            CropNotifications = new HashSet<CropNotification>();
            LandPreparationDetails = new HashSet<LandPreparationDetail>();
            NutrientDetails = new HashSet<NutrientDetail>();
        }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime? SeasonStartMonth { get; set; }
  
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? EndMonth { get; set; }

        public virtual ICollection<CropNotification> CropNotifications { get; set; }
        public virtual ICollection<LandPreparationDetail> LandPreparationDetails { get; set; }
        public virtual ICollection<NutrientDetail> NutrientDetails { get; set; }
    }
}
