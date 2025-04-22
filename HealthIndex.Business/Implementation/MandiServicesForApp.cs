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
   public class MandiServicesForApp : IMandiForMobileServices
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MandiServicesForApp(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }
        public List<MandiMasterModel> GetAllMandiWithCity(int CityId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var MandiMasterModelList = new List<MandiMasterModel>();
            var MandiMasterModelListEntity = (from MandiMaster in _agriContext.MandiMasters
                                              join CityMaster in _agriContext.CityMasters
                                              on MandiMaster.CityId equals CityMaster.CityId
                                              where MandiMaster.DeleteStatus == false && MandiMaster.CityId == CityId
                                              select new
                                              {
                                                  MandiMaster.MandiId,
                                                  MandiMaster.MandiName,
                                                  CityMaster.CityId,
                                                  MandiMaster.Address,
                                                  MandiMaster.PhoneNo,
                                                  MandiMaster.Website,
                                                  MandiMaster.IsWebApi,
                                                  MandiMaster.IsActive,
                                                  MandiMaster.ChangedBy,
                                                  MandiMaster.ChangedDate,
                                                  MandiMaster.EnteredBy,
                                                  MandiMaster.EnteredDate,
                                                  MandiMaster.DeleteStatus,
                                                  CityMaster.CityName,

                                              }
                                  ).ToList();
            if (MandiMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in MandiMasterModelListEntity)
            {
                var model = new MandiMasterModel();
                model.MandiId = item.MandiId;
                model.MandiName = item.MandiName + " " + (item.CityName);
                model.CityId = item.CityId;
                model.Address = item.Address;
                model.PhoneNo = item.PhoneNo;
                model.Website = item.Website;
                model.IsActive = item.IsActive;
                model.IsWebApi = item.IsWebApi;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.CityName = item.CityName;

                MandiMasterModelList.Add(model);
            }
            return MandiMasterModelList;
        }
        public List<AgriProductPriceDetailModel> GetAllMandi(long MandiId, long AgriProductId, DateTime Date)
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
                                               //join AgriProductTypeMaster in _agriContext.AgriProductTypeMasters
                                               //on AgriProductMaster.AgriProductTypeId equals AgriProductTypeMaster.AgriProductTypeId
                                               ////where AgriProductMaster.DeleteStatus == false
                                               join UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                               on AgriProductPriceDetail.UnitId equals UnitofMeasurementMaster.UnitId
                                               where ((AgriProductPriceDetail.DeleteStatus == false)
                                               && (AgriProductId != 0  && Date != DateTime.Parse("01-01-0001") ? (AgriProductPriceDetail.MandiId == MandiId && AgriProductPriceDetail.AgriProductId == AgriProductId  && AgriProductPriceDetail.Date == Date) :
                                             //  (AgriProductId != 0 ? (AgriProductPriceDetail.MandiId == MandiId && AgriProductPriceDetail.AgriProductId == AgriProductId ) :
                                               (AgriProductId != 0 ? (AgriProductPriceDetail.MandiId == MandiId && AgriProductPriceDetail.AgriProductId == AgriProductId) : (AgriProductPriceDetail.MandiId == MandiId))))
                                               select new
                                               {
                                                   AgriProductPriceDetail.AgriProductPriceId,
                                                   AgriProductMaster.AgriProductId,
                                                   AgriProductMaster.AgriProductName,
                                                   AgriProductMaster.AgriProductTypeId,
                                                  // AgriProductTypeMaster.AgriProductTypeName,
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
               // model.AgriProductTypeName = item.AgriProductTypeName;
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

        public List<AgriProductMasterModel> GetAllAgriProductMaster(int MandiId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var agriProdcutMasterModelList = new List<AgriProductMasterModel>();
            var agriProdcutMasterListEntity = (from agriProduct in _agriContext.AgriProductMasters

                                               join agriProductpricedetails in _agriContext.AgriProductPriceDetails
                                               on agriProduct.AgriProductId equals agriProductpricedetails.AgriProductId

                                               where agriProductpricedetails.DeleteStatus == false && agriProductpricedetails.MandiId==MandiId
                                               select new
                                               {
                                                   agriProduct.AgriProductId,
                                                   agriProduct.AgriProductName,
                                                   agriProduct.AgriProductCode,
                                                   agriProduct.EnteredBy,
                                                   agriProduct.EnteredDate,
                                                   agriProduct.ChangedBy,
                                                   agriProduct.ChangedDate,
                                                  // agriProductType.AgriProductTypeId,
                                                   //agriProductType.AgriProductTypeName
                                               }).ToList();
            if (agriProdcutMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in agriProdcutMasterListEntity)
            {
                var model = new AgriProductMasterModel();
                model.AgriProductId = item.AgriProductId;
                model.AgriProductName = item.AgriProductName;
               // model.AgriProductTypeId = item.AgriProductTypeId;
                //model.AgriProductTypeName = item.AgriProductTypeName;
                model.AgriProductCode = item.AgriProductCode;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                agriProdcutMasterModelList.Add(model);
            }
            return agriProdcutMasterModelList;
        }

    }
}
