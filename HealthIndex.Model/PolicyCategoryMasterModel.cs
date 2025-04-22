using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class PolicyCategoryMasterModel
    {
        public int PolicyCategoryId { get; set; }
        public string PolicyCategoryName { get; set; }
        public int? PolicyTypeId { get; set; }
        public string PolicyTypeName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

    }
}
