using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IMandimasterService
    {

        object Value { get; }
        List<MandiMasterModel> GetAll();
      
        string Add(MandiMasterModel model, ref ErrorResponseModel errorResponseModel);
        MandiMasterModel GetById(long MandiId, ref ErrorResponseModel errorResponseModel);
        public bool Put(MandiMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long MandiId, ref ErrorResponseModel errorResponseModel);

        List<AgriProductMasterModel> GetAgriProduct(long AgriProductId);



    }
}
