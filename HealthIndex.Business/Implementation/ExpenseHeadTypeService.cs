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
   public class ExpenseHeadTypeService: IExpenseHeadTypeService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public ExpenseHeadTypeService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<ExpenseHeadTypeModel> GetAllExpenseHeadType()
        {
            var errorResponseModel = new ErrorResponseModel();
            var expenseTypeModelList = new List<ExpenseHeadTypeModel>();
            var expenseTypeListEntity = _agriContext.ExpenseHeadTypeMasters.Where(x => x.DeleteStatus == false).ToList();
            if (expenseTypeListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in expenseTypeListEntity)
            {
                var model = new ExpenseHeadTypeModel();
                model.ExpenseHeadTypeId = item.ExpenseHeadTypeId;
                model.ExpenseHeadType = item.ExpenseHeadType;
                model.Description = item.Description;
                model.DeleteStatus = (bool)item.DeleteStatus;
                expenseTypeModelList.Add(model);
            }
            return expenseTypeModelList;
        }

        public ExpenseHeadTypeModel GetExpenseHeadTypeById(long ExpenseHeadTypeId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var expenseTypeEntity = _agriContext.ExpenseHeadTypeMasters.FirstOrDefault(x => x.ExpenseHeadTypeId == ExpenseHeadTypeId && x.DeleteStatus==false);
            if (expenseTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ExpenseHeadTypeModel
            {
                ExpenseHeadTypeId = expenseTypeEntity.ExpenseHeadTypeId,
                ExpenseHeadType = expenseTypeEntity.ExpenseHeadType,
                Description = expenseTypeEntity.Description,
                DeleteStatus= (bool)expenseTypeEntity.DeleteStatus,
            };
        }

        public string AddExpenseHeadType(ExpenseHeadTypeModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingPolicyType = _agriContext.ExpenseHeadTypeMasters.Any(x => x.ExpenseHeadTypeId == model.ExpenseHeadTypeId);
            if (existingPolicyType)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                ExpenseHeadTypeMaster expenseTyppeEntity = new ExpenseHeadTypeMaster();
                expenseTyppeEntity.ExpenseHeadTypeId = model.ExpenseHeadTypeId;
                expenseTyppeEntity.ExpenseHeadType = model.ExpenseHeadType;
                expenseTyppeEntity.Description = model.Description;
                expenseTyppeEntity.DeleteStatus = false;
                _agriContext.ExpenseHeadTypeMasters.Add(expenseTyppeEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateExpenseHeadType(ExpenseHeadTypeModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ExpenseHeadTypeId = model.ExpenseHeadTypeId;
            var expenseTypeEntity = _agriContext.ExpenseHeadTypeMasters.FirstOrDefault(x => x.ExpenseHeadTypeId == ExpenseHeadTypeId);
            if (expenseTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                expenseTypeEntity.ExpenseHeadTypeId = model.ExpenseHeadTypeId;
                expenseTypeEntity.ExpenseHeadType = model.ExpenseHeadType;
                expenseTypeEntity.Description = model.Description;
                expenseTypeEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteExpenseHeadType(long ExpenseHeadTypeId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            ExpenseHeadTypeModel model = new ExpenseHeadTypeModel();
            var expenseTypeEntity = _agriContext.ExpenseHeadTypeMasters.FirstOrDefault(x => x.ExpenseHeadTypeId == ExpenseHeadTypeId);
            if (expenseTypeEntity != null)
            {
                expenseTypeEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }
}
