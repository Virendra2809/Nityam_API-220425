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
   public class SoilTypeMasterService:ISoilTypeMasterService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SoilTypeMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<SoilTypeMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var SoilTypeMasterModelList = new List<SoilTypeMasterModel>();
            var SoilTypeMasterListEntity = (from SoilTypeMaster in _agriContext.SoilTypeMasters
                                              where SoilTypeMaster.DeleteStatus == false
                                              select new
                                              {
                                                  SoilTypeMaster.SoilTypeId,
                                                  SoilTypeMaster.SoilTypeName,
                                                  SoilTypeMaster.Description,
                                                  SoilTypeMaster.EnteredBy,
                                                  SoilTypeMaster.EnteredDate,
                                                  SoilTypeMaster.ChangedBy,
                                                  SoilTypeMaster.ChangedDate,
                                                  SoilTypeMaster.DeleteStatus,

                                              }
                                  ).ToList();
            if (SoilTypeMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in SoilTypeMasterListEntity)
            {
                var model = new SoilTypeMasterModel();
                model.SoilTypeId = item.SoilTypeId;
                model.SoilTypeName = item.SoilTypeName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = (bool)item.DeleteStatus;

                SoilTypeMasterModelList.Add(model);
            }
            return SoilTypeMasterModelList;

        }
        SoilTypeMasterModel ISoilTypeMasterService.GetById(long SoilTypeId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var SoilTypeEntity = _agriContext.SoilTypeMasters.FirstOrDefault(x => x.SoilTypeId == SoilTypeId && x.DeleteStatus==false);
            if (SoilTypeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SoilTypeMasterModel
            {
                SoilTypeId = SoilTypeEntity.SoilTypeId,
                SoilTypeName = SoilTypeEntity.SoilTypeName,
                Description = SoilTypeEntity.Description,
                EnteredBy = SoilTypeEntity.EnteredBy,
                EnteredDate = SoilTypeEntity.EnteredDate,
                ChangedBy = SoilTypeEntity.ChangedBy,
                ChangedDate = SoilTypeEntity.ChangedDate,
                DeleteStatus = (bool)SoilTypeEntity.DeleteStatus,
            };

        }

        public string Add(SoilTypeMasterModel model, ref ErrorResponseModel errorResponseModel)

        {

            var message = string.Empty;

            var existing = _agriContext.SoilTypeMasters.Any(x => x.SoilTypeId == model.SoilTypeId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var SoilEntity = new SoilTypeMaster();

                SoilEntity.SoilTypeId = model.SoilTypeId;
                SoilEntity.SoilTypeName = model.SoilTypeName;
                SoilEntity.Description = model.Description;
                SoilEntity.EnteredBy = model.EnteredBy;
                SoilEntity.EnteredDate = DateTime.Now;
                SoilEntity.DeleteStatus = false;

                _agriContext.SoilTypeMasters.Add(SoilEntity);
                _agriContext.SaveChanges();

                message = "SoilTypeMaster Added Succesfully ";
            }
            return message;
        }


        public bool Put(SoilTypeMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var SoilTypeId = Convert.ToInt32(model.SoilTypeId);
            var SoilEntity = _agriContext.SoilTypeMasters.FirstOrDefault(x => x.SoilTypeId == SoilTypeId && x.DeleteStatus==false);
            if (SoilEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                SoilEntity.SoilTypeId = model.SoilTypeId;
                SoilEntity.SoilTypeName = model.SoilTypeName;
                SoilEntity.Description = model.Description;
                SoilEntity.ChangedBy = model.ChangedBy;
                SoilEntity.ChangedDate = DateTime.Now;
                SoilEntity.DeleteStatus = false;

                _agriContext.SaveChanges();
                return true;
            }
        }
        public string Delete(long SoilTypeId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SoilTypeMasterModel model = new SoilTypeMasterModel();
            var SoilTypeMasterEntity = _agriContext.SoilTypeMasters.FirstOrDefault(x => x.SoilTypeId == SoilTypeId);
            if (SoilTypeMasterEntity != null)
            {
                SoilTypeMasterEntity.DeleteStatus = true;
                SoilTypeMasterEntity.ChangedBy = model.ChangedBy;
                SoilTypeMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}

