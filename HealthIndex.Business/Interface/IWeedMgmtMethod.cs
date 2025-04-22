using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IWeedMgmtMethod
    {
        List<WeedMgmtMethodModel> GetAll();
        string Add(WeedMgmtMethodModel model, ref ErrorResponseModel errorResponseModel);
        public bool Update(WeedMgmtMethodModel model, ref ErrorResponseModel errorResponseModel);
        WeedMgmtMethodModel GetById(long WeedMgmtMethodId, ref ErrorResponseModel errorResponseModel);
        string Delete(long WeedMgmtMethodId, ref ErrorResponseModel errorResponseModel);
       public string DeleteImageFromDB(WeedMgmtMethodModel model);
    }
}
