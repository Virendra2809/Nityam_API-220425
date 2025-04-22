using System;
using System.Collections.Generic;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class ConsultingCategory
    {
        public ConsultingCategory()
        {
            ConsultantDetails = new HashSet<ConsultantDetail>();
        }

        public int ConsultingCategoryId { get; set; }
        public string ConsultingCategory1 { get; set; }
        public int? SeqNo { get; set; }

        public virtual ICollection<ConsultantDetail> ConsultantDetails { get; set; }
    }
}
