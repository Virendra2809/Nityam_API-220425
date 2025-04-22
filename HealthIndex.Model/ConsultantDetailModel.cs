using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class ConsultantDetailModel
    {
        public int ConsultantId { get; set; }
        public int? ConsultingCategoryId { get; set; }
        public string ConsultantName { get; set; }
        public string MobileNo { get; set; }
        public bool? DeleteStatus { get; set; }
        public string EmailId { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string ConsultingCategory1 { get; set; }
  
    }
    public class consultingCategory
    {
        public int? ConsultingCategoryId { get; set; }
        public string ConsultingCategory1 { get; set; }
    }

    public class ConsultantDetailExpert
    {
        public int ConsultantId { get; set; }
        public int? ConsultingCategoryId { get; set; }
        public string ConsultantName { get; set; }
        public string MobileNo { get; set; }
        public bool? DeleteStatus { get; set; }
        public string EmailId { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string ConsultingCategory1 { get; set; }
        public int? QuestionMasterId { get; set; }
    }
}
