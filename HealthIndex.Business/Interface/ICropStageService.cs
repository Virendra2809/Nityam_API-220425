using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropStageService
    {
        object Value { get; }
        List<CropStageModel> GetAllCropstage();
        string AddCropstage(CropStageModel model, ref ErrorResponseModel errorResponseModel);
        CropStageModel GetById(long CropStageId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropStageModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropStageId, ref ErrorResponseModel errorResponseModel);

    }
}
