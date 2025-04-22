using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductOprManual
    {
        public int ProductOprManualId { get; set; }
        public int? ProductId { get; set; }
        public string ProductManualTitle { get; set; }
        public string ProductManual { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Product Product { get; set; }
    }
}
