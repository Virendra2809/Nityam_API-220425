using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IFarmerMasterService
    {
        List<FarmerMasterModel> GetAllFarmerMaster();
        string AddFarmerMaster(FarmerAddModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdatefarmerMaster(FarmerMasterModel model, ref ErrorResponseModel errorResponseModel);
        FarmerMasterModel GetFarmerMasterById(long FarmerId, ref ErrorResponseModel errorResponseModel);
        string DeleteFarmerMaster(long FarmerId, ref ErrorResponseModel errorResponseModel);
        FarmerMasterModel ExistingFarmer(string MobileNumber, ref ErrorResponseModel errorResponseModel);
        public bool UpdateDeviceToken(FarmerDeviceTokenModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(FarmerMasterModel model);


    }
}
