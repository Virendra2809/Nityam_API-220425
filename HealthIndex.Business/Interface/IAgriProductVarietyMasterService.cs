using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAgriProductVarietyMasterService
    {
        List<AgriProductVarietyMasterModel> GetAllAgriProductVarietyMaster();
        AgriProductVarietyMasterModel GetAgriAgriProductVarietyMasterById(long VarietyId, ref ErrorResponseModel errorResponseModel);
        string AddAgriProductVarietyMaster(AgriProductVarietyMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateAgriProductVarietyMaster(AgriProductVarietyMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteAgriProductVarietyMaster(long VarietyId, ref ErrorResponseModel errorResponseModel);
    }
}
