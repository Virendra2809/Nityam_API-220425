using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAgriProductMasterService
    {
        List<AgriProductMasterModel> GetAllAgriProductMaster();
        AgriProductMasterModel GetAgriProductMasterById(long AgriProductId, ref ErrorResponseModel errorResponseModel);
        string AddAgriProductMaster(AgriProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateAgriProductMaster(AgriProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteAgriProductMaster(long AgriProductId, ref ErrorResponseModel errorResponseModel);
    }
}
