using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class ConsultantDetail
    {
        public ConsultantDetail()
        {
            QuestionMasters = new HashSet<QuestionMaster>();
        }

        public int ConsultantId { get; set; }
        public int? ConsultingCategoryId { get; set; }
        public string ConsultantName { get; set; }
        public string MobileNo { get; set; }
        public bool? DeleteStatus { get; set; }
        public string EmailId { get; set; }
        public int? RegistrationNo { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string Consultantcode { get; set; }

        public virtual ConsultingCategory ConsultingCategory { get; set; }
        public virtual ICollection<QuestionMaster> QuestionMasters { get; set; }
    }
}
