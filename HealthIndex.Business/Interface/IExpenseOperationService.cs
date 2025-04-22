using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IExpenseOperationService
    {

        object Value { get; }
        List<ExpenseOperationModel> GetAll();
        string Add(ExpenseOperationModel model, ref ErrorResponseModel errorResponseModel);
        ExpenseOperationModel GetById(long ExpenseOperationId, ref ErrorResponseModel errorResponseModel);
        public bool Put(ExpenseOperationModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long ExpenseOperationId, ref ErrorResponseModel errorResponseModel);
        List<ExpenseOperationsModel> GetAllOperation();


    }
}
