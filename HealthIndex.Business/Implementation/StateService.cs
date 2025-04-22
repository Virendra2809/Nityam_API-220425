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
   public class StateService:IStateService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public StateService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<StateModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var StateModelList = new List<StateModel>();
            var StateListEntity = (from  StateMaster in _agriContext.StateMasters
                                   where StateMaster.DeleteStatus == false
                                   select new
                                               {
                                                   StateMaster.StateId,
                                                   StateMaster.StateName,
                                                   StateMaster.EnteredBy,
                                                   StateMaster.EnteredDate,
                                                   StateMaster.ChangedBy,
                                                   StateMaster.ChangedDate,
                                                   StateMaster.DeleteStatus,

                                               }
                                  ).ToList();
            if (StateListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in StateListEntity)
            {
                var model = new StateModel();
                model.StateId = item.StateId;
                model.StateName = item.StateName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                StateModelList.Add(model);
            }
            return StateModelList;



        }
        StateModel IStateService.GetById(long StateId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var StateEntity = _agriContext.StateMasters.FirstOrDefault(x => x.StateId == StateId && !x.DeleteStatus);
            if (StateEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new StateModel
            {
                StateId = StateEntity.StateId,
                StateName = StateEntity.StateName,
                EnteredBy = StateEntity.EnteredBy,
                EnteredDate = StateEntity.EnteredDate,
                ChangedBy = StateEntity.ChangedBy,
                ChangedDate = StateEntity.ChangedDate,
                DeleteStatus = StateEntity.DeleteStatus,
            };

        }


        public string Add(StateModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.StateMasters.Any(x => x.StateName == model.StateName);
            if (existing)
            {
                message="State Name Already Exist";
            }
            else

            {
                var StateEntity = new StateMaster();
                StateEntity.StateId = model.StateId;
                StateEntity.StateName = model.StateName;
                StateEntity.EnteredBy = model.EnteredBy;
                StateEntity.EnteredDate = DateTime.Now;
                StateEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.StateMasters.Add(StateEntity);
                _agriContext.SaveChanges();

                message = "StateMaster Added Succesfully ";
            }
            return message;
        }

        public bool Put(StateModel model, ref ErrorResponseModel errorResponseModel)
        {
            var StateId = Convert.ToInt32(model.StateId);
            var StateEntity = _agriContext.StateMasters.FirstOrDefault(x => x.StateId == StateId && !x.DeleteStatus);
            if (StateEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                StateEntity.StateId = model.StateId;
                StateEntity.StateName = model.StateName;
                StateEntity.ChangedBy = model.ChangedBy;
                StateEntity.ChangedDate = DateTime.Now;
                StateEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long StateId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            StateModel model = new StateModel();
            var stateMasterEntity = _agriContext.StateMasters.FirstOrDefault(x => x.StateId == StateId);
            if (stateMasterEntity != null)
            {
                stateMasterEntity.DeleteStatus = true;
                stateMasterEntity.ChangedBy = model.ChangedBy;
                stateMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}

