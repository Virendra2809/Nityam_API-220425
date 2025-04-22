using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductPhotoVideo
    {
        public int ProductPhotoVideoId { get; set; }
        public int? ProductId { get; set; }
        public string Type { get; set; }
        public string ProductPhotoLink { get; set; }
        public string ProductVideoLink { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Product Product { get; set; }
    }
}
