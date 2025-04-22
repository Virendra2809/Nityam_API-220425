using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISeedSubCategoryMasterService
    {
        List<SeedSubCategoryMasterModel> GetAllSeedSubCategoryMaster();
        string AddSeedSubCategoryMaster(SeedSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedSubCategoryMaster(SeedSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        SeedSubCategoryMasterModel GetSeedSubCategoryMasterById(long SeedSubCategoryId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedSubCategoryMaster(long SeedSubCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
