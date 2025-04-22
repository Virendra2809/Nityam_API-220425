using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class ExpenseStageService:IExpenseStageService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ExpenseStageService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<ExpenseStageModel> GetAllExpenseStage(long FarmerId, long CropId, string Category)
        {
            var errorResponseModel = new ErrorResponseModel();
            var expenseStageModelList = new List<ExpenseStageModel>();
            var expenseStageListEntity = _agriContext.ExpenseStageMasters.Where(x => x.DeleteStatus == false && x.StageEntryType== Category).OrderBy(x=>x.SeqNo).ToList();

            if (expenseStageListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in expenseStageListEntity)
            {
                var model = new ExpenseStageModel();
                var TotalExpense = _agriContext.ExpensesDetails.Where(x => x.CropId == CropId && x.FarmerId == FarmerId && x.ExpenseStageId == item.ExpenseStageId).Sum(x => x.TotalAmount);
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseStageName = item.ExpenseStageName;
                model.SeqNo = item.SeqNo;
                model.DeleteStatus = (bool)item.DeleteStatus;
                model.ExpenseAmount = (decimal)TotalExpense;
                model.StageEntryType = item.StageEntryType;
                expenseStageModelList.Add(model);
            }
            return expenseStageModelList;
        }



        public List<ExpenseStageList> GetExpenseStage(long FarmerId, long CropId)
        {
            var model1 = new ExpenseStageList();
            var errorResponseModel = new ErrorResponseModel();
            var expenseStageModelList = new List<ExpenseStageList>();
            var expenseStageListEntity = _agriContext.ExpenseStageMasters.Where(x => x.DeleteStatus == false && x.StageEntryType== "ExpenseStage").OrderBy(x => x.SeqNo).ToList();
            var incomeStageListEntity = _agriContext.ExpenseStageMasters.Where(x => x.DeleteStatus == false && x.StageEntryType == "IncomeStage").OrderBy(x => x.SeqNo).ToList();

            if (expenseStageListEntity.Count == 0 && incomeStageListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in expenseStageListEntity)
            {
                var model = new ExpenseStageModel();
                var TotalExpense = _agriContext.ExpensesDetails.Where(x => x.CropId == CropId && x.FarmerId == FarmerId && x.ExpenseStageId == item.ExpenseStageId).Sum(x => x.TotalAmount);
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseStageName = item.ExpenseStageName;
                model.SeqNo = item.SeqNo;
                model.DeleteStatus = (bool)item.DeleteStatus;
                model.ExpenseAmount = (decimal)TotalExpense;
                model.StageEntryType = item.StageEntryType;
                model1.ExpenseStages.Add(model);
            }
            foreach (var items in incomeStageListEntity)
            {
                var incomemodel = new ExpenseStageModel();
                var TotalExpense = _agriContext.ExpensesDetails.Where(x => x.CropId == CropId && x.FarmerId == FarmerId && x.ExpenseStageId == items.ExpenseStageId).Sum(x => x.TotalAmount);
                incomemodel.ExpenseStageId = items.ExpenseStageId;
                incomemodel.ExpenseStageName = items.ExpenseStageName;
                incomemodel.SeqNo = items.SeqNo;
                incomemodel.DeleteStatus = (bool)items.DeleteStatus;
                incomemodel.ExpenseAmount = (decimal)TotalExpense;
                incomemodel.StageEntryType = items.StageEntryType;
                model1.IncomeStages.Add(incomemodel);
            }
            return expenseStageModelList;

        }



        public ExpenseStageModel GetExpenseStageById(long ExpenseStageId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var expenseStageEntity = _agriContext.ExpenseStageMasters.FirstOrDefault(x => x.ExpenseStageId == ExpenseStageId && x.DeleteStatus == false);
            if (expenseStageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ExpenseStageModel
            {
                ExpenseStageId = expenseStageEntity.ExpenseStageId,
                ExpenseStageName = expenseStageEntity.ExpenseStageName,
                SeqNo = expenseStageEntity.SeqNo,
                DeleteStatus = (bool)expenseStageEntity.DeleteStatus,
                StageEntryType = expenseStageEntity.StageEntryType,
            };
        }

        public string AddExpenseStage(ExpenseStageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingPolicyType = _agriContext.ExpenseStageMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingPolicyType)
            {
                return null;
            }
            else
            {
                ExpenseStageMaster expenseTyppeEntity = new ExpenseStageMaster();
                expenseTyppeEntity.ExpenseStageId = model.ExpenseStageId;
                expenseTyppeEntity.ExpenseStageName = model.ExpenseStageName;
                expenseTyppeEntity.SeqNo = model.SeqNo;
                expenseTyppeEntity.DeleteStatus = false;
                expenseTyppeEntity.StageEntryType = model.StageEntryType;
                _agriContext.ExpenseStageMasters.Add(expenseTyppeEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateExpenseStage(ExpenseStageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ExpenseStageId = model.ExpenseStageId;
            var expenseStageEntity = _agriContext.ExpenseStageMasters.FirstOrDefault(x => x.ExpenseStageId == ExpenseStageId);
            if (expenseStageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                expenseStageEntity.ExpenseStageId = model.ExpenseStageId;
                expenseStageEntity.ExpenseStageName = model.ExpenseStageName;
                expenseStageEntity.SeqNo = model.SeqNo;
                expenseStageEntity.StageEntryType = model.StageEntryType;
                expenseStageEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteExpenseStage(long ExpenseStageId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ExpenseStageModel model = new ExpenseStageModel();
            var expenseStageEntity = _agriContext.ExpenseStageMasters.FirstOrDefault(x => x.ExpenseStageId == ExpenseStageId);
            if (expenseStageEntity != null)
            {
                expenseStageEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }



        ExpenseStagedetailModel IExpenseStageService.GetById(long ExpenseStageId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var StageOperationsListEntity = (from stage in _agriContext.ExpenseStageMasters
                                            join operation in _agriContext.ExpenseOperations
                                            on stage.ExpenseStageId equals operation.ExpenseStageId
                                            where operation.DeleteStatus == false && stage.ExpenseStageId == ExpenseStageId

                                            select new
                                            {
                                                stage.ExpenseStageId,
                                                stage.ExpenseStageName,
                                                operation.ExpenseOperationId,
                                                operation.ExpenseOperation1,
                                                operation.Description,
                                                stage.StageEntryType
                                             }
                                 ).FirstOrDefault();
            if (StageOperationsListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new ExpenseStagedetailModel();
            model.ExpenseStageId = StageOperationsListEntity.ExpenseStageId;
            model.ExpenseStageName = StageOperationsListEntity.ExpenseStageName;
            model.StageEntryType = StageOperationsListEntity.StageEntryType;
            var OperationList = _agriContext.ExpenseOperations
                              .Where(x => x.ExpenseStageId == StageOperationsListEntity.ExpenseStageId).ToList();
            foreach (var operations in OperationList)
            {
                var operationModel = new OperationModel();
                operationModel.ExpenseOperationId = operations.ExpenseOperationId;
                operationModel.ExpenseOperation1 = operations.ExpenseOperation1;
                operationModel.Description = operations.Description;

                model.Operations.Add(operationModel);
            }
            if (StageOperationsListEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }

        
    }
}
