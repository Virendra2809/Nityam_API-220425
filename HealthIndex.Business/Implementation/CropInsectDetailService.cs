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
    public class CropInsectDetailService : ICropInsectDetailService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public CropInsectDetailService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropInsectDetailModel> GetAllCropInsectDetail()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropInsectModelList = new List<CropInsectDetailModel>();
            var cropInsectListEntity = (from detail in _agriContext.CropInsectDetails
                                               join insect in _agriContext.CropInsectMasters
                                               on detail.CropInsectId equals insect.CropInsectId

                                               join crop in _agriContext.CropMasters
                                               on detail.CropId equals crop.CropId

                                               select new
                                               {
                                                   detail.CropInsectDetailId,
                                                   insect.CropInsectId,
                                                   insect.CropInsectName,
                                                   crop.CropId,
                                                   crop.CropName,                                                   
                                               }).ToList();
            if (cropInsectListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropInsectListEntity)
            {
                var model = new CropInsectDetailModel();
                model.CropInsectDetailId = item.CropInsectDetailId;
                model.CropInsectId1 = item.CropInsectId;
                model.CropInsectname = item.CropInsectName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                cropInsectModelList.Add(model);
            }
            return cropInsectModelList;
        }
        CropInsectDetailModel ICropInsectDetailService.GetById(long CropInsectDetailId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var cropInsectListEntity = (from detail in _agriContext.CropInsectDetails
                                        join insect in _agriContext.CropInsectMasters
                                        on detail.CropInsectId equals insect.CropInsectId
                                        join crop in _agriContext.CropMasters
                                        on detail.CropId equals crop.CropId
                                        where detail.CropInsectDetailId == CropInsectDetailId
                                        select new
                                        {
                                            detail.CropInsectDetailId,
                                            insect.CropInsectId,
                                            insect.CropInsectName,
                                            crop.CropId,
                                            crop.CropName,
                                        }).FirstOrDefault();

            if (cropInsectListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropInsectDetailModel
            {
                CropInsectDetailId = cropInsectListEntity.CropInsectDetailId,
                CropInsectId1 = Convert.ToInt32(cropInsectListEntity.CropInsectId),
                CropId = cropInsectListEntity.CropId,
                CropInsectname = cropInsectListEntity.CropInsectName,
                CropName = cropInsectListEntity.CropName,

            };

        }

        public string AddCropInsectDetail(CropInsectDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            int[] arr = model.CropInsectId;

            var existingCropInsect = _agriContext.CropInsectDetails.Any(x => x.CropInsectDetailId == model.CropInsectDetailId);
            if (existingCropInsect)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var cropInsectEntity = new CropInsectDetail();
                var t1 = arr.Length;
                for (int i = 0; i < t1; i++)
                {

                    //CropInsectDetail cropInsectEntity = new CropInsectDetail();
                    cropInsectEntity.CropInsectDetailId = model.CropInsectDetailId;
                    cropInsectEntity.CropInsectId = model.CropInsectId[i];
                    cropInsectEntity.CropId = model.CropId;
                    _agriContext.CropInsectDetails.Add(cropInsectEntity);
                    _agriContext.SaveChanges();
                    message = "Added Successfully";
                }
            }
            return message;
        }


        public bool Put(CropInsectDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropInsectDetailId = Convert.ToInt32(model.CropInsectDetailId);
            var CropInsectEntity = _agriContext.CropInsectDetails.FirstOrDefault(x => x.CropInsectDetailId == CropInsectDetailId);
            if (CropInsectEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropInsectEntity.CropInsectId = model.CropInsectId1;
                CropInsectEntity.CropInsectDetailId = model.CropInsectDetailId;
                CropInsectEntity.CropId = model.CropId;
                _agriContext.SaveChanges();
                return true;
            }
        }

    }
}
