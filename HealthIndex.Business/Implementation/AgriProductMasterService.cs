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
    public class AgriProductMasterService : IAgriProductMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;


        IConfiguration _configuration;

        public AgriProductMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }
        public List<AgriProductMasterModel> GetAllAgriProductMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var agriProdcutMasterModelList = new List<AgriProductMasterModel>();
            var agriProdcutMasterListEntity = (from agriProduct in _agriContext.AgriProductMasters
                                                   join agriProductType in _agriContext.AgriProductTypeMasters
                                                   on agriProduct.AgriProductTypeId equals agriProductType.AgriProductTypeId
                                                   where agriProduct.DeleteStatus == false
                                                   select new
                                                   {
                                                       agriProduct.AgriProductId,
                                                       agriProduct.AgriProductName,
                                                       agriProduct.AgriProductCode,
                                                       agriProduct.EnteredBy,
                                                       agriProduct.EnteredDate,
                                                       agriProduct.ChangedBy,
                                                       agriProduct.ChangedDate,
                                                       agriProductType.AgriProductTypeId,
                                                       agriProductType.AgriProductTypeName
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
                model.AgriProductTypeId = item.AgriProductTypeId;
                model.AgriProductTypeName = item.AgriProductTypeName;
                model.AgriProductCode = item.AgriProductCode;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                agriProdcutMasterModelList.Add(model);
            }
            return agriProdcutMasterModelList;
        }

        public AgriProductMasterModel GetAgriProductMasterById(long AgriProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var agriProdcutMasterModelList = new List<AgriProductMasterModel>();
            var agriProdcutMasterEntity = (from agriProduct in _agriContext.AgriProductMasters
                                               join agriProductType in _agriContext.AgriProductTypeMasters
                                               on agriProduct.AgriProductTypeId equals agriProductType.AgriProductTypeId
                                               where agriProduct.DeleteStatus == false && agriProduct.AgriProductId == AgriProductId
                                               select new
                                               {
                                                   agriProduct.AgriProductId,
                                                   agriProduct.AgriProductName,
                                                   agriProduct.AgriProductCode,
                                                   agriProduct.EnteredBy,
                                                   agriProduct.EnteredDate,
                                                   agriProduct.ChangedBy,
                                                   agriProduct.ChangedDate,
                                                   agriProductType.AgriProductTypeId,
                                                   agriProductType.AgriProductTypeName
                                               }).FirstOrDefault();
            if (agriProdcutMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new AgriProductMasterModel
            {
                AgriProductId = agriProdcutMasterEntity.AgriProductId,
                AgriProductName = agriProdcutMasterEntity.AgriProductName,
                AgriProductTypeId = agriProdcutMasterEntity.AgriProductTypeId,
                AgriProductTypeName = agriProdcutMasterEntity.AgriProductTypeName,
                AgriProductCode=agriProdcutMasterEntity.AgriProductCode,
                EnteredBy = agriProdcutMasterEntity.EnteredBy,
                EnteredDate = agriProdcutMasterEntity.EnteredDate,
                ChangedBy = agriProdcutMasterEntity.ChangedBy,
                ChangedDate = agriProdcutMasterEntity.ChangedDate      
            };
        }

        public string AddAgriProductMaster(AgriProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingAgriProduct = _agriContext.AgriProductMasters.Any(x => x.AgriProductId == model.AgriProductId);
            if (existingAgriProduct)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                AgriProductMaster agriProductMasterEntity = new AgriProductMaster();
                agriProductMasterEntity.AgriProductName = model.AgriProductName;
                agriProductMasterEntity.AgriProductCode = model.AgriProductCode;
                agriProductMasterEntity.AgriProductTypeId = model.AgriProductTypeId;
                agriProductMasterEntity.EnteredBy = model.EnteredBy;
                agriProductMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.AgriProductMasters.Add(agriProductMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateAgriProductMaster(AgriProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var agriProductId = model.AgriProductId;
            var agriProductMasterEntity = _agriContext.AgriProductMasters.FirstOrDefault(x => x.AgriProductId == agriProductId);
            if (agriProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                agriProductMasterEntity.AgriProductName = model.AgriProductName;
                agriProductMasterEntity.AgriProductCode = model.AgriProductCode;
                agriProductMasterEntity.AgriProductTypeId = model.AgriProductTypeId;
                agriProductMasterEntity.ChangedBy = model.ChangedBy;
                agriProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteAgriProductMaster(long AgriProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            AgriProductMasterModel model = new AgriProductMasterModel();
            var agriProductMasterEntity = _agriContext.AgriProductMasters.FirstOrDefault(x => x.AgriProductId == AgriProductId);
            if (agriProductMasterEntity != null)
            {
                agriProductMasterEntity.DeleteStatus = true;
                agriProductMasterEntity.ChangedBy = model.ChangedBy;
                agriProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;

        }
    }
}
