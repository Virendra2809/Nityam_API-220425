using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class MandiMaster
    {
        public MandiMaster()
        {
            AgriProductPriceDetails = new HashSet<AgriProductPriceDetail>();
        }

        public int MandiId { get; set; }
        public string MandiName { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public bool? IsWebApi { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual CityMaster City { get; set; }
        public virtual ICollection<AgriProductPriceDetail> AgriProductPriceDetails { get; set; }
    }
}
