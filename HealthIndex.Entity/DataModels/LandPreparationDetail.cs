using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class LandPreparationDetail
    {
        public LandPreparationDetail()
        {
            LandPreparationImages = new HashSet<LandPreparationImage>();
        }

        public int LandPreparationId { get; set; }
        public int CropId { get; set; }
        public int SeqNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? SeasonId { get; set; }

        public virtual SeasonMaster Season { get; set; }
        public virtual ICollection<LandPreparationImage> LandPreparationImages { get; set; }
    }
}
