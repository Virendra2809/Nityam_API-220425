using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class SoilTestReportService : ISoilTestReportService
    {

        private AgtonomicsAgriCultureDbContext _agriContext;
        private ConfigurationModel _configuration;
        public object Value => throw new NotImplementedException();
        public SoilTestReportService(AgtonomicsAgriCultureDbContext agriContext, IOptions<ConfigurationModel> hostName)
        {
            _agriContext = agriContext;
            this._configuration = hostName.Value;
        }

        public List<SoilTestReportModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var SoilTestModelList = new List<SoilTestReportModel>();
            var SoilTestListEntity = (from soil in _agriContext.SoilTestReports
                                      join FarmerCropDetail in _agriContext.FarmerCropDetails
                                      on soil.FarmerCropDetailId equals FarmerCropDetail.FarmerCropDetailId
                                      where soil.DeleteStatus == false
                                      select new
                                      {
                                         soil.SoilTestReportId,
                                         soil.FarmerCropDetailId,
                                         soil.Date,
                                         soil.Ph,
                                         soil.Ec,
                                         soil.Nitrogen,
                                         soil.Phosphorus,
                                         soil.Potassium,
                                         soil.Zink,
                                         soil.Iron,
                                         soil.Manganese,
                                         soil.Copper,
                                         soil.Boron,
                                         soil.OcPercentage,
                                         soil.SoilTexture,
                                         soil.DeleteStatus
                                       }
                                  ).ToList();
            if (SoilTestListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            foreach (var item in SoilTestListEntity)
            {

                var model = new SoilTestReportModel();
                model.SoilTestReportId = item.SoilTestReportId;
                model.FarmerCropDetailId = item.FarmerCropDetailId;
                model.Date = item.Date;
                model.Ph = item.Ph;
                model.Ec = item.Ec;
                model.Nitrogen = item.Nitrogen;
                model.Phosphorus = item.Phosphorus;
                model.Potassium = item.Potassium;
                model.Zink = item.Zink;
                model.Iron = item.Iron;
                model.Manganese = item.Manganese;
                model.Copper = item.Copper;
                model.Boron = item.Boron;
                model.OcPercentage = item.OcPercentage;
                model.SoilTexture = item.SoilTexture;
                model.DeleteStatus = item.DeleteStatus;
                SoilTestModelList.Add(model);

            }
            return SoilTestModelList;
        }


        
        SoilTestReportModel ISoilTestReportService.GetById(long SoilTestReportId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
             var SoilTestListEntity = (from soil in _agriContext.SoilTestReports
                                       join FarmerCropDetail in _agriContext.FarmerCropDetails
                                       on soil.FarmerCropDetailId equals FarmerCropDetail.FarmerCropDetailId
                                       where soil.DeleteStatus == false && soil.SoilTestReportId==SoilTestReportId

                                          select new
                                          {
                                              soil.SoilTestReportId,
                                              soil.FarmerCropDetailId,
                                              soil.Date,
                                              soil.Ph,
                                              soil.Ec,
                                              soil.Nitrogen,
                                              soil.Phosphorus,
                                              soil.Potassium,
                                              soil.Zink,
                                              soil.Iron,
                                              soil.Manganese,
                                              soil.Copper,
                                              soil.Boron,
                                              soil.OcPercentage,
                                              soil.SoilTexture,
                                              soil.DeleteStatus
                                          }
                                ).FirstOrDefault();
            if (SoilTestListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new SoilTestReportModel
            {

                SoilTestReportId = SoilTestListEntity.SoilTestReportId,
                FarmerCropDetailId = SoilTestListEntity.FarmerCropDetailId,
                Date = SoilTestListEntity.Date,
                Ph = SoilTestListEntity.Ph,
                Ec = SoilTestListEntity.Ec,
                Nitrogen = SoilTestListEntity.Nitrogen,
                Phosphorus = SoilTestListEntity.Phosphorus,
                Potassium = SoilTestListEntity.Potassium,
                Zink = SoilTestListEntity.Zink,
                Iron = SoilTestListEntity.Iron,
                Manganese = SoilTestListEntity.Manganese,
                Copper = SoilTestListEntity.Copper,
                Boron = SoilTestListEntity.Boron,
                OcPercentage = SoilTestListEntity.OcPercentage,
                SoilTexture = SoilTestListEntity.SoilTexture,
                DeleteStatus = SoilTestListEntity.DeleteStatus,

        };

        }


        public string Add(SoilTestReportModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           // var SoilModelList = new SoilTestReportModel();
             SoilTestReport soilEntity = new SoilTestReport();
            soilEntity.SoilTestReportId = model.SoilTestReportId;
            soilEntity.FarmerCropDetailId = model.FarmerCropDetailId;
            soilEntity.Date = model.Date;
            soilEntity.Ph = model.Ph;
            soilEntity.Ec = model.Ec;
            soilEntity.Nitrogen = model.Nitrogen;
            soilEntity.Phosphorus = model.Phosphorus;
            soilEntity.Potassium = model.Potassium;
            soilEntity.Zink = model.Zink;
            soilEntity.Iron = model.Iron;
            soilEntity.Manganese = model.Manganese;
            soilEntity.Copper = model.Copper;
            soilEntity.Boron = model.Boron;
            soilEntity.OcPercentage = model.OcPercentage;
            soilEntity.SoilTexture = model.SoilTexture;
            soilEntity.DeleteStatus = model.DeleteStatus;
            _agriContext.SoilTestReports.Add(soilEntity);
             _agriContext.SaveChanges();
             message = "Data Added Succesfully";
            return message;
        }

        public bool Put(SoilTestReportModel model, ref ErrorResponseModel errorResponseModel)
        {
            var SoilTestReportId = Convert.ToInt32(model.SoilTestReportId);
            var soilEntity = _agriContext.SoilTestReports.FirstOrDefault(x => x.SoilTestReportId == SoilTestReportId);
            if (soilEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                soilEntity.FarmerCropDetailId = model.FarmerCropDetailId;
                soilEntity.Date = model.Date;
                soilEntity.Ph = model.Ph;
                soilEntity.Ec = model.Ec;
                soilEntity.Nitrogen = model.Nitrogen;
                soilEntity.Phosphorus = model.Phosphorus;
                soilEntity.Potassium = model.Potassium;
                soilEntity.Zink = model.Zink;
                soilEntity.Iron = model.Iron;
                soilEntity.Manganese = model.Manganese;
                soilEntity.Copper = model.Copper;
                soilEntity.Boron = model.Boron;
                soilEntity.OcPercentage = model.OcPercentage;
                soilEntity.SoilTexture = model.SoilTexture;
                soilEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SaveChanges();
                return true;
            }
        }

       
        public string Delete(long SoilTestReportId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SoilTestReportModel model = new SoilTestReportModel();
            var soilEntity = _agriContext.SoilTestReports.FirstOrDefault(x => x.FarmerCropDetailId == SoilTestReportId);
            if (soilEntity != null)
            {
                soilEntity.DeleteStatus = true;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;

        }

        public List<SoilTestReportModel> GetAllFarmDetails(int FarmerCropDetailId)
        {
            var errorResponseModel = new ErrorResponseModel();
            var SoilTestModelList = new List<SoilTestReportModel>();
            var SoilTestListEntity = (from soil in _agriContext.SoilTestReports
                                      join FarmerCropDetail in _agriContext.FarmerCropDetails
                                      on soil.FarmerCropDetailId equals FarmerCropDetail.FarmerCropDetailId
                                      where soil.DeleteStatus == false && soil.FarmerCropDetailId == FarmerCropDetailId
                                      select new
                                      {
                                          soil.SoilTestReportId,
                                          soil.FarmerCropDetailId,
                                          soil.Date,
                                          soil.Ph,
                                          soil.Ec,
                                          soil.Nitrogen,
                                          soil.Phosphorus,
                                          soil.Potassium,
                                          soil.Zink,
                                          soil.Iron,
                                          soil.Manganese,
                                          soil.Copper,
                                          soil.Boron,
                                          soil.OcPercentage,
                                          soil.SoilTexture,
                                          soil.DeleteStatus,
                                      }
                                  ).ToList();
            if (SoilTestListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }

            foreach (var item in SoilTestListEntity)
            {
                var model = new SoilTestReportModel();
                model.SoilTestReportId = item.SoilTestReportId;
                model.FarmerCropDetailId = item.FarmerCropDetailId;
                model.Date = item.Date;
                model.Ph = item.Ph;
                model.Ec = item.Ec;
                model.Nitrogen = item.Nitrogen;
                model.Phosphorus = item.Phosphorus;
                model.Potassium = item.Potassium;
                model.Zink = item.Zink;
                model.Iron = item.Iron;
                model.Manganese = item.Manganese;
                model.Copper = item.Copper;
                model.Boron = item.Boron;
                model.OcPercentage = item.OcPercentage;
                model.SoilTexture = item.SoilTexture;
                model.DeleteStatus = item.DeleteStatus;
                SoilTestModelList.Add(model);
            }
            return SoilTestModelList;
        }

    }
}

