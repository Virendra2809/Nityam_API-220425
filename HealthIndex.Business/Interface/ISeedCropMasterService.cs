using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISeedCropMasterService
    {
        List<SeedCropMasterModel> GetAllSeedCropMaster();
        string AddSeedCropMaster(SeedCropMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedCropMaster(SeedCropMasterModel model, ref ErrorResponseModel errorResponseModel);
        SeedCropMasterModel GetSeedCropMasterById(long SeedCropId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedCropMaster(long SeedCropId, ref ErrorResponseModel errorResponseModel);
    }
}
