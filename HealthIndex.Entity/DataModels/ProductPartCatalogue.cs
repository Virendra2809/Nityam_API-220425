using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductPartCatalogue
    {
        public int ProductPartsCatalogueId { get; set; }
        public int? ProductId { get; set; }
        public string CatalogueTitle { get; set; }
        public string CatalogueDesc { get; set; }
        public string PartCatalogue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedOn { get; set; }
        public DateTime? UpdatedBy { get; set; }

        public virtual Product Product { get; set; }
    }
}
