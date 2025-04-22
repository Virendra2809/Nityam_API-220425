using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IExpensesDetailService
    {
        List<ExpensesDetailModel> GetAllExpenseDetail();
        ExpensesDetailModel GetExpenseDetailById(long ExpenseDetailsId, ref ErrorResponseModel errorResponseModel);
        string AddExpenseDetail(ExpensesDetailAddModel model, string EntryType, ref ErrorResponseModel errorResponseModel);
        public bool UpdateExpenseDetail(ExpensesDetailAddModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteExpenseDetail(long ExpenseDetailsId, ref ErrorResponseModel errorResponseModel);
        List<ExpensesDetailModel>GetExpenseByIds(long FarmerId,long CropId,long StageId, string EntryType);

    }
}
