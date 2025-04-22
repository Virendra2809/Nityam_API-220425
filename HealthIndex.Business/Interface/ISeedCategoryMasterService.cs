using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISeedCategoryMasterService
    {
        List<SeedCategoryMasterModel> GetAllSeedCategoryMaster();
        string AddSeedCategoryMaster(SeedCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedCategoryMaster(SeedCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        SeedCategoryMasterModel GetSeedCategoryMasterById(long SeedCategoryId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedCategoryMaster(long SeedCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
