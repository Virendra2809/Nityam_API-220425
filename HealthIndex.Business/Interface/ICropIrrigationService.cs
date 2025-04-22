using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropIrrigationService
    {
        object Value { get; }
        List<CropIrrigationDetailModel> GetAllCropIrrigation();
        string AddCropIrrigation(CropIrrigationDetailModel model, ref ErrorResponseModel errorResponseModel);
        irrigationModel GetById(long CropId, ref ErrorResponseModel errorResponseModel);
        CropIrrigationDetailModel GetByIdirrigationId(long IrrigationDetailId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropIrrigationDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long IrrigationDetailId, ref ErrorResponseModel errorResponseModel);
        List<CropStageModel> GetAllCropstage(int CropId);


    }
}
