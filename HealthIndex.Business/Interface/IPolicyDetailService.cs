using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IPolicyDetailService
    {
        List<PolicyDetailModel> GetAll();
        PolicyDetailModel GetById(long PostId, ref ErrorResponseModel errorResponseModel);
        List<PolicyDetailModel> GetPolicyByCategory(long CategoryId);

        string Add(PolicyDetailModel model, ref ErrorResponseModel errorResponseModel);
        public bool Put(PolicyDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long PostId, ref ErrorResponseModel errorResponseModel);
       
    }
}
