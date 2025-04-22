using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IExpenseStageService
    {


        List<ExpenseStageModel> GetAllExpenseStage(long FarmerId, long CropId, string Category);
        ExpenseStageModel GetExpenseStageById(long ExpenseStageId, ref ErrorResponseModel errorResponseModel);
        string AddExpenseStage(ExpenseStageModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateExpenseStage(ExpenseStageModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteExpenseStage(long ExpenseStageId, ref ErrorResponseModel errorResponseModel);
        ExpenseStagedetailModel GetById(long ExpenseStageId, ref ErrorResponseModel errorResponseModel);
        List<ExpenseStageList> GetExpenseStage(long FarmerId, long CropId);

    }
}
