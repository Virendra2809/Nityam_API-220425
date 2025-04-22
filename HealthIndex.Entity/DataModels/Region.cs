using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class Region
    {
        public Region()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int RegionId { get; set; }
        public string Region1 { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
