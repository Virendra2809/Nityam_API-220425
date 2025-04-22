using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class WeedManagementCategoryModel
    {
        public int WeedMgmtCategoryId { get; set; }
        public string WeedMgmtCategoryName { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

    }
}
