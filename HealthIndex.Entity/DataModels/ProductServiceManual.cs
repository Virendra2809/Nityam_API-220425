using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductServiceManual
    {
        public int ProductServiceManualId { get; set; }
        public int? ProductId { get; set; }
        public string ServiceManualTitle { get; set; }
        public string ServiceManualDesc { get; set; }
        public string ServiceManual { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Product Product { get; set; }
    }
}
