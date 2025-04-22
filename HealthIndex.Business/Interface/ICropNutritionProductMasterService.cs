using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropNutritionProductMasterService
    {
        List<CropNutritionProductMasterModel> GetAllCropNutritionProductMaster();
        string AddCropNutritionProductMaster(CropNutritionProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropNutritionProductMaster(CropNutritionProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropNutritionProductMasterModel GetCropNutritionProductMasterById(long CropNutritionProductId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropNutritionProductMaster(long CropNutritionProductId, ref ErrorResponseModel errorResponseModel);
    }
}
