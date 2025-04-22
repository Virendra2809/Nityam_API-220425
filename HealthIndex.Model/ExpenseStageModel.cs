using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class ExpenseStageModel
    {
        public int ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public int? SeqNo { get; set; }
        public bool DeleteStatus { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string StageEntryType { get; set; }


    }


    public class ExpenseStagedetailModel
    {
        public ExpenseStagedetailModel()
        {
            this.Operations = new List<OperationModel>();

        }
        public int ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public string StageEntryType { get; set; }
        public List<OperationModel> Operations { get; set; }

    }

    public class OperationModel
    {
        public int ExpenseOperationId { get; set; }
        public int? ExpenseStageId { get; set; }
        public string ExpenseOperation1 { get; set; }
        public string Description { get; set; }
        
    }

    public class ExpenseStageList
    {
        public ExpenseStageList()
        {
            this.ExpenseStages = new List<ExpenseStageModel>();
            this.IncomeStages= new List<ExpenseStageModel>();

        }
        public List<ExpenseStageModel> ExpenseStages { get; set; }
        public List<ExpenseStageModel> IncomeStages { get; set; }


    }
}
