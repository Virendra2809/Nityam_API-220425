using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class RecommendedBy
    {
        public RecommendedBy()
        {
            QuestionnaireMasters = new HashSet<QuestionnaireMaster>();
        }

        public int RecommendedById { get; set; }
        public string RecommendedBy1 { get; set; }
        public int? SeqNo { get; set; }

        public virtual ICollection<QuestionnaireMaster> QuestionnaireMasters { get; set; }
    }
}
