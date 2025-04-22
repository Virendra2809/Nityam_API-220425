using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ISeedProductMasterService
    {
        List<SeedProductMasterModel> GetAllSeedProductMaster();
        string AddSeedProductMaster(SeedProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedProductMaster(SeedProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        SeedProductMasterModel GetSeedProductMasterById(long SeedProductId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedProductMaster(long SeedProductId, ref ErrorResponseModel errorResponseModel);
       List<MenuProductCategory> CategoryList();

    }
}
