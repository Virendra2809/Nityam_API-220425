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
   public class SeedCropSpacingService: ISeedCropSpacingService
    {

        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public SeedCropSpacingService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }
        public List<SeedCropSpacingModel> GetAllSeedCropSpacing()
        {
            var errorResponseModel = new ErrorResponseModel();
            var seedCropSpacingMasterModelList = new List<SeedCropSpacingModel>();
            var seedCropSpacingModelListEntity = (from SeedCropSpacing in _agriContext.SeedCropSpacings
                                                 join SeedCropMaster in _agriContext.SeedCropMasters
                                                 on SeedCropSpacing.SeedCropId equals SeedCropMaster.SeedCropId
                                                 select new
                                                 {
                                                     SeedCropSpacing.SeedCropSpacingId,
                                                     SeedCropMaster.SeedCropId,
                                                     SeedCropMaster.SeedCropName,
                                                     SeedCropSpacing.RowSpacing,
                                                     SeedCropSpacing.PlantSpacing,
                                                     SeedCropSpacing.PlantPopulatin,

                                                 }).ToList();
            if (seedCropSpacingModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in seedCropSpacingModelListEntity)
            {
                var model = new SeedCropSpacingModel();
                model.SeedCropSpacingId = item.SeedCropSpacingId;
                model.SeedCropId = item.SeedCropId;
                model.SeedCropName = item.SeedCropName;
                model.RowSpacing = item.RowSpacing;
                model.PlantSpacing = item.PlantSpacing;
                model.PlantPopulatin = item.PlantPopulatin;

                seedCropSpacingMasterModelList.Add(model);
            }
            return seedCropSpacingMasterModelList;
        }

        public SeedCropSpacingModel GetSeedCropSpacingById(long SeedCropSpacingId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var seedCropSpacingEntity = (from SeedCropSpacing in _agriContext.SeedCropSpacings
                                         join SeedCropMaster in _agriContext.SeedCropMasters
                                         on SeedCropSpacing.SeedCropId equals SeedCropMaster.SeedCropId

                                         where SeedCropSpacing.SeedCropSpacingId == SeedCropSpacingId 
                                        select new
                                        {
                                            SeedCropSpacing.SeedCropSpacingId,
                                            SeedCropMaster.SeedCropId,
                                            SeedCropMaster.SeedCropName,
                                            SeedCropSpacing.RowSpacing,
                                            SeedCropSpacing.PlantSpacing,
                                            SeedCropSpacing.PlantPopulatin,


                                        }).FirstOrDefault();
            if (seedCropSpacingEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SeedCropSpacingModel
            {
                SeedCropSpacingId = seedCropSpacingEntity.SeedCropSpacingId,
                SeedCropId = seedCropSpacingEntity.SeedCropId,
                SeedCropName = seedCropSpacingEntity.SeedCropName,
                RowSpacing = seedCropSpacingEntity.RowSpacing,
                PlantSpacing = seedCropSpacingEntity.PlantSpacing,
                PlantPopulatin = seedCropSpacingEntity.PlantPopulatin,
               
            };
        }

        public string AddSeedCropSpacing(SeedCropSpacingModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingSeedCropSpacingCategory = _agriContext.SeedCropSpacings.Any(x => x.SeedCropSpacingId == model.SeedCropSpacingId);
            if (existingSeedCropSpacingCategory)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                SeedCropSpacing seedCropspacingEntity = new SeedCropSpacing();
                seedCropspacingEntity.SeedCropSpacingId = model.SeedCropSpacingId;
                seedCropspacingEntity.SeedCropId = model.SeedCropId;
                seedCropspacingEntity.RowSpacing = Convert.ToDecimal(model.RowSpacing);
                seedCropspacingEntity.PlantSpacing = Convert.ToDecimal(model.PlantSpacing);
                seedCropspacingEntity.PlantPopulatin = Convert.ToDecimal(model.PlantPopulatin);
                _agriContext.SeedCropSpacings.Add(seedCropspacingEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateSeedCropSpacing(SeedCropSpacingModel model, ref ErrorResponseModel errorResponseModel)
        {
            var SeedCropSpacingId = model.SeedCropSpacingId;
            var seedCropspacingEntity = _agriContext.SeedCropSpacings.FirstOrDefault(x => x.SeedCropSpacingId == SeedCropSpacingId);
            if (seedCropspacingEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seedCropspacingEntity.SeedCropSpacingId = model.SeedCropSpacingId;
                seedCropspacingEntity.SeedCropId = model.SeedCropId;
                seedCropspacingEntity.RowSpacing = Convert.ToDecimal(model.RowSpacing);
                seedCropspacingEntity.PlantSpacing = Convert.ToDecimal(model.PlantSpacing);
                seedCropspacingEntity.PlantPopulatin = Convert.ToDecimal(model.PlantPopulatin);

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteSeedCropSpacing(long SeedCropSpacingId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
           // SeedCropSpacingModel model = new SeedCropSpacingModel();
            var seedCropSpacingEntity = _agriContext.SeedCropSpacings.FirstOrDefault(x => x.SeedCropSpacingId == SeedCropSpacingId);
            if (seedCropSpacingEntity != null)
            {
               // seedCropSpacingEntity.SeedCropSpacingId = model.SeedCropSpacingId;

                _agriContext.Remove(seedCropSpacingEntity);
                _agriContext.SaveChanges();

                message = "Deleted Successfully";
            }
            return message;
        }

       
    }
}

