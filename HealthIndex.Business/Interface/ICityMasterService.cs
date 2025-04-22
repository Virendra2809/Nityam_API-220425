using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICityMasterService

    { 
        object Value { get; }
        List<CityMasterModel> GetAll();
        string Add(CityMasterModel model, ref ErrorResponseModel errorResponseModel);
        CityMasterModel GetById(long CityId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CityMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CityId, ref ErrorResponseModel errorResponseModel);

    }
}
