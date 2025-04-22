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
   public class CropIrrigationService:ICropIrrigationService
    {
        
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CropIrrigationService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<CropIrrigationDetailModel> GetAllCropIrrigation()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropIrrigationList = new List<CropIrrigationDetailModel>();
            var CropIrrigationListEntity = (from CropIrrigationDetail in _agriContext.CropIrrigationDetails
                                             join Crop in _agriContext.CropMasters
                                             on CropIrrigationDetail.CropId equals Crop.CropId

                                            join Soil in _agriContext.SoilTypeMasters
                                            on CropIrrigationDetail.SoilTypeId equals Soil.SoilTypeId
                                          
                                            join Cropstage in _agriContext.CropStageMasters
                                            on CropIrrigationDetail.CropStageId equals Cropstage.CropStageId
                                            where CropIrrigationDetail.DeleteStatus == false

                                              select new
                                                  {

                                                          CropIrrigationDetail.IrrigationDetailId,
                                                          CropIrrigationDetail.IrrigationDay,
                                                          Crop.CropId,
                                                          Crop.CropName,
                                                          Soil.SoilTypeId,
                                                          Soil.SoilTypeName,
                                                          Cropstage.CropStageId,
                                                          Cropstage.CropStageName,
                                                          CropIrrigationDetail.DeleteStatus,
                                                     
                                                   }
                                  ).ToList();
            if (CropIrrigationListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropIrrigationListEntity)
            {
                var model = new CropIrrigationDetailModel();
                model.IrrigationDetailId = item.IrrigationDetailId;
                model.IrrigationDay1 =item.IrrigationDay;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SoilTypeId = item.SoilTypeId;
                model.SoilTypeName = item.SoilTypeName;
                model.CropStageId = item.CropStageId;
                model.DeleteStatus = item.DeleteStatus;
                model.CropStageName = item.CropStageName;

                CropIrrigationList.Add(model);
            }
            return CropIrrigationList;
        }


        public string AddCropIrrigation(CropIrrigationDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;
                int[] arr = model.IrrigationDay;
                var t2 = arr.Length;
                foreach (var i in arr)
                {

                    var existing = _agriContext.CropIrrigationDetails.Any(x => x.IrrigationDetailId == model.IrrigationDetailId);
                    if (existing)
                    {
                        message = GlobalConstants.NotFoundMessage;
                    }

                    else
                    {

                        var CropstageEntity = new CropIrrigationDetail();

                        CropstageEntity.IrrigationDetailId = Convert.ToInt32(model.IrrigationDetailId);
                        CropstageEntity.CropId = model.CropId;
                        CropstageEntity.SoilTypeId = model.SoilTypeId;
                        CropstageEntity.CropStageId = model.CropStageId;
                        CropstageEntity.IrrigationDay = i;
                        CropstageEntity.DeleteStatus = false;

                        _agriContext.CropIrrigationDetails.Add(CropstageEntity);
                        _agriContext.SaveChanges();



                        message = "Data Added Succesfully ";
                    }
                }
                return message;
            }

        }

        irrigationModel ICropIrrigationService.GetById(long CropId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var CropIrrigationListEntity = (from CropIrrigationDetail in _agriContext.CropIrrigationDetails
                                            join Crop in _agriContext.CropMasters
                                            on CropIrrigationDetail.CropId equals Crop.CropId

                                            join Soil in _agriContext.SoilTypeMasters
                                            on CropIrrigationDetail.SoilTypeId equals Soil.SoilTypeId

                                            join Cropstage in _agriContext.CropStageMasters
                                             on CropIrrigationDetail.CropStageId equals Cropstage.CropStageId


                                            where CropIrrigationDetail.DeleteStatus == false && CropIrrigationDetail.CropId== CropId

                                            select new
                                            {
                                                CropIrrigationDetail.IrrigationDetailId,
                                                CropIrrigationDetail.IrrigationDay,
                                                Crop.CropId,
                                                Crop.CropName,
                                                Soil.SoilTypeId,
                                                Soil.SoilTypeName,
                                                Cropstage.CropStageId,
                                                Cropstage.CropStageName,
                                              
                                            }
                                 ).FirstOrDefault();
            if (CropIrrigationListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            var model = new irrigationModel();
            model.CropId = CropIrrigationListEntity.CropId;
            model.SoilTypeId = CropIrrigationListEntity.SoilTypeId;
            model.CropStageId = CropIrrigationListEntity.CropStageId;
            model.CropName = CropIrrigationListEntity.CropName;
            model.SoilTypeName = CropIrrigationListEntity.SoilTypeName;
            model.CropStageName = CropIrrigationListEntity.CropStageName;
                var DayList = _agriContext.CropIrrigationDetails
                                  .Where(x => x.CropId == CropIrrigationListEntity.CropId && x.DeleteStatus==false).ToList();
            foreach (var days in DayList)
            {
                var daymodel = new IrrigationDayModel();
                daymodel.IrrigationDay = days.IrrigationDay;
                model.IrrigationDays.Add(daymodel);
            }
            if (CropIrrigationListEntity == null)
            {
                return null;
            }
            else
            {
                return model;
            }
        }


        public bool Put(CropIrrigationDetailModel model, ref ErrorResponseModel errorResponseModel)
        {

            // var CropId = Convert.ToInt32(model.CropId);
            var IrrigationDetailId = model.IrrigationDetailId;
           // int[] arr = model.IrrigationDay;
            
           
            
               
            //foreach (var i in IrrigationDetailId)
            //{
                var CropstageEntity = _agriContext.CropIrrigationDetails.FirstOrDefault(x => /*x.CropId == CropId &&*/ x.DeleteStatus == false && x.IrrigationDetailId == IrrigationDetailId);
                if (CropstageEntity == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                    return false;
                }
                else
                {

                    //CropstageEntity.IrrigationDetailId = Convert.ToInt32(model.IrrigationDetailId);
                    CropstageEntity.CropId = model.CropId;
                    CropstageEntity.CropStageId = model.CropStageId;
                    CropstageEntity.SoilTypeId = model.SoilTypeId;
                    CropstageEntity.IrrigationDay = model.IrrigationDay1;
                    CropstageEntity.DeleteStatus = false;
                    _agriContext.SaveChanges();
                }

           // }
            return true;

        }

        CropIrrigationDetailModel ICropIrrigationService.GetByIdirrigationId(long IrrigationDetailId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var CropIrrigationListEntity = (from CropIrrigationDetail in _agriContext.CropIrrigationDetails
                                            join Crop in _agriContext.CropMasters
                                            on CropIrrigationDetail.CropId equals Crop.CropId

                                            join Soil in _agriContext.SoilTypeMasters
                                            on CropIrrigationDetail.SoilTypeId equals Soil.SoilTypeId

                                            join Cropstage in _agriContext.CropStageMasters
                                             on CropIrrigationDetail.CropStageId equals Cropstage.CropStageId


                                            where CropIrrigationDetail.DeleteStatus == false && CropIrrigationDetail.IrrigationDetailId == IrrigationDetailId

                                            select new
                                            {
                                                CropIrrigationDetail.IrrigationDetailId,
                                                CropIrrigationDetail.IrrigationDay,
                                                Crop.CropId,
                                                Crop.CropName,
                                                Soil.SoilTypeId,
                                                Soil.SoilTypeName,
                                                Cropstage.CropStageId,
                                                Cropstage.CropStageName,

                                            }
                                 ).FirstOrDefault();
            if (CropIrrigationListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropIrrigationDetailModel
            {
                IrrigationDetailId = CropIrrigationListEntity.IrrigationDetailId,
                IrrigationDay1 = CropIrrigationListEntity.IrrigationDay,
                CropId = CropIrrigationListEntity.CropId,
                SoilTypeId = CropIrrigationListEntity.SoilTypeId,
                CropStageId = CropIrrigationListEntity.CropStageId,
                CropName = CropIrrigationListEntity.CropName,
                SoilTypeName = CropIrrigationListEntity.SoilTypeName,
                CropStageName = CropIrrigationListEntity.CropStageName,

            };

        }


        public string Delete(long IrrigationDetailId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropIrrigationDetailModel model = new CropIrrigationDetailModel();
            var CropstageEntity = _agriContext.CropIrrigationDetails.FirstOrDefault(x => x.IrrigationDetailId == IrrigationDetailId);
            if (CropstageEntity != null)
            {
                CropstageEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


        public List<CropStageModel> GetAllCropstage(int CropId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropstageModelList = new List<CropStageModel>();
            var CropstageListEntity = (from CropStageMaster in _agriContext.CropStageMasters
                                       join Crop in _agriContext.CropMasters
                                       on CropStageMaster.CropId equals Crop.CropId
                                       where CropStageMaster.DeleteStatus == false && CropStageMaster.CropId==CropId
                                       select new
                                       {
                                           CropStageMaster.CropStageId,
                                           CropStageMaster.CropStageName,
                                           Crop.CropId,
                                           Crop.CropName,
                                           CropStageMaster.SeqNo,
                                           CropStageMaster.DeleteStatus,
                                       }
                                  ).ToList();
            if (CropstageListEntity == null)
            {
                return null;
            }
            foreach (var item in CropstageListEntity)
            {
                var model = new CropStageModel();
                model.CropStageId = item.CropStageId;
                model.CropStageName = item.CropStageName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeqNo = item.SeqNo;
                model.DeleteStatus = item.DeleteStatus;

                CropstageModelList.Add(model);
            }
            return CropstageModelList;
        }


    }

}


