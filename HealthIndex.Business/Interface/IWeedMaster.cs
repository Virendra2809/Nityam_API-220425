using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IWeedMaster
    {
        object Value { get; }
        List<WeedMasterModel> GetAll();
        string Add(WeedMasterModel model, ref ErrorResponseModel errorResponseModel);
        WeedMasterModel GetById(long WeedId, ref ErrorResponseModel errorResponseModel);
        public bool Put(WeedMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long WeedId, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(WeedMasterModel model);
        string DeleteManagement(long WeedDetailId, ref ErrorResponseModel errorResponseModel);

    }
}
