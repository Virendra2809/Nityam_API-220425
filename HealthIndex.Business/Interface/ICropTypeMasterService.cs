using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropTypeMasterService
    {
        List<CropTypeMasterModel> GetAllCropTypeMaster();
        string AddCropTypeMaster(CropTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropTypeMaster(CropTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropTypeMasterModel GetCropTypeMasterById(long CropTypeId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropTypeMaster(long CropTypeId, ref ErrorResponseModel errorResponseModel);
    }
}
