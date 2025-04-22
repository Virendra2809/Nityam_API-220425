using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class ExpenseOperationModel
    {

        public int ExpenseOperationId { get; set; }
        public int? ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public int? ExpenseHeadTypeId { get; set; }
        public string ExpenseHeadType { get; set; }
        public string ExpenseOperation1 { get; set; }
        public string Description { get; set; }
        public bool DeleteStatus { get; set; }


    }

    public class ExpenseOperationAdd
    {
        public int ExpenseOperationId { get; set; }
        public string ExpenseOperation1 { get; set; }

    }

    public class ExpenseOperationsModel
    {
        public int ExpenseOperationId { get; set; }
        public int? ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public string ExpenseOperation1 { get; set; }
        public string Description { get; set; }
    }
}
