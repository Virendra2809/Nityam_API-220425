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
   public class UnitofMeasurementMasterService: IUnitofMeasurementMasterService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public UnitofMeasurementMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<UnitofMeasurementMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var UnitofMeasurementMasterModelList = new List<UnitofMeasurementMasterModel>();
            var UnitofMeasurementMasterListEntity = (from UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                                     where UnitofMeasurementMaster.DeleteStatus == false
                                                     select new
                                   {
                                       UnitofMeasurementMaster.UnitId,
                                       UnitofMeasurementMaster.UnitName,
                                       UnitofMeasurementMaster.UnitAlias,
                                       UnitofMeasurementMaster.EnteredBy,
                                       UnitofMeasurementMaster.EnteredDate,
                                       UnitofMeasurementMaster.ChangedBy,
                                       UnitofMeasurementMaster.ChangedDate,
                                       UnitofMeasurementMaster.DeleteStatus,

                                   }
                                  ).ToList();
            if (UnitofMeasurementMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in UnitofMeasurementMasterListEntity)
            {
                var model = new UnitofMeasurementMasterModel();
                model.UnitId = item.UnitId;
                model.UnitName = item.UnitName;
                model.UnitAlias = item.UnitAlias;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                UnitofMeasurementMasterModelList.Add(model);
            }
            return UnitofMeasurementMasterModelList;



        }
        UnitofMeasurementMasterModel IUnitofMeasurementMasterService.GetById(long UnitId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var UnitofMeasurementMasterEntity = _agriContext.UnitofMeasurementMasters.FirstOrDefault(x => x.UnitId == UnitId && !x.DeleteStatus);
            if (UnitofMeasurementMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new UnitofMeasurementMasterModel
            {
                UnitId=UnitofMeasurementMasterEntity.UnitId,
                UnitName=UnitofMeasurementMasterEntity.UnitName,
                UnitAlias=UnitofMeasurementMasterEntity.UnitAlias,
                EnteredBy = UnitofMeasurementMasterEntity.EnteredBy,
                EnteredDate = UnitofMeasurementMasterEntity.EnteredDate,
                ChangedBy = UnitofMeasurementMasterEntity.ChangedBy,
                ChangedDate = UnitofMeasurementMasterEntity.ChangedDate,
                DeleteStatus = UnitofMeasurementMasterEntity.DeleteStatus,
            };

        }

        public string Add(UnitofMeasurementMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.UnitofMeasurementMasters.Any(x => x.UnitId == model.UnitId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var UnitofMeasurementMasterEntity = new UnitofMeasurementMaster();
                UnitofMeasurementMasterEntity.UnitId = model.UnitId;
                UnitofMeasurementMasterEntity.UnitName = model.UnitName;
                UnitofMeasurementMasterEntity.UnitAlias = model.UnitAlias;
                UnitofMeasurementMasterEntity.EnteredBy = model.EnteredBy;
                UnitofMeasurementMasterEntity.EnteredDate = DateTime.Now;
                UnitofMeasurementMasterEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.UnitofMeasurementMasters.Add(UnitofMeasurementMasterEntity);
                _agriContext.SaveChanges();

                message = "UnitofMeasurementMasterEntity Added Succesfully ";
            }
            return message;
        }



        public bool Put(UnitofMeasurementMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var UnitId = Convert.ToInt32(model.UnitId);
            var UnitofMeasurementMasterEntity = _agriContext.UnitofMeasurementMasters.FirstOrDefault(x => x.UnitId == UnitId && !x.DeleteStatus);
            if (UnitofMeasurementMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }

            else
            {
                UnitofMeasurementMasterEntity.UnitId = model.UnitId;
                UnitofMeasurementMasterEntity.UnitName = model.UnitName;
                UnitofMeasurementMasterEntity.UnitAlias = model.UnitAlias;
                UnitofMeasurementMasterEntity.ChangedBy = model.ChangedBy;
                UnitofMeasurementMasterEntity.ChangedDate = DateTime.Now;
                UnitofMeasurementMasterEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long UnitId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            UnitofMeasurementMasterModel model = new UnitofMeasurementMasterModel();
            var unitofMeasurementMasterEntity = _agriContext.UnitofMeasurementMasters.FirstOrDefault(x => x.UnitId == UnitId);
            if (unitofMeasurementMasterEntity != null)
            {
                unitofMeasurementMasterEntity.DeleteStatus = true;
                unitofMeasurementMasterEntity.ChangedBy = model.ChangedBy;
                unitofMeasurementMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}

