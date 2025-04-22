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
   public class ExpensesDetailService: IExpensesDetailService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ExpensesDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<ExpensesDetailModel> GetAllExpenseDetail()
        {
            var errorResponseModel = new ErrorResponseModel();
            var expenseDetailModelList = new List<ExpensesDetailModel>();
            var expenseDetailListEntity = (from ExpensesDetail in _agriContext.ExpensesDetails

                                           join ExpenseOperation in _agriContext.ExpenseOperations
                                           on ExpensesDetail.ExpenseOperationId equals ExpenseOperation.ExpenseOperationId

                                           join crop in _agriContext.CropMasters
                                           on ExpensesDetail.CropId equals crop.CropId

                                           join farmer in _agriContext.FarmerMasters
                                           on ExpensesDetail.FarmerId equals farmer.FarmerId

                                           join ExpenseStage in _agriContext.ExpenseStageMasters
                                           on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId
                                           //where ExpensesDetail.FarmerId == FarmerId

                                           select new
                                           {
                                               ExpensesDetail.ExpenseDetailsId,
                                               ExpensesDetail.ExpenseDate,
                                               crop.CropId,
                                               crop.CropName,
                                               farmer.FarmerId,
                                               farmer.FirstName,
                                               farmer.LastName,
                                               ExpensesDetail.Rate,
                                               ExpensesDetail.WorkersAmount,
                                               ExpensesDetail.NoofWorkers,
                                               ExpensesDetail.OperationAmount,
                                               ExpensesDetail.TotalAmount,
                                               ExpenseOperation.ExpenseOperationId,
                                               ExpenseOperation.ExpenseOperation1,
                                               ExpenseStage.ExpenseStageId,
                                               ExpenseStage.ExpenseStageName,
                                              
                                           }
                                  ).ToList();
            if (expenseDetailListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in expenseDetailListEntity)
            {
                var model = new ExpensesDetailModel();
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseStageName = item.ExpenseStageName;

                model.ExpenseDetailsId = item.ExpenseDetailsId;
                model.ExpenseOperationId = item.ExpenseOperationId;
                model.ExpenseOperation1 = item.ExpenseOperation1;
                model.FarmerId = item.FarmerId;
                model.FarmerName = item.FirstName +" "+ item.LastName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.ExpenseDate = item.ExpenseDate;
                model.NoofWorkers = item.NoofWorkers;
                model.WorkersAmount = item.WorkersAmount;
                model.OperationAmount = item.OperationAmount;
                model.TotalAmount = item.TotalAmount;
                model.Rate = item.Rate;
                expenseDetailModelList.Add(model);
            }
            return expenseDetailModelList;
        }

        public ExpensesDetailModel GetExpenseDetailById(long ExpenseDetailsId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var expenseDetailEntity = (from ExpensesDetail in _agriContext.ExpensesDetails

                                      join ExpenseOperation in _agriContext.ExpenseOperations
                                      on ExpensesDetail.ExpenseOperationId equals ExpenseOperation.ExpenseOperationId

                                      join crop in _agriContext.CropMasters
                                      on ExpensesDetail.CropId equals crop.CropId

                                      join farmer in _agriContext.FarmerMasters
                                      on ExpensesDetail.FarmerId equals farmer.FarmerId

                                      join ExpenseStage in _agriContext.ExpenseStageMasters
                                      on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId
                                      where ExpensesDetail.ExpenseDetailsId==ExpenseDetailsId
                                      select new
                                      {
                                          ExpensesDetail.ExpenseDetailsId,
                                          ExpensesDetail.ExpenseDate,
                                          crop.CropId,
                                          crop.CropName,
                                          farmer.FarmerId,
                                          farmer.FirstName,
                                          farmer.LastName,
                                          ExpensesDetail.Rate,
                                          ExpensesDetail.WorkersAmount,
                                          ExpensesDetail.NoofWorkers,
                                          ExpensesDetail.OperationAmount,
                                          ExpensesDetail.TotalAmount,
                                          ExpenseOperation.ExpenseOperationId,
                                          ExpenseOperation.ExpenseOperation1,
                                          ExpenseStage.ExpenseStageId,
                                          ExpenseStage.ExpenseStageName,

                                      }
                                  ).FirstOrDefault();
            if (expenseDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ExpensesDetailModel
            {
                ExpenseDetailsId = expenseDetailEntity.ExpenseDetailsId,
                ExpenseStageId = expenseDetailEntity.ExpenseStageId,
                ExpenseStageName = expenseDetailEntity.ExpenseStageName,
                ExpenseOperationId = expenseDetailEntity.ExpenseOperationId,
                ExpenseOperation1 = expenseDetailEntity.ExpenseOperation1,
                FarmerId=expenseDetailEntity.FarmerId,
                FarmerName=expenseDetailEntity.FirstName+" "+expenseDetailEntity.LastName,
                CropId=expenseDetailEntity.CropId,
                CropName=expenseDetailEntity.CropName,
                NoofWorkers=expenseDetailEntity.NoofWorkers,
                WorkersAmount=expenseDetailEntity.WorkersAmount,
                OperationAmount=expenseDetailEntity.OperationAmount,
                TotalAmount=expenseDetailEntity.TotalAmount,
                Rate=expenseDetailEntity.Rate,
            };
        }

        public string AddExpenseDetail(ExpensesDetailAddModel model, string EntryType, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.ExpensesDetails.Any(x => x.ExpenseDetailsId == model.ExpenseDetailsId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                ExpensesDetail expensedetailEntity = new ExpensesDetail();
                expensedetailEntity.ExpenseDetailsId = model.ExpenseDetailsId;
                expensedetailEntity.ExpenseOperationId = model.ExpenseOperationId;
                expensedetailEntity.ExpenseStageId = model.ExpenseStageId;
                expensedetailEntity.FarmerId = model.FarmerId;
                expensedetailEntity.CropId = model.CropId;
                expensedetailEntity.ExpenseDate = model.ExpenseDate;
                expensedetailEntity.NoofWorkers = model.NoofWorkers;
                expensedetailEntity.WorkersAmount = model.WorkersAmount;
                expensedetailEntity.OperationAmount = model.OperationAmount;
                expensedetailEntity.TotalAmount = model.TotalAmount;
                expensedetailEntity.Rate = model.Rate;
                if (EntryType == "Expense")
                {
                    expensedetailEntity.EntryType = "Expense";
                }
                if (EntryType == "Income")

                {
                    expensedetailEntity.EntryType = "Income";
                }
                _agriContext.ExpensesDetails.Add(expensedetailEntity);
                _agriContext.SaveChanges();

                var ExpenseOperationEntity = new ExpenseOperation();

                if (expensedetailEntity.ExpenseOperationId == 1)
                {
                    ExpenseOperationEntity.ExpenseOperationId = ExpenseOperationEntity.ExpenseOperationId;
                    ExpenseOperationEntity.ExpenseStageId = expensedetailEntity.ExpenseStageId;
                    ExpenseOperationEntity.ExpenseOperation1 = model.ExpenseOperation1;
                    ExpenseOperationEntity.DeleteStatus = false;
                    _agriContext.Add(ExpenseOperationEntity);
                    _agriContext.SaveChanges();

                }
                var ExpenseDetailsId = model.ExpenseDetailsId;
                var expensedetailEntitys = _agriContext.ExpensesDetails.FirstOrDefault(x => x.ExpenseOperationId == 1&& x.ExpenseDetailsId==expensedetailEntity.ExpenseDetailsId);
                if (expensedetailEntitys == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                    //return false;
                }
                else
                {
                   // expensedetailEntitys.ExpenseDetailsId = expensedetailEntity.ExpenseDetailsId;
                    expensedetailEntitys.ExpenseOperationId = ExpenseOperationEntity.ExpenseOperationId;
                    expensedetailEntitys.ExpenseStageId = model.ExpenseStageId;
                    expensedetailEntitys.FarmerId = model.FarmerId;
                    expensedetailEntitys.CropId = model.CropId;
                    expensedetailEntitys.ExpenseDate = model.ExpenseDate;
                    expensedetailEntitys.NoofWorkers = model.NoofWorkers;
                    expensedetailEntitys.WorkersAmount = model.WorkersAmount;
                    expensedetailEntitys.OperationAmount = model.OperationAmount;
                    expensedetailEntitys.TotalAmount = model.TotalAmount;
                    expensedetailEntitys.Rate = model.Rate;
                    _agriContext.SaveChanges();

                }
                


                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateExpenseDetail(ExpensesDetailAddModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ExpenseDetailsId = model.ExpenseDetailsId;
            var expensedetailEntity = _agriContext.ExpensesDetails.FirstOrDefault(x => x.ExpenseDetailsId == ExpenseDetailsId);
            if (expensedetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                expensedetailEntity.ExpenseDetailsId = model.ExpenseDetailsId;
                expensedetailEntity.ExpenseOperationId = model.ExpenseOperationId;
                expensedetailEntity.ExpenseStageId = model.ExpenseStageId;
                expensedetailEntity.FarmerId = model.FarmerId;
                expensedetailEntity.CropId = model.CropId;
                expensedetailEntity.ExpenseDate = model.ExpenseDate;
                expensedetailEntity.NoofWorkers = model.NoofWorkers;
                expensedetailEntity.WorkersAmount = model.WorkersAmount;
                expensedetailEntity.OperationAmount = model.OperationAmount;
                expensedetailEntity.TotalAmount = model.TotalAmount;
                expensedetailEntity.Rate = model.Rate;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteExpenseDetail(long ExpenseDetailsId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ExpensesDetailModel model = new ExpensesDetailModel();
            var expenseDetailEntity = _agriContext.ExpensesDetails.FirstOrDefault(x => x.ExpenseDetailsId == ExpenseDetailsId);
            if (expenseDetailEntity != null)
            {
                _agriContext.Remove(expenseDetailEntity);
                message = "Deleted Successfully";
            }
            _agriContext.SaveChanges();
            return message;
        }

        public List<ExpensesDetailModel> GetExpenseByIds(long FarmerId,long CropId,long StageId, string EntryType)
        {
            var errorResponseModel = new ErrorResponseModel();
            var expenseDetailModelList = new List<ExpensesDetailModel>();
            var expenseDetailListEntity = (from ExpensesDetail in _agriContext.ExpensesDetails

                                           join ExpenseOperation in _agriContext.ExpenseOperations
                                           on ExpensesDetail.ExpenseOperationId equals ExpenseOperation.ExpenseOperationId

                                           join crop in _agriContext.CropMasters
                                           on ExpensesDetail.CropId equals crop.CropId

                                           join farmer in _agriContext.FarmerMasters
                                           on ExpensesDetail.FarmerId equals farmer.FarmerId

                                           join ExpenseStage in _agriContext.ExpenseStageMasters
                                           on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId

                                           where ExpensesDetail.FarmerId == FarmerId && ExpensesDetail.CropId==CropId && ExpensesDetail.ExpenseStageId==StageId && ExpensesDetail.EntryType==EntryType

                                           select new
                                           {
                                               ExpensesDetail.ExpenseDetailsId,
                                               ExpensesDetail.ExpenseDate,
                                               crop.CropId,
                                               crop.CropName,
                                               farmer.FarmerId,
                                               farmer.FirstName,
                                               farmer.LastName,
                                               ExpensesDetail.Rate,
                                               ExpensesDetail.WorkersAmount,
                                               ExpensesDetail.NoofWorkers,
                                               ExpensesDetail.OperationAmount,
                                               ExpensesDetail.TotalAmount,
                                               ExpenseOperation.ExpenseOperationId,
                                               ExpenseOperation.ExpenseOperation1,
                                               ExpenseStage.ExpenseStageId,
                                               ExpenseStage.ExpenseStageName,
                                               ExpensesDetail.EntryType

                                           }
                                  ).ToList();
            if (expenseDetailListEntity==null)
            {
                //errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                //errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in expenseDetailListEntity)
            {
                var model = new ExpensesDetailModel();
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseStageName = item.ExpenseStageName;
                model.ExpenseDetailsId = item.ExpenseDetailsId;
                model.ExpenseOperationId = item.ExpenseOperationId;
                model.ExpenseOperation1 = item.ExpenseOperation1;
                model.FarmerId = item.FarmerId;
                model.FarmerName = item.FirstName + " " + item.LastName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.ExpenseDate = item.ExpenseDate;
                model.NoofWorkers = item.NoofWorkers;
                model.WorkersAmount = item.WorkersAmount;
                model.OperationAmount = item.OperationAmount;
                model.TotalAmount = item.TotalAmount;
                model.Rate = item.Rate;
                model.EntryType = item.EntryType;
                expenseDetailModelList.Add(model);
            }
            return expenseDetailModelList;
            

        }



    }
}
