using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IPolicyCategoryMasterService
    {
        List<PolicyCategoryMasterModel> GetAllPolicyCategoryMaster();
        PolicyCategoryMasterModel GetPolicyCategoryeMasterById(long PolicyCategoryId, ref ErrorResponseModel errorResponseModel);
        string AddPolicyCategoryMaster(PolicyCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdatePolicyCategoryMaster(PolicyCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeletePolicyCategoryMaster(long PolicyCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
