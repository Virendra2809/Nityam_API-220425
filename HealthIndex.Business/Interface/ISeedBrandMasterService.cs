using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISeedBrandMasterService
    {
        List<SeedBrandMasterModel> GetAllSeedBrandMaster();
        string AddSeedBrandMaster(SeedBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedBrandMaster(SeedBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        SeedBrandMasterModel GetSeedBrandMasterById(long SeedBrandId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedBrandMaster(long SeedBrandId, ref ErrorResponseModel errorResponseModel);
    }
}
