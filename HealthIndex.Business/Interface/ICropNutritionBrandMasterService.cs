using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropNutritionBrandMasterService
    {
        List<CropNutritionBrandMasterModel> GetAllCropNutritionBrandMaster();
        string AddCropNutritionBrandMaster(CropNutritionBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropNutritionBrandMaster(CropNutritionBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropNutritionBrandMasterModel GetCropNutritionBrandMasterById(long CropNutritionBrandId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropNutrtionBrandMaster(long CropNutritionBrandId, ref ErrorResponseModel errorResponseModel);
    }
}
