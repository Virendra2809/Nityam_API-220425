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
   public class ExpenseOperationService: IExpenseOperationService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ExpenseOperationService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<ExpenseOperationModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var ExpenseOperationModelList = new List<ExpenseOperationModel>();
            var ExpenseOperationModelListEntity = (from ExpenseOperation in _agriContext.ExpenseOperations

                                             join ExpenseType in _agriContext.ExpenseHeadTypeMasters
                                             on ExpenseOperation.ExpenseHeadTypeId equals ExpenseType.ExpenseHeadTypeId

                                             join ExpenseStage in _agriContext.ExpenseStageMasters
                                             on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId
                                             where ExpenseOperation.DeleteStatus == false

                                             select new
                                             {
                                                 ExpenseOperation.ExpenseOperationId,
                                                 ExpenseOperation.ExpenseOperation1,
                                                 ExpenseOperation.Description,
                                                 ExpenseStage.ExpenseStageId,
                                                 ExpenseStage.ExpenseStageName,
                                                 ExpenseType.ExpenseHeadTypeId,
                                                 ExpenseType.ExpenseHeadType,
                                                 ExpenseOperation.DeleteStatus,

                                             }
                                  ).ToList();
            if (ExpenseOperationModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in ExpenseOperationModelListEntity)
            {
                var model = new ExpenseOperationModel();
                model.ExpenseOperationId = item.ExpenseOperationId;
                model.ExpenseHeadTypeId = item.ExpenseHeadTypeId;
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseOperation1 = item.ExpenseOperation1;
                model.Description = item.Description;
                model.ExpenseStageName = item.ExpenseStageName;
                model.ExpenseHeadType = item.ExpenseHeadType;
                model.DeleteStatus = (bool)item.DeleteStatus;
               
                ExpenseOperationModelList.Add(model);
            }
            return ExpenseOperationModelList;
        }

        public List<ExpenseOperationsModel> GetAllOperation()
        {
            var errorResponseModel = new ErrorResponseModel();
            var ExpenseOperationModelList = new List<ExpenseOperationsModel>();
            var ExpenseOperationModelListEntity = (from ExpenseOperation in _agriContext.ExpenseOperations

                                                   
                                                   join ExpenseStage in _agriContext.ExpenseStageMasters
                                                   on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId
                                                   where ExpenseOperation.DeleteStatus == false

                                                   select new
                                                   {
                                                       ExpenseOperation.ExpenseOperationId,
                                                       ExpenseOperation.ExpenseOperation1,
                                                       ExpenseOperation.Description,
                                                       ExpenseStage.ExpenseStageId,
                                                       ExpenseStage.ExpenseStageName,
                                                       ExpenseOperation.DeleteStatus,

                                                   }
                                  ).ToList();
            if (ExpenseOperationModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in ExpenseOperationModelListEntity)
            {
                var model = new ExpenseOperationsModel();
                model.ExpenseOperationId = item.ExpenseOperationId;
                model.ExpenseStageId = item.ExpenseStageId;
                model.ExpenseOperation1 = item.ExpenseOperation1;
                model.Description = item.Description;
                model.ExpenseStageName = item.ExpenseStageName;
                ExpenseOperationModelList.Add(model);
            }
            return ExpenseOperationModelList;
        }

        public string Add(ExpenseOperationModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.ExpenseOperations.Any(x => x.ExpenseOperationId == model.ExpenseOperationId);
                if (existing)
                {
                    message = GlobalConstants.NotFoundMessage;
                }

                else
                {
                    var ExpenseOperationEntity = new ExpenseOperation();
                    ExpenseOperationEntity.ExpenseOperationId = Convert.ToInt32(model.ExpenseOperationId);
                    ExpenseOperationEntity.ExpenseHeadTypeId = model.ExpenseHeadTypeId;
                    ExpenseOperationEntity.ExpenseStageId = model.ExpenseStageId;
                    ExpenseOperationEntity.ExpenseOperation1 = model.ExpenseOperation1;
                    ExpenseOperationEntity.Description = model.Description;
                    ExpenseOperationEntity.DeleteStatus = false;

                    _agriContext.ExpenseOperations.Add(ExpenseOperationEntity);
                    _agriContext.SaveChanges();

                    message = "ExpenseOperation Added Succesfully ";
                }
                return message;
            }

        }

        ExpenseOperationModel IExpenseOperationService.GetById(long ExpenseOperationId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var ExpenseOperationEntity = (from ExpenseOperation in _agriContext.ExpenseOperations
                                          join ExpenseType in _agriContext.ExpenseHeadTypeMasters
                                          on ExpenseOperation.ExpenseHeadTypeId equals ExpenseType.ExpenseHeadTypeId

                                          join ExpenseStage in _agriContext.ExpenseStageMasters
                                          on ExpenseOperation.ExpenseStageId equals ExpenseStage.ExpenseStageId

                                          where ExpenseOperation.DeleteStatus == false && ExpenseOperation.ExpenseOperationId == ExpenseOperationId

                                    select new
                                    {
                                        ExpenseOperation.ExpenseOperationId,
                                        ExpenseOperation.ExpenseOperation1,
                                        ExpenseOperation.Description,
                                        ExpenseStage.ExpenseStageId,
                                        ExpenseStage.ExpenseStageName,
                                        ExpenseType.ExpenseHeadTypeId,
                                        ExpenseType.ExpenseHeadType,
                                        ExpenseOperation.DeleteStatus,

                                    }
                                  ).FirstOrDefault();
            if (ExpenseOperationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ExpenseOperationModel
            {
                ExpenseOperationId = ExpenseOperationEntity.ExpenseOperationId,
                ExpenseHeadTypeId = ExpenseOperationEntity.ExpenseHeadTypeId,
                ExpenseHeadType = ExpenseOperationEntity.ExpenseHeadType,
                ExpenseOperation1 = ExpenseOperationEntity.ExpenseOperation1,
                Description=ExpenseOperationEntity.Description,
                ExpenseStageId = ExpenseOperationEntity.ExpenseStageId,
                ExpenseStageName = ExpenseOperationEntity.ExpenseStageName,
                DeleteStatus =(bool)ExpenseOperationEntity.DeleteStatus,
                
            };

        }


        public bool Put(ExpenseOperationModel model, ref ErrorResponseModel errorResponseModel)
        {

            var ExpenseOperationId = Convert.ToInt32(model.ExpenseOperationId);
            var ExpenseOperationEntity = _agriContext.ExpenseOperations.FirstOrDefault(x => x.ExpenseOperationId == ExpenseOperationId && x.DeleteStatus==false);
            if (ExpenseOperationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                ExpenseOperationEntity.ExpenseOperationId = Convert.ToInt32(model.ExpenseOperationId);
                ExpenseOperationEntity.ExpenseHeadTypeId = model.ExpenseHeadTypeId;
                ExpenseOperationEntity.ExpenseOperation1 = model.ExpenseOperation1;
                ExpenseOperationEntity.Description = model.Description;
                ExpenseOperationEntity.ExpenseStageId = model.ExpenseStageId;
                ExpenseOperationEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long ExpenseOperationId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ExpenseOperationModel model = new ExpenseOperationModel();
            var ExpenseOperationEntity = _agriContext.ExpenseOperations.FirstOrDefault(x => x.ExpenseOperationId == ExpenseOperationId);
            if (ExpenseOperationEntity != null)
            {
                ExpenseOperationEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }

}


