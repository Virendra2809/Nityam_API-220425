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
   public class CropDiseaseDetailService:ICropDiseaseDetailService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public CropDiseaseDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropDiseaseDetailModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropDiseaseDetailModelList = new List<CropDiseaseDetailModel>();
            var CropDiseaseDetailListEntity = (from CropDiseaseDetail in _agriContext.CropDiseaseDetails
                                               join Disease in _agriContext.CropDiseaseMasters
                                                on CropDiseaseDetail.CropDiseaseId equals Disease.CropDiseaseId

                                               join crop in _agriContext.CropMasters
                                               on CropDiseaseDetail.CropId equals crop.CropId
                                               select new
                                               {
                                                   CropDiseaseDetail.CropDiseaseDetailId,
                                                   Disease.CropDiseaseId,
                                                   Disease.CropDiseaseName,
                                                   crop.CropName,
                                                   crop.CropId,
                                               }
                                  ).ToList();
            if (CropDiseaseDetailListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropDiseaseDetailListEntity)
            {
                var model = new CropDiseaseDetailModel();
                model.CropDiseaseDetailId = item.CropDiseaseDetailId;
                model.CropDiseaseId1 = (int)item.CropDiseaseId;
                model.CropDiseaseName = item.CropDiseaseName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;

                CropDiseaseDetailModelList.Add(model);
            }
            return CropDiseaseDetailModelList;



        }
        CropDiseaseDetailModel ICropDiseaseDetailService.GetById(long CropDiseaseDetailId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropDiseaseDetailEntity = (from CropDiseaseDetail in _agriContext.CropDiseaseDetails
                                           join Disease in _agriContext.CropDiseaseMasters
                                            on CropDiseaseDetail.CropDiseaseId equals Disease.CropDiseaseId

                                           join crop in _agriContext.CropMasters
                                           on CropDiseaseDetail.CropId equals crop.CropId
                                           where CropDiseaseDetail.CropDiseaseDetailId == CropDiseaseDetailId

                                           select new
                                           {
                                               CropDiseaseDetail.CropDiseaseDetailId,
                                               Disease.CropDiseaseId,
                                               Disease.CropDiseaseName,
                                               crop.CropName,
                                               crop.CropId,
                                           }
                                  ).FirstOrDefault();
            if (cropDiseaseDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropDiseaseDetailModel
            {
                CropDiseaseDetailId = cropDiseaseDetailEntity.CropDiseaseDetailId,
                CropDiseaseId1 = Convert.ToInt32(cropDiseaseDetailEntity.CropDiseaseId),
                CropId = cropDiseaseDetailEntity.CropId,
                CropDiseaseName=cropDiseaseDetailEntity.CropDiseaseName,
                CropName=cropDiseaseDetailEntity.CropName,
               
            };

        }


        public string Add(CropDiseaseDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            int[] arr= model.CropDiseaseId;
            var existing = _agriContext.CropDiseaseDetails.Any(x => x.CropDiseaseDetailId == model.CropDiseaseDetailId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var CropDiseaseEntity = new CropDiseaseDetail();
                var t1 = arr.Length;
                for (int i = 0; i <t1; i++)
                {
                    CropDiseaseEntity.CropDiseaseDetailId = model.CropDiseaseDetailId;
                    CropDiseaseEntity.CropDiseaseId = model.CropDiseaseId[i];
                    CropDiseaseEntity.CropId = model.CropId;
                    _agriContext.CropDiseaseDetails.Add(CropDiseaseEntity);
                    _agriContext.SaveChanges();
                    message = "CropDiseaseDetailMaster Added Succesfully ";

                }
            }

            return message;
        }
        //    else
        //    {
        //        var CropDiseaseEntity = new CropDiseaseDetail();
              
        //        CropDiseaseEntity.CropDiseaseDetailId = model.CropDiseaseDetailId;
        //        CropDiseaseEntity.CropDiseaseId  = model.CropDiseaseId[0];
        //        CropDiseaseEntity.CropId = model.CropId;
                
        //        _agriContext.CropDiseaseDetails.Add(CropDiseaseEntity);
        //        _agriContext.SaveChanges();

        //        message = "CropDiseaseDetailMaster Added Succesfully ";
        //    }
        //    return message;
        //}

        public bool Put(CropDiseaseDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropDiseaseDetailId = Convert.ToInt32(model.CropDiseaseDetailId);
            var CropDiseaseEntity = _agriContext.CropDiseaseDetails.FirstOrDefault(x => x.CropDiseaseDetailId == CropDiseaseDetailId );
            if (CropDiseaseEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropDiseaseEntity.CropDiseaseId = model.CropDiseaseId1;
                CropDiseaseEntity.CropDiseaseDetailId = model.CropDiseaseDetailId;
                CropDiseaseEntity.CropId = model.CropId;
                _agriContext.SaveChanges();
                return true;
            }
        }
       
    }
}
