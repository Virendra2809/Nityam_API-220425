using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ExpensesDetail
    {
        public int ExpenseDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public int? ExpenseStageId { get; set; }
        public int? ExpenseOperationId { get; set; }
        public int? CropId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal? OperationAmount { get; set; }
        public string NoofWorkers { get; set; }
        public decimal? WorkersAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Rate { get; set; }
        public string EntryType { get; set; }

        public virtual CropMaster Crop { get; set; }
        public virtual ExpenseOperation ExpenseOperation { get; set; }
        public virtual ExpenseStageMaster ExpenseStage { get; set; }
        public virtual FarmerMaster Farmer { get; set; }
    }
}
