using System;
using System.Collections.Generic;

#nullable disable

namespace StartUpX.Entity.DataModels
{
    public partial class ExpenseHeadTypeMaster
    {
        public ExpenseHeadTypeMaster()
        {
            ExpenseOperations = new HashSet<ExpenseOperation>();
        }

        public int ExpenseHeadTypeId { get; set; }
        public string ExpenseHeadType { get; set; }
        public string Description { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<ExpenseOperation> ExpenseOperations { get; set; }
    }
}
