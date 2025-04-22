using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class QuestionMaster
    {
        public QuestionMaster()
        {
            QuestionSubscriptions = new HashSet<QuestionSubscription>();
            Questions = new HashSet<Question>();
        }

        public int QuestionMasterId { get; set; }
        public int? ConsultantId { get; set; }
        public int? QuestionnaireCategoryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ConsultantDetail Consultant { get; set; }
        public virtual ICollection<QuestionSubscription> QuestionSubscriptions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
