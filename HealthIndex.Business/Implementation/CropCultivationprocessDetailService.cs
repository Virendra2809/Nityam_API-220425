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
    public class CropCultivationprocessDetailService:ICropCultivationProcessDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public CropCultivationprocessDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropCultivationProcessDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropCultivationProcessDetailList = new List<CropCultivationProcessDetailModel>();
            var CropCultivationProcessDetailEntity = (from CropCultivationProcessDetail in _agriContext.CropCultivationProcessDetails
                                                      join CropCultivationProcess in _agriContext.CropCultivationProcesses
                                                      on CropCultivationProcessDetail.CropCultivationProcessId equals CropCultivationProcess.CropCultivationProcessId
                                             
                                                      join CropMaster in _agriContext.CropMasters
                                                      on CropCultivationProcessDetail.CropId equals CropMaster.CropId
                                                      where CropCultivationProcessDetail.DeleteStatus == false
                                                      select new
                                                   {

                                                    CropCultivationProcessDetail.CropCultivationProcessDetailId,
                                                    CropCultivationProcess.CropCultivationProcessId,
                                                    CropCultivationProcess.CropCultivationProcessName,
                                                    CropCultivationProcessDetail.Title,
                                                    CropMaster.CropId,
                                                    CropMaster.CropName,
                                                    CropCultivationProcessDetail.SeqNo,
                                                    Description = CropCultivationProcessDetail.Description == null?"": CropCultivationProcessDetail.Description,

                                                       //   CropCultivationProcessDetail.Description,
                                                    CropCultivationProcessDetail.IsActive,
                                                    CropCultivationProcessDetail.EnteredBy,
                                                    CropCultivationProcessDetail.EnteredDate,
                                                    CropCultivationProcessDetail.ChangedBy,
                                                    CropCultivationProcessDetail.ChangedDate,
                                                    CropCultivationProcessDetail.DeleteStatus,

                                                }
                                  ).ToList();
            if (CropCultivationProcessDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropCultivationProcessDetailEntity)
            {
                var model = new CropCultivationProcessDetailModel();
                model.CropCultivationProcessDetailId = item.CropCultivationProcessDetailId;
                model.CropCultivationProcessId = item.CropCultivationProcessId;
                model.CropCultivationProcessName = item.CropCultivationProcessName;
                model.Title = item.Title;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeqNo = item.SeqNo;
                model.IsActive = item.IsActive;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = (bool)item.DeleteStatus;

                CropCultivationProcessDetailList.Add(model);
            }
            return CropCultivationProcessDetailList;



        }
        CropCultivationProcessDetailModel ICropCultivationProcessDetailService.GetById(long CropCultivationProcessDetailId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropCultivationDetailEntity = (from CropCultivationProcessDetail in _agriContext.CropCultivationProcessDetails
                                               where CropCultivationProcessDetail.DeleteStatus == false
                                               join CropCultivationProcess in _agriContext.CropCultivationProcesses
                                               on CropCultivationProcessDetail.CropCultivationProcessId equals CropCultivationProcess.CropCultivationProcessId
                                               where CropCultivationProcessDetail.DeleteStatus == false && CropCultivationProcessDetail.CropCultivationProcessDetailId == CropCultivationProcessDetailId

                                               join CropMaster in _agriContext.CropMasters
                                               on CropCultivationProcessDetail.CropId equals CropMaster.CropId

                                               where CropCultivationProcessDetail.DeleteStatus == false && CropCultivationProcessDetail.CropCultivationProcessDetailId == CropCultivationProcessDetailId
                                               select new
                                               {

                                                   CropCultivationProcessDetail.CropCultivationProcessDetailId,
                                                   CropCultivationProcess.CropCultivationProcessId,
                                                   CropCultivationProcess.CropCultivationProcessName,
                                                   CropCultivationProcessDetail.Title,
                                                   CropMaster.CropId,
                                                   CropMaster.CropName,
                                                   CropCultivationProcessDetail.SeqNo,
                                                   CropCultivationProcessDetail.Description,
                                                   CropCultivationProcessDetail.IsActive,
                                                   CropCultivationProcessDetail.EnteredBy,
                                                   CropCultivationProcessDetail.EnteredDate,
                                                   CropCultivationProcessDetail.ChangedBy,
                                                   CropCultivationProcessDetail.ChangedDate,
                                                   CropCultivationProcessDetail.DeleteStatus,

                                               }
                                  ).FirstOrDefault();
            if (cropCultivationDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropCultivationProcessDetailModel
            {
                CropCultivationProcessDetailId=cropCultivationDetailEntity.CropCultivationProcessDetailId,
                CropCultivationProcessId = cropCultivationDetailEntity.CropCultivationProcessId,
                CropCultivationProcessName = cropCultivationDetailEntity.CropCultivationProcessName,
                Title=cropCultivationDetailEntity.Title,
                CropId=cropCultivationDetailEntity.CropId,
                CropName=cropCultivationDetailEntity.CropName,
                Description = cropCultivationDetailEntity.Description,
                SeqNo = cropCultivationDetailEntity.SeqNo,
                IsActive = cropCultivationDetailEntity.IsActive,
                EnteredBy = cropCultivationDetailEntity.EnteredBy,
                EnteredDate = cropCultivationDetailEntity.EnteredDate,
                ChangedBy = cropCultivationDetailEntity.ChangedBy,
                ChangedDate = cropCultivationDetailEntity.ChangedDate,
                DeleteStatus = (bool)cropCultivationDetailEntity.DeleteStatus,
            };

        }


        public string Add(CropCultivationProcessDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.CropCultivationProcessDetails.Any(x => x.SeqNo == model.SeqNo);
            if (existing)
            {
                return null;
            }
            else
            {
                var cropCultivationDetailEntity = new CropCultivationProcessDetail();
                cropCultivationDetailEntity.CropCultivationProcessDetailId = model.CropCultivationProcessDetailId;
                cropCultivationDetailEntity.CropCultivationProcessId = model.CropCultivationProcessId;
                cropCultivationDetailEntity.Title = model.Title;
                cropCultivationDetailEntity.CropId = model.CropId;
                cropCultivationDetailEntity.Description = model.Description;
                cropCultivationDetailEntity.SeqNo = model.SeqNo;
                cropCultivationDetailEntity.IsActive = model.IsActive;
                cropCultivationDetailEntity.EnteredBy = model.EnteredBy;
                cropCultivationDetailEntity.EnteredDate = DateTime.Now;
                cropCultivationDetailEntity.DeleteStatus =false;

                _agriContext.CropCultivationProcessDetails.Add(cropCultivationDetailEntity);
                _agriContext.SaveChanges();

                message = "CropCultivation Added Succesfully ";
            }
            return message;
        }

        public bool Put(CropCultivationProcessDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropCultivationProcessDetailId = Convert.ToInt32(model.CropCultivationProcessDetailId);
            var cropCultivationDetailEntity = _agriContext.CropCultivationProcessDetails.FirstOrDefault(x => x.CropCultivationProcessDetailId == CropCultivationProcessDetailId && x.DeleteStatus==false);
            if (cropCultivationDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                cropCultivationDetailEntity.CropCultivationProcessDetailId = model.CropCultivationProcessDetailId;
                cropCultivationDetailEntity.CropCultivationProcessId = model.CropCultivationProcessId;
                cropCultivationDetailEntity.Title = model.Title;
                cropCultivationDetailEntity.CropId = model.CropId;
                cropCultivationDetailEntity.Description = model.Description;
                cropCultivationDetailEntity.SeqNo = model.SeqNo;
                cropCultivationDetailEntity.IsActive = model.IsActive;
                cropCultivationDetailEntity.ChangedBy = model.ChangedBy;
                cropCultivationDetailEntity.ChangedDate = DateTime.Now;
                cropCultivationDetailEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                return true;
            }
        }
        public string Delete(long CropCultivationProcessDetailId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropCultivationProcessDetailModel model = new CropCultivationProcessDetailModel();
            var cropCultivationDetailEntity = _agriContext.CropCultivationProcessDetails.FirstOrDefault(x => x.CropCultivationProcessDetailId == CropCultivationProcessDetailId);
            if (cropCultivationDetailEntity != null)
            {
                cropCultivationDetailEntity.ChangedBy = model.ChangedBy;
                cropCultivationDetailEntity.ChangedDate = DateTime.Now;
                cropCultivationDetailEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}




