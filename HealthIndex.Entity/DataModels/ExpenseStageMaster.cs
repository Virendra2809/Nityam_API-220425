using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ExpenseStageMaster
    {
        public ExpenseStageMaster()
        {
            ExpenseOperations = new HashSet<ExpenseOperation>();
            ExpensesDetails = new HashSet<ExpensesDetail>();
        }

        public int ExpenseStageId { get; set; }
        public string ExpenseStageName { get; set; }
        public int? SeqNo { get; set; }
        public bool? DeleteStatus { get; set; }
        public string StageEntryType { get; set; }

        public virtual ICollection<ExpenseOperation> ExpenseOperations { get; set; }
        public virtual ICollection<ExpensesDetail> ExpensesDetails { get; set; }
    }
}
