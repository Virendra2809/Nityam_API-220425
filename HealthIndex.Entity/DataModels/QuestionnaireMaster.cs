using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class QuestionnaireMaster
    {
        public QuestionnaireMaster()
        {
            Questions = new HashSet<Question>();
        }

        public int QuestionnaireId { get; set; }
        public int RecommendedById { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual RecommendedBy RecommendedBy { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
