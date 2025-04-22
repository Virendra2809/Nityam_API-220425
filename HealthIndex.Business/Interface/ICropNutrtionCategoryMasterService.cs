using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropNutrtionCategoryMasterService
    {
        List<CropNutrtionCategoryMasterModel> GetAllCropNutrtionCategoryMaster();
        string AddCropNutrtionCategoryMaster(CropNutrtionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropNutrtionCategoryMaster(CropNutrtionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropNutrtionCategoryMasterModel GetCropNutrtionCategoryMasterById(long CropNutrtionCategoryId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropNutrtionCategoryMaster(long CropNutrtionCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
