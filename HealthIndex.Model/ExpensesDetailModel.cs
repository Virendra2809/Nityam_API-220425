using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class ExpensesDetailModel
    {
        
        public int ExpenseDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public string FarmerName { get; set; }
        public int? ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public int? ExpenseOperationId { get; set; }
        public string ExpenseOperation1 { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal? OperationAmount { get; set; }
        public string NoofWorkers { get; set; }
        public decimal? WorkersAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Rate { get; set; }
        public string EntryType { get; set; }



    }

    public class ExpensesDetailAddModel
    {

        public int ExpenseDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public int? ExpenseStageId { get; set; }
        public int? ExpenseOperationId { get; set; }
        public int? CropId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal? OperationAmount { get; set; }
        public string NoofWorkers { get; set; }
        public decimal WorkersAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Rate { get; set; }
        public string ExpenseOperation1 { get; set; }


    }
}
