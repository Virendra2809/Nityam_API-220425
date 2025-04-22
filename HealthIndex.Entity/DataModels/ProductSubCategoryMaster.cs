using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductSubCategoryMaster
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ParentSubCategoryId { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? HaschildCategory { get; set; }
    }
}
