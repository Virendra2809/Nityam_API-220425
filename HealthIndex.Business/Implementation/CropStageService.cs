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
   public class CropStageService:ICropStageService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public CropStageService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<CropStageModel> GetAllCropstage()
        {
            var errorResponseModel = new ErrorResponseModel();
            var CropstageModelList = new List<CropStageModel>();
            var CropstageListEntity = (from CropStageMaster in _agriContext.CropStageMasters
                                       join Crop in _agriContext.CropMasters
                                       on CropStageMaster.CropId equals Crop.CropId
                                       where CropStageMaster.DeleteStatus == false  
                                               select new
                                               {
                                                   CropStageMaster.CropStageId,
                                                   CropStageMaster.CropStageName,
                                                   Crop.CropId,
                                                   Crop.CropName,
                                                   CropStageMaster.SeqNo,
                                                   CropStageMaster.DeleteStatus,
                                               }
                                  ).ToList();
            if (CropstageListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in CropstageListEntity)
            {
                var model = new CropStageModel();
                model.CropStageId = item.CropStageId;
                model.CropStageName = item.CropStageName;
                model.CropId = item.CropId;
                model.CropName = item.CropName;
                model.SeqNo = item.SeqNo;
                model.DeleteStatus = item.DeleteStatus;
               
                CropstageModelList.Add(model);
            }
            return CropstageModelList;
        }

        CropStageModel ICropStageService.GetById(long CropStageId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var CropstageListEntity = (from CropStageMaster in _agriContext.CropStageMasters
                                       join Crop in _agriContext.CropMasters
                                       on CropStageMaster.CropId equals Crop.CropId
                                       where CropStageMaster.DeleteStatus == false && CropStageMaster.CropStageId==CropStageId
                                       select new
                                       {
                                           CropStageMaster.CropStageId,
                                           CropStageMaster.CropStageName,
                                           Crop.CropId,
                                           Crop.CropName,
                                           CropStageMaster.SeqNo,
                                           CropStageMaster.DeleteStatus,
                                       }
                                 ).FirstOrDefault();

            if (CropstageListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropStageModel
            {
                CropStageId = CropstageListEntity.CropStageId,
                CropName = CropstageListEntity.CropName,
                CropId = CropstageListEntity.CropId,
                CropStageName = CropstageListEntity.CropStageName,
                SeqNo = CropstageListEntity.SeqNo,
                DeleteStatus = CropstageListEntity.DeleteStatus,
               
            };

        }

        public string AddCropstage(CropStageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existing = _agriContext.CropStageMasters.Any(x => x.CropStageName == model.CropStageName && x.CropId==model.CropId);
            var existingNo = _agriContext.CropStageMasters.Any(x => x.SeqNo == model.SeqNo);

            if (existing)
            {
                message = GlobalConstants.ExistingName;
            }
            else if (existingNo)
            {
                message = GlobalConstants.ExistingSequenceNumber;

            }
            else
            {
                var CropStageEntity = new CropStageMaster();
                CropStageEntity.CropStageId = model.CropStageId;
                CropStageEntity.CropStageName = model.CropStageName;
                CropStageEntity.CropId = model.CropId;
                CropStageEntity.SeqNo = model.SeqNo;
                CropStageEntity.DeleteStatus = false;  
                _agriContext.CropStageMasters.Add(CropStageEntity);
                _agriContext.SaveChanges();
                message = "Data Added Successfully";
            }
            return message;
        }

        public bool Put(CropStageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var CropStageId = Convert.ToInt32(model.CropStageId);
            var CropStageEntity = _agriContext.CropStageMasters.FirstOrDefault(x => x.CropStageId == CropStageId && x.DeleteStatus==false);
            if (CropStageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropStageEntity.CropStageId = model.CropStageId;
                CropStageEntity.CropStageName = model.CropStageName;
                CropStageEntity.CropId = model.CropId;
                CropStageEntity.SeqNo = model.SeqNo;
                CropStageEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                }

            return true;
        }

        public string Delete(long CropStageId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropStageModel model = new CropStageModel();
            var cropStageEntity = _agriContext.CropStageMasters.FirstOrDefault(x => x.CropStageId == CropStageId);
            if (cropStageEntity != null)
            {
                cropStageEntity.DeleteStatus = true;
              
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}





