using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public  class ExpenseHeadTypeModel
    {
        public int ExpenseHeadTypeId { get; set; }
        public string ExpenseHeadType { get; set; }
        public string Description { get; set; }
        public bool DeleteStatus { get; set; }

    }
}
