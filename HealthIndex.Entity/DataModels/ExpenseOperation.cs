using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ExpenseOperation
    {
        public ExpenseOperation()
        {
            ExpensesDetails = new HashSet<ExpensesDetail>();
        }

        public int ExpenseOperationId { get; set; }
        public int? ExpenseStageId { get; set; }
        public int? ExpenseHeadTypeId { get; set; }
        public string ExpenseOperation1 { get; set; }
        public string Description { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ExpenseHeadTypeMaster ExpenseHeadType { get; set; }
        public virtual ExpenseStageMaster ExpenseStage { get; set; }
        public virtual ICollection<ExpensesDetail> ExpensesDetails { get; set; }
    }
}
