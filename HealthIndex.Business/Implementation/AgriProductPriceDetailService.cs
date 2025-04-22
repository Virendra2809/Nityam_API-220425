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
   public class AgriProductPriceDetailService:IAgriProductPriceDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AgriProductPriceDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<AgriProductPriceDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var userExist = _agriContext.AgriProductPriceDetails.FirstOrDefault(x => x.DeleteStatus == false);

            var agriProductPriceModelList = new List<AgriProductPriceDetailModel>();
            var agriProductPriceModelEntity = (from AgriProductPriceDetail in _agriContext.AgriProductPriceDetails

                                               join MandiMaster in _agriContext.MandiMasters
                                               on AgriProductPriceDetail.MandiId equals MandiMaster.MandiId
                                               //where MandiMaster.DeleteStatus == false
                                               join AgriProductMaster in _agriContext.AgriProductMasters
                                               on AgriProductPriceDetail.AgriProductId equals AgriProductMaster.AgriProductId
                                               //where  AgriProductMaster.DeleteStatus == false
                                               join AgriProductTypeMaster in _agriContext.AgriProductTypeMasters
                                               on AgriProductMaster.AgriProductTypeId equals AgriProductTypeMaster.AgriProductTypeId
                                               //where AgriProductMaster.DeleteStatus == false
                                               join UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                               on AgriProductPriceDetail.UnitId equals UnitofMeasurementMaster.UnitId
                                               where AgriProductPriceDetail.DeleteStatus == false
                                               select new
                                               {
                                                   AgriProductPriceDetail.AgriProductPriceId,
                                                   AgriProductMaster.AgriProductId,
                                                   AgriProductMaster.AgriProductName,
                                                   AgriProductMaster.AgriProductTypeId,
                                                   AgriProductTypeMaster.AgriProductTypeName,
                                                   MandiMaster.MandiId,
                                                   MandiMaster.MandiName,
                                                   AgriProductPriceDetail.UnitId,
                                                   UnitofMeasurementMaster.UnitName,
                                                   AgriProductPriceDetail.Date,
                                                   AgriProductPriceDetail.AverageRate,
                                                   AgriProductPriceDetail.MaxRate,
                                                   AgriProductPriceDetail.MinRate,
                                                   AgriProductPriceDetail.Outward,
                                                   AgriProductPriceDetail.Inward,
                                                   AgriProductPriceDetail.EnteredBy,
                                                   AgriProductPriceDetail.EnteredDate,
                                                   AgriProductPriceDetail.ChangedBy,
                                                   AgriProductPriceDetail.ChangedDate,
                                                   AgriProductPriceDetail.DeleteStatus,
                                               }).ToList();

            if (agriProductPriceModelEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in agriProductPriceModelEntity)
            {
                var model = new AgriProductPriceDetailModel();
                model.AgriProductPriceId = item.AgriProductPriceId;
                model.AgriProductId = item.AgriProductId;
                model.AgriProductTypeId = item.AgriProductTypeId;
                model.AgriProductTypeName = item.AgriProductTypeName;
                model.AgriProductName = item.AgriProductName;
                model.MandiName = item.MandiName;
                model.MandiId = item.MandiId;
                model.UnitId = item.UnitId;
                model.UnitName = item.UnitName;
                model.Date = (DateTime)item.Date;
                model.AverageRate = item.AverageRate;
                model.MinRate = item.MinRate;
                model.MaxRate = item.MaxRate;
                model.Inward = item.Inward;
                model.Outward = item.Outward;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                //model.UnitName = item.UnitName;
                agriProductPriceModelList.Add(model);
            }
            return agriProductPriceModelList;
        }

       
        public string Add(AgriProductPriceDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            {
                var message = string.Empty;
                var existing = _agriContext.AgriProductPriceDetails.Any(x => x.AgriProductId == model.AgriProductId && x.MandiId==model.MandiId && x.Date==model.Date);
                if (existing)
                {
                    message = "Mandi Product Already Exist";
                }
                else
                {
                    var agriProductPriceModelEntity = new AgriProductPriceDetail();
                    agriProductPriceModelEntity.AgriProductPriceId = model.AgriProductPriceId;
                    agriProductPriceModelEntity.AgriProductId = model.AgriProductId;
                    agriProductPriceModelEntity.MandiId = model.MandiId;
                    agriProductPriceModelEntity.UnitId = model.UnitId;
                    agriProductPriceModelEntity.Date = Convert.ToDateTime(model.Date);
                    agriProductPriceModelEntity.Inward = model.Inward;
                    agriProductPriceModelEntity.Outward = model.Outward;
                    agriProductPriceModelEntity.MaxRate = model.MaxRate;
                    agriProductPriceModelEntity.MinRate = model.MinRate;
                    agriProductPriceModelEntity.AverageRate = model.AverageRate;
                    agriProductPriceModelEntity.EnteredBy = model.EnteredBy;
                    agriProductPriceModelEntity.EnteredDate = DateTime.Now;
                    agriProductPriceModelEntity.DeleteStatus = false;
                    _agriContext.AgriProductPriceDetails.Add(agriProductPriceModelEntity);
                    _agriContext.SaveChanges();
                    message = "Data Added Succesfully ";
                }
                return message;
            }

        }
        AgriProductPriceDetailModel IAgriProductPriceDetailService.GetById(long AgriProductPriceId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var agriProductPriceModelEntity = (from AgriProductPriceDetail in _agriContext.AgriProductPriceDetails
                                               join MandiMaster in _agriContext.MandiMasters
                                               on AgriProductPriceDetail.MandiId equals MandiMaster.MandiId
                                               where AgriProductPriceDetail.DeleteStatus == false && AgriProductPriceDetail.AgriProductPriceId==AgriProductPriceId

                                               join AgriProductMaster in _agriContext.AgriProductMasters
                                               on AgriProductPriceDetail.AgriProductId equals AgriProductMaster.AgriProductId
                                               where AgriProductPriceDetail.DeleteStatus == false && AgriProductPriceDetail.AgriProductPriceId == AgriProductPriceId

                                               join UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                               on AgriProductPriceDetail.UnitId equals UnitofMeasurementMaster.UnitId
                                               where AgriProductPriceDetail.DeleteStatus == false && AgriProductPriceDetail.AgriProductPriceId == AgriProductPriceId


                                               select new
                                               {
                                                   AgriProductPriceDetail.AgriProductPriceId,
                                                   AgriProductMaster.AgriProductId,
                                                   AgriProductMaster.AgriProductName,
                                                   MandiMaster.MandiId,
                                                   MandiMaster.MandiName,
                                                   UnitofMeasurementMaster.UnitId,
                                                   UnitofMeasurementMaster.UnitName,
                                                   AgriProductPriceDetail.Date,
                                                   AgriProductPriceDetail.AverageRate,
                                                   AgriProductPriceDetail.MaxRate,
                                                   AgriProductPriceDetail.MinRate,
                                                   AgriProductPriceDetail.Outward,
                                                   AgriProductPriceDetail.Inward,
                                                   AgriProductPriceDetail.EnteredBy,
                                                   AgriProductPriceDetail.EnteredDate,
                                                   AgriProductPriceDetail.ChangedBy,
                                                   AgriProductPriceDetail.ChangedDate,
                                                   AgriProductPriceDetail.DeleteStatus,

                                               }
                                               ).FirstOrDefault();
            if (agriProductPriceModelEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            string date = agriProductPriceModelEntity.Date.ToString();
          
            return new AgriProductPriceDetailModel
            {
                AgriProductPriceId = agriProductPriceModelEntity.AgriProductPriceId,
                AgriProductId = agriProductPriceModelEntity.AgriProductId,
                AgriProductName = agriProductPriceModelEntity.AgriProductName,
                MandiId = agriProductPriceModelEntity.MandiId,
                MandiName = agriProductPriceModelEntity.MandiName,
                UnitId = agriProductPriceModelEntity.UnitId,
                UnitName = agriProductPriceModelEntity.UnitName,
                Date = (DateTime)agriProductPriceModelEntity.Date,
                AverageRate = agriProductPriceModelEntity.AverageRate,
                MaxRate = agriProductPriceModelEntity.MaxRate,
                MinRate = agriProductPriceModelEntity.MinRate,
                Outward = agriProductPriceModelEntity.Outward,
                Inward = agriProductPriceModelEntity.Inward,
                EnteredBy = agriProductPriceModelEntity.EnteredBy,
                EnteredDate = agriProductPriceModelEntity.EnteredDate,
                ChangedBy = agriProductPriceModelEntity.ChangedBy,
                ChangedDate = agriProductPriceModelEntity.ChangedDate,
                DeleteStatus = agriProductPriceModelEntity.DeleteStatus,
             
            };

        }


        public bool Put(AgriProductPriceDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var AgriProductPriceId = Convert.ToInt32(model.AgriProductPriceId);
            var agriProductPriceModelEntity = _agriContext.AgriProductPriceDetails.FirstOrDefault(x => x.AgriProductPriceId == AgriProductPriceId);
            if (agriProductPriceModelEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                agriProductPriceModelEntity.AgriProductPriceId = model.AgriProductPriceId;
                agriProductPriceModelEntity.AgriProductId = model.AgriProductId;
                agriProductPriceModelEntity.MandiId = model.MandiId;
                agriProductPriceModelEntity.UnitId = model.UnitId;
                agriProductPriceModelEntity.Date = Convert.ToDateTime(model.Date);
                agriProductPriceModelEntity.Inward = model.Inward;
                agriProductPriceModelEntity.Outward = model.Outward;
                agriProductPriceModelEntity.MaxRate = model.MaxRate;
                agriProductPriceModelEntity.MinRate = model.MinRate;
                agriProductPriceModelEntity.AverageRate = model.AverageRate;
                agriProductPriceModelEntity.ChangedBy = model.ChangedBy;
                agriProductPriceModelEntity.ChangedDate = DateTime.Now;
                agriProductPriceModelEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long AgriProductPriceId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            AgriProductPriceDetailModel model = new AgriProductPriceDetailModel();
            var agriProductPriceModelEntity = _agriContext.AgriProductPriceDetails.FirstOrDefault(x => x.AgriProductPriceId == AgriProductPriceId);
            if (agriProductPriceModelEntity != null)
            {
                agriProductPriceModelEntity.DeleteStatus = true;
                agriProductPriceModelEntity.ChangedBy = model.ChangedBy;
                agriProductPriceModelEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }



}


