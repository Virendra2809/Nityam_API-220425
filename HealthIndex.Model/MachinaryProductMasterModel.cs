using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public class MachinaryProductMasterModel
    {
        public int MachinaryProductId { get; set; }
        public string MachinaryProductName { get; set; }
        public int? MachinaryBrandId { get; set; }
        public int? MachinaryCategoryId { get; set; }
        public string ModelName { get; set; }
        public int? SequenceNo { get; set; }
        public decimal? ActualPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string MachinaryCategoryName { get; set; }
        public string MachinaryBrandName { get; set; }


    }
}
