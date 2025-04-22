using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IExpenseHeadTypeService
    {
        List<ExpenseHeadTypeModel> GetAllExpenseHeadType();
        ExpenseHeadTypeModel GetExpenseHeadTypeById(long ExpenseHeadTypeId, ref ErrorResponseModel errorResponseModel);
        string AddExpenseHeadType(ExpenseHeadTypeModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateExpenseHeadType(ExpenseHeadTypeModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteExpenseHeadType(long ExpenseHeadTypeId, ref ErrorResponseModel errorResponseModel);
    }
}
