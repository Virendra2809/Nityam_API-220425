using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class PolicyDetailModel
    {
        public int PolicyId { get; set; }
        public int? PolicyCategoryId { get; set; }
        public string PolicyTitle { get; set; }
        public string Description { get; set; }
        public string PolicyUrl { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PolicyCategoryName { get; set; }
        public int? SeqNo { get; set; }


    }
}
