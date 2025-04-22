using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ProductTraining
    {
        public int ProductTrainingId { get; set; }
        public int? ProductId { get; set; }
        public string TrainingTitle { get; set; }
        public string Type { get; set; }
        public string ProductPpt { get; set; }
        public string ProductTrainingLink { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Product Product { get; set; }
    }
}
