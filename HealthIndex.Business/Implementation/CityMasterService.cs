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
   public class CityMasterService:ICityMasterService
    {

        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CityMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<CityMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CityMasterModelList = new List<CityMasterModel>();
            var CityMasterModelListEntity = (from CityMaster in _agriContext.CityMasters
                                             join StateMaster in _agriContext.StateMasters
                                             on CityMaster.StateId equals StateMaster.StateId
                                             where CityMaster.DeleteStatus == false

                                              select new
                                                  {
                                                     CityMaster.CityId,
                                                     CityMaster.CityName,
                                                     StateMaster.StateId,
                                                     StateMaster.StateName,
                                                     CityMaster.ChangedBy,
                                                     CityMaster.ChangedDate,
                                                     CityMaster.EnteredBy,
                                                     CityMaster.EnteredDate,
                                                     CityMaster.DeleteStatus,
                                                     
                                                   }
                                  ).ToList();
            if (CityMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CityMasterModelListEntity)
            {
                var model = new CityMasterModel();
                model.CityId = item.CityId;
                model.StateId =item.StateId;
                model.CityName = item.CityName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.StateName = item.StateName;

                CityMasterModelList.Add(model);
            }
            return CityMasterModelList;
        }


        public string Add(CityMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.CityMasters.Any(x => x.CityId == model.CityId);
                if (existing)
                {
                    message = GlobalConstants.NotFoundMessage;
                }

                else
                {
                    var CityMasterEntity = new CityMaster();
                    CityMasterEntity.CityId = Convert.ToInt32(model.CityId);
                    CityMasterEntity.StateId = model.StateId;
                    CityMasterEntity.CityName = model.CityName;
                    CityMasterEntity.EnteredBy = model.LoggedUserId;
                    CityMasterEntity.EnteredDate = DateTime.Now;
                    CityMasterEntity.DeleteStatus = model.DeleteStatus;

                    _agriContext.CityMasters.Add(CityMasterEntity);
                    _agriContext.SaveChanges();

                    message = "CityMaster Added Succesfully ";
                }
                return message;
            }

        }

        CityMasterModel ICityMasterService.GetById(long CityId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var CityMasterEntity = (from CityMaster in _agriContext.CityMasters
                                    join StateMaster in _agriContext.StateMasters
                                    on CityMaster.StateId equals StateMaster.StateId
                                    where CityMaster.DeleteStatus == false && CityMaster.CityId==CityId

                                    select new
                                    {
                                        CityMaster.CityId,
                                        CityMaster.CityName,
                                        StateMaster.StateId,
                                        StateMaster.StateName,
                                        CityMaster.ChangedBy,
                                        CityMaster.ChangedDate,
                                        CityMaster.EnteredBy,
                                        CityMaster.EnteredDate,
                                        CityMaster.DeleteStatus,

                                    }
                                  ).FirstOrDefault();
            if (CityMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CityMasterModel
            {
                CityId = CityMasterEntity.CityId,
                StateId = CityMasterEntity.StateId,
                CityName = CityMasterEntity.CityName,
                EnteredBy = CityMasterEntity.EnteredBy,
                EnteredDate = CityMasterEntity.EnteredDate,
                ChangedBy = CityMasterEntity.ChangedBy,
                ChangedDate = CityMasterEntity.ChangedDate,
                DeleteStatus = CityMasterEntity.DeleteStatus,
                StateName = CityMasterEntity.StateName,
            };

        }


        public bool Put(CityMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var CityId = Convert.ToInt32(model.CityId);
            var CityMasterEntity = _agriContext.CityMasters.FirstOrDefault(x => x.CityId == CityId && !x.DeleteStatus);
            if (CityMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                CityMasterEntity.CityId = Convert.ToInt32(model.CityId);
                CityMasterEntity.StateId = model.StateId;
                CityMasterEntity.CityName = model.CityName;
                CityMasterEntity.ChangedBy = model.LoggedUserId;
                CityMasterEntity.ChangedDate = DateTime.Now;
                CityMasterEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long CityId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CityMasterModel model = new CityMasterModel();
            var cityMasterEntity = _agriContext.CityMasters.FirstOrDefault(x => x.CityId == CityId);
            if (cityMasterEntity != null)
            {
                cityMasterEntity.DeleteStatus = true;
                cityMasterEntity.ChangedBy = model.ChangedBy;
                cityMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }

}

