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
    public class NutrientMasterService: INutrientMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public NutrientMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<NutrientMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var nutrientMasterModelList = new List<NutrientMasterModel>();
            var nutrientMasterListEntity = (from NutrientMaster in _agriContext.NutrientMasters
                                                  join NutrientsCategoryMaster in _agriContext.NutrientsCategoryMasters
                                                  on NutrientMaster.NutrientsCategoryId equals NutrientsCategoryMaster.NutrientsCategoryId
                                                  where NutrientMaster.Deletestatus == false
                                                  select new
                                                  {
                                                      NutrientMaster.NutrientId,
                                                      NutrientMaster.NutrientsName,
                                                      NutrientsCategoryMaster.NutrientsCategoryId,
                                                      NutrientsCategoryMaster.NutrientsCategoryName,
                                                      NutrientMaster.Alias,
                                                      NutrientMaster.Percentage,
                                                     
                                                  }).ToList();
            if (nutrientMasterListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in nutrientMasterListEntity)
            {
                var model = new NutrientMasterModel();
                model.NutrientId = item.NutrientId;
                model.NutrientsName = item.NutrientsName;
                model.NutrientsCategoryId = item.NutrientsCategoryId;
                model.NutrientsCategoryName = item.NutrientsCategoryName;
                model.Alias = item.Alias;
                model.Percentage = item.Percentage;
                 nutrientMasterModelList.Add(model);
            }
            return nutrientMasterModelList;
        }

        public NutrientMasterModel GetById(long NutrientId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var nutrientMasterEntity = (from NutrientMaster in _agriContext.NutrientMasters
                                        join NutrientsCategoryMaster in _agriContext.NutrientsCategoryMasters
                                        on NutrientMaster.NutrientsCategoryId equals NutrientsCategoryMaster.NutrientsCategoryId
                                        where NutrientMaster.Deletestatus == false && NutrientMaster.NutrientId==NutrientId
                                        select new
                                        {
                                            NutrientMaster.NutrientId,
                                            NutrientMaster.NutrientsName,
                                            NutrientsCategoryMaster.NutrientsCategoryId,
                                            NutrientsCategoryMaster.NutrientsCategoryName,
                                            NutrientMaster.Alias,
                                            NutrientMaster.Percentage,

                                        }).FirstOrDefault();
            if (nutrientMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            return new NutrientMasterModel
            {
                NutrientId = nutrientMasterEntity.NutrientId,
                NutrientsName = nutrientMasterEntity.NutrientsName,
                NutrientsCategoryId = nutrientMasterEntity.NutrientsCategoryId,
                NutrientsCategoryName = nutrientMasterEntity.NutrientsCategoryName,
                Alias = nutrientMasterEntity.Alias,
                Percentage = nutrientMasterEntity.Percentage,
               
            };
        }

        public string Add(NutrientMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.NutrientMasters.Any(x => x.NutrientId == model.NutrientId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                NutrientMaster nutrientMasterEntity = new NutrientMaster();
                nutrientMasterEntity.NutrientsName = model.NutrientsName;
                nutrientMasterEntity.NutrientsCategoryId = model.NutrientsCategoryId;
                nutrientMasterEntity.Alias = model.Alias;
                nutrientMasterEntity.Percentage = model.Percentage;
                nutrientMasterEntity.Deletestatus = false;
                _agriContext.NutrientMasters.Add(nutrientMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool Update(NutrientMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var NutrientId = model.NutrientId;
            var nutrientMasterEntity = _agriContext.NutrientMasters.FirstOrDefault(x => x.NutrientId == NutrientId);
            if (nutrientMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                nutrientMasterEntity.NutrientId = model.NutrientId;

                nutrientMasterEntity.NutrientsName = model.NutrientsName;
                nutrientMasterEntity.NutrientsCategoryId = model.NutrientsCategoryId;
                nutrientMasterEntity.Alias = model.Alias;
                nutrientMasterEntity.Percentage =model.Percentage;
                nutrientMasterEntity.Deletestatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long NutrientId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            NutrientMasterModel model = new NutrientMasterModel();
            var nutrientMasterEntity = _agriContext.NutrientMasters.FirstOrDefault(x => x.NutrientId == NutrientId);
            if (nutrientMasterEntity != null)
            {
                nutrientMasterEntity.Deletestatus = true;
               
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }
    }
}

