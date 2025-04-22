using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropProtectionBrandMasterService
    {
        List<CropProtectionBrandMasterModel> GetAllCropProtectionBrandMaster();
        string AddCropProtectionBrandMaster(CropProtectionBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropProtectionBrandMaster(CropProtectionBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropProtectionBrandMasterModel GetCropProtectionBrandMasterById(long CropProtectionBrandId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropProtectionBrandMasterr(long CropProtectionBrandId, ref ErrorResponseModel errorResponseModel);
    }
}
