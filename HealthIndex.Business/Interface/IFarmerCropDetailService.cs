using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IFarmerCropDetailService
    {
        object Value { get; }

        List<FarmerCropDetailsModel> GetAll(int FarmerId);
        string Add(FarmAddModel model, ref ErrorResponseModel errorResponseModel);
        FarmerCropDetailsModel GetById(long FarmerId, ref ErrorResponseModel errorResponseModel);
        public bool Put(FarmerCropDetailsModel model, ref ErrorResponseModel errorResponseModel);
        List<FarmerSelectedCropModel> GetByFarmerId(int FarmerId);
        List<FarmerSelectedCropModel> GetCropListByFarmerId(int FarmerId);
        string Delete(long FarmerCropDetailId, ref ErrorResponseModel errorResponseModel);
        List<FarmerCropDetailsModel> GetAllFarmerDetails(int FarmerId,int CropId);
        List<FarmerSelectedCropModel> GetAllfarmerCrop();
        string AddSelectedCrop(FarmerSelectedCropModel model, ref ErrorResponseModel errorResponseModel);
        FarmerSelectedCropModel FarmerSelectedCropId(long FarmerSelectedCropId, ref ErrorResponseModel errorResponseModel);
        public bool PutFarmerSelectedCrop(FarmerSelectedCropModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteSelectedCrops(long FarmerSelectedCropId, ref ErrorResponseModel errorResponseModel);


    }
}
