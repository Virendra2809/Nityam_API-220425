using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAgriProductTypeMasterService
    {
        List<AgriProductTypeMasterModel> GetAllAgriProductTypeMaster();
        AgriProductTypeMasterModel GetAgriProductTypeMasterById(long AgriProductTypeId, ref ErrorResponseModel errorResponseModel);
        string AddAgriProductTypeMaster(AgriProductTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateAgriProductTypeMaster(AgriProductTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteAgriProductTypeMaster(long AgriProductTypeId, ref ErrorResponseModel errorResponseModel);
    }
}
