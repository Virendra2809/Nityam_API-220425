using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class AgriProductMasterModel
    {
        public int AgriProductId { get; set; }
        public int? AgriProductTypeId { get; set; }
        public string AgriProductTypeName { get; set; }
        public string AgriProductName { get; set; }
        public string AgriProductCode { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        
    }
}
