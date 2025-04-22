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
    public class MandiMasterService:IMandimasterService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MandiMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<MandiMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var MandiMasterModelList = new List<MandiMasterModel>();
            var MandiMasterModelListEntity = (from MandiMaster in _agriContext.MandiMasters
                                              join CityMaster in _agriContext.CityMasters
                                              on MandiMaster.CityId equals CityMaster.CityId
                                              where MandiMaster.DeleteStatus == false

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
                model.MandiName = item.MandiName;
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

       

        public string Add(MandiMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.MandiMasters.Any(x => x.MandiId == model.MandiId);
                if (existing)
                {
                    message = GlobalConstants.NotFoundMessage;
                }
                else
                {
                    var MandiMasterEntity = new MandiMaster();
                    MandiMasterEntity.MandiId = Convert.ToInt32(model.MandiId);
                    MandiMasterEntity.CityId = model.CityId;
                    MandiMasterEntity.MandiName = model.MandiName;
                    MandiMasterEntity.Address = model.Address;
                    MandiMasterEntity.PhoneNo = model.PhoneNo;
                    MandiMasterEntity.Website = model.Website;
                    MandiMasterEntity.IsWebApi = model.IsWebApi;
                    MandiMasterEntity.IsActive = model.IsActive;
                    MandiMasterEntity.EnteredBy = model.EnteredBy;
                    MandiMasterEntity.EnteredDate = DateTime.Now;
                    MandiMasterEntity.DeleteStatus = model.DeleteStatus;

                    _agriContext.MandiMasters.Add(MandiMasterEntity);
                    _agriContext.SaveChanges();

                    message = " MandiMaster Added Succesfully ";
                }
                return message;
            }

        }

        MandiMasterModel IMandimasterService.GetById(long MandiId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var MandiMasterEntity = (from MandiMaster in _agriContext.MandiMasters
                                     join CityMaster in _agriContext.CityMasters
                                     on MandiMaster.CityId equals CityMaster.CityId
                                     where MandiMaster.DeleteStatus == false && MandiMaster.MandiId==MandiId

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
                                  ).FirstOrDefault();
            if (MandiMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new MandiMasterModel
            {
                MandiId = MandiMasterEntity.MandiId,
                MandiName = MandiMasterEntity.MandiName,
                CityId = MandiMasterEntity.CityId,
                Address=MandiMasterEntity.Address,
                PhoneNo = MandiMasterEntity.PhoneNo,
                Website = MandiMasterEntity.Website,
                IsWebApi = MandiMasterEntity.IsWebApi,
                IsActive = MandiMasterEntity.IsActive,
                EnteredBy = MandiMasterEntity.EnteredBy,
                EnteredDate = MandiMasterEntity.EnteredDate,
                ChangedBy = MandiMasterEntity.ChangedBy,
                ChangedDate = MandiMasterEntity.ChangedDate,
                DeleteStatus = MandiMasterEntity.DeleteStatus,
                CityName = MandiMasterEntity.CityName,

            };

        }


        public bool Put(MandiMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var MandiId = Convert.ToInt32(model.MandiId);
            var MandiMasterEntity = _agriContext.MandiMasters.FirstOrDefault(x => x.MandiId == MandiId && !x.DeleteStatus);
            if (MandiMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }

            else
            {

                MandiMasterEntity.MandiId = Convert.ToInt32(model.MandiId);
                MandiMasterEntity.CityId = model.CityId;
                MandiMasterEntity.MandiName = model.MandiName;
                MandiMasterEntity.Address = model.Address;
                MandiMasterEntity.PhoneNo = model.PhoneNo;
                MandiMasterEntity.Website = model.Website;
                MandiMasterEntity.IsWebApi = model.IsWebApi;
                MandiMasterEntity.IsActive = model.IsActive;
                MandiMasterEntity.ChangedBy = model.ChangedBy;
                MandiMasterEntity.ChangedDate = DateTime.Now;
                MandiMasterEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long MandiId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            MandiMasterModel model = new MandiMasterModel();
            var mandiEntity = _agriContext.MandiMasters.FirstOrDefault(x => x.MandiId == MandiId);
            if (mandiEntity != null)
            {
                mandiEntity.DeleteStatus = true;
                mandiEntity.ChangedBy = model.ChangedBy;
                mandiEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public List<AgriProductMasterModel> GetAgriProduct(long AgriProductId)
        {
            throw new NotImplementedException();
        }

        //        public List<AgriProductMasterModel> GetAgriProduct(long AgriProductId)
        //        {
        //            var errorResponseModel = new ErrorResponseModel();
        //            var agriProdcutMasterModelList = new List<AgriProductMasterModel>();
        //            var agriProdcutMasterListEntity = (from agriProduct in _agriContext.AgriProductMasters
        //                                               join agriProductType in _agriContext.AgriProductTypeMasters
        //                                               on agriProduct.AgriProductTypeId equals agriProductType.AgriProductTypeId
        //                                               join mandi in _agriContext.MandiMasters
        //                                               on agriProduct.AgriProductId equals mandi.Agr
        //                                               where agriProduct.DeleteStatus == false
        //                                               select new
        //                                               {
        //                                                   agriProduct.AgriProductId,
        //                                                   agriProduct.AgriProductName,
        //                                                   agriProduct.AgriProductCode,
        //                                                   agriProduct.EnteredBy,
        //                                                   agriProduct.EnteredDate,
        //                                                   agriProduct.ChangedBy,
        //                                                   agriProduct.ChangedDate,
        //                                                   agriProductType.AgriProductTypeId,
        //                                                   agriProductType.AgriProductTypeName
        //                                               }).ToList();
        //            if (agriProdcutMasterListEntity == null)
        //            {
        //                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
        //                return null;
        //            }
        //            foreach (var item in agriProdcutMasterListEntity)
        //            {
        //                var model = new AgriProductMasterModel();
        //                model.AgriProductId = item.AgriProductId;
        //                model.AgriProductName = item.AgriProductName;
        //                model.AgriProductTypeId = item.AgriProductTypeId;
        //                model.AgriProductTypeName = item.AgriProductTypeName;
        //                model.AgriProductCode = item.AgriProductCode;
        //                model.EnteredBy = item.EnteredBy;
        //                model.EnteredDate = item.EnteredDate;
        //                model.ChangedBy = item.ChangedBy;
        //                model.ChangedDate = item.ChangedDate;
        //                agriProdcutMasterModelList.Add(model);
        //            }
        //            return agriProdcutMasterModelList;
        //        }

    }
}

