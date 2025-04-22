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
   public class NutrientsCategoryService: INutrientsCategoryService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public NutrientsCategoryService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public string Add(NutrientsCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCategory = _agriContext.NutrientsCategoryMasters.Any(x => x.NutrientsCategoryId == model.NutrientsCategoryId);
            if (existingCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                NutrientsCategoryMaster nutrientsCategoryMasterEntity = new NutrientsCategoryMaster();
                nutrientsCategoryMasterEntity.NutrientsCategoryId = model.NutrientsCategoryId;
                nutrientsCategoryMasterEntity.NutrientsCategoryName = model.NutrientsCategoryName;
                nutrientsCategoryMasterEntity.Description = model.Description;
                _agriContext.NutrientsCategoryMasters.Add(nutrientsCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public List<NutrientsCategoryModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var NutrientsCategoryModelList = new List<NutrientsCategoryModel>();
            var NutrientsCategoryMasterListEntity = _agriContext.NutrientsCategoryMasters.ToList();
            if (NutrientsCategoryMasterListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in NutrientsCategoryMasterListEntity)
            {
                var model = new NutrientsCategoryModel();
                model.NutrientsCategoryId = item.NutrientsCategoryId;
                model.NutrientsCategoryName = item.NutrientsCategoryName;
                model.Description = item.Description;
                NutrientsCategoryModelList.Add(model);
            }
            return NutrientsCategoryModelList;
        }

        public NutrientsCategoryModel GetById(long NutrientsCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var nutrientsCategoryMasterEntity = _agriContext.NutrientsCategoryMasters.FirstOrDefault(x => x.NutrientsCategoryId == NutrientsCategoryId );
            if (nutrientsCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new NutrientsCategoryModel
            {
                NutrientsCategoryId = nutrientsCategoryMasterEntity.NutrientsCategoryId,
                NutrientsCategoryName = nutrientsCategoryMasterEntity.NutrientsCategoryName,
                Description = nutrientsCategoryMasterEntity.Description,
                       };
        }

        public bool Update(NutrientsCategoryModel model, ref ErrorResponseModel errorResponseModel)
        {
            var NutrientsCategoryId = model.NutrientsCategoryId;
            var nutrientsCategoryMasterEntity = _agriContext.NutrientsCategoryMasters.FirstOrDefault(x => x.NutrientsCategoryId == NutrientsCategoryId);
            if (nutrientsCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                nutrientsCategoryMasterEntity.NutrientsCategoryName = model.NutrientsCategoryName;
                nutrientsCategoryMasterEntity.Description = model.Description;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long NutrientsCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            var nutrientsCategoryMasterEntity = _agriContext.NutrientsCategoryMasters.FirstOrDefault(x => x.NutrientsCategoryId == NutrientsCategoryId);
            if (nutrientsCategoryMasterEntity != null)
            {
                _agriContext.Remove(nutrientsCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}

