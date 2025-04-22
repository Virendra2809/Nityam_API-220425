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
   public class CropCultivationProcessService:ICropCultivationProcessService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public CropCultivationProcessService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropCultivationProcessModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropCultivationProcessList = new List<CropCultivationProcessModel>();
            var CropCultivationProcessEntity = (from CropCultivationProcess in _agriContext.CropCultivationProcesses
                                               where CropCultivationProcess.DeleteStatus == false
                                               select new
                                               {
                                                   CropCultivationProcess.CropCultivationProcessId,
                                                   CropCultivationProcess.CropCultivationProcessName,
                                                   CropCultivationProcess.SeqNo,
                                                   CropCultivationProcess.Description,
                                                   CropCultivationProcess.IsActive,
                                                   CropCultivationProcess.EnteredBy,
                                                   CropCultivationProcess.EnteredDate,
                                                   CropCultivationProcess.ChangedBy,
                                                   CropCultivationProcess.ChangedDate,
                                                   CropCultivationProcess.DeleteStatus,

                                               }
                                  ).ToList();
            if (CropCultivationProcessEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropCultivationProcessEntity)
            {
                var model = new CropCultivationProcessModel();
                model.CropCultivationProcessId = item.CropCultivationProcessId;
                model.CropCultivationProcessName = item.CropCultivationProcessName;
                model.SeqNo = item.SeqNo;
                model.IsActive = item.IsActive;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = (bool)item.DeleteStatus;

                CropCultivationProcessList.Add(model);
            }
            return CropCultivationProcessList;



        }
        CropCultivationProcessModel ICropCultivationProcessService.GetById(long CropCultivationProcessId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropCultivationEntity = _agriContext.CropCultivationProcesses.FirstOrDefault(x => x.CropCultivationProcessId == CropCultivationProcessId && x.DeleteStatus==false);
            if (cropCultivationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropCultivationProcessModel
            {
                CropCultivationProcessId = cropCultivationEntity.CropCultivationProcessId,
                CropCultivationProcessName = cropCultivationEntity.CropCultivationProcessName,
                Description = cropCultivationEntity.Description,
                SeqNo= cropCultivationEntity.SeqNo,
                IsActive= cropCultivationEntity.IsActive,
                EnteredBy = cropCultivationEntity.EnteredBy,
                EnteredDate = cropCultivationEntity.EnteredDate,
                ChangedBy = cropCultivationEntity.ChangedBy,
                ChangedDate = cropCultivationEntity.ChangedDate,
                DeleteStatus = (bool)cropCultivationEntity.DeleteStatus,
            };

        }


        public string Add(CropCultivationProcessModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.CropCultivationProcesses.Any(x => x.SeqNo == model.SeqNo);
            if (existing)
            {
                //message = GlobalConstants.NotFoundMessage;
                return null;
            }
            else
            {
                var CropCultivationEntity = new CropCultivationProcess();

                CropCultivationEntity.CropCultivationProcessId = model.CropCultivationProcessId;
                CropCultivationEntity.CropCultivationProcessName = model.CropCultivationProcessName;
                CropCultivationEntity.Description = model.Description;
                CropCultivationEntity.SeqNo = model.SeqNo;
                CropCultivationEntity.IsActive = model.IsActive;
                CropCultivationEntity.EnteredBy = model.EnteredBy;
                CropCultivationEntity.EnteredDate = DateTime.Now;
                CropCultivationEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.CropCultivationProcesses.Add(CropCultivationEntity);
                _agriContext.SaveChanges();
                message = "CropCultivation Added Succesfully ";
            }
            return message;
        }

        public bool Put(CropCultivationProcessModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropCultivationProcessId = Convert.ToInt32(model.CropCultivationProcessId);
            var CropCultivationEntity = _agriContext.CropCultivationProcesses.FirstOrDefault(x => x.CropCultivationProcessId == CropCultivationProcessId && x.DeleteStatus==false);
            if (CropCultivationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropCultivationEntity.CropCultivationProcessId = model.CropCultivationProcessId;
                CropCultivationEntity.CropCultivationProcessName = model.CropCultivationProcessName;
                CropCultivationEntity.Description = model.Description;
                CropCultivationEntity.SeqNo = model.SeqNo;
                CropCultivationEntity.IsActive = model.IsActive;
                CropCultivationEntity.ChangedBy = model.ChangedBy;
                CropCultivationEntity.ChangedDate = DateTime.Now;
                CropCultivationEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                return true;
            }
        }
        public string Delete(long CropCultivationProcessId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropCultivationProcessModel model = new CropCultivationProcessModel();
            var CropCultivationEntity = _agriContext.CropCultivationProcesses.FirstOrDefault(x => x.CropCultivationProcessId == CropCultivationProcessId);
            if (CropCultivationEntity != null)
            {
                CropCultivationEntity.ChangedBy = model.ChangedBy;
                CropCultivationEntity.ChangedDate = DateTime.Now;
                CropCultivationEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}



