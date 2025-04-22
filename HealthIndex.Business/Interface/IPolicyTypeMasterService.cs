using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IPolicyTypeMasterService
    {
        List<PolicyTypeMasterModel> GetAllPolicyTypeMaster();
        PolicyTypeMasterModel GetPolicyTypeMasterById(long PolicyTypeId, ref ErrorResponseModel errorResponseModel);
        string AddPolicyTypeMaster(PolicyTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdatePolicyTypeMaster(PolicyTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeletePolicyTypeMaster(long PolicyTypeId, ref ErrorResponseModel errorResponseModel);
    }
}
