using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class AgriProductMaster
    {
        public AgriProductMaster()
        {
            AgriProductPriceDetails = new HashSet<AgriProductPriceDetail>();
        }

        public int AgriProductId { get; set; }
        public int? AgriProductTypeId { get; set; }
        public string AgriProductName { get; set; }
        public string AgriProductCode { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<AgriProductPriceDetail> AgriProductPriceDetails { get; set; }
    }
}
