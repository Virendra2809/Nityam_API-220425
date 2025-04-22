using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ISoilTypeMasterService
    {
        object Value { get; }
        List<SoilTypeMasterModel> GetAll();
        string Add(SoilTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        SoilTypeMasterModel GetById(long SoilTypeId, ref ErrorResponseModel errorResponseModel);
        public bool Put(SoilTypeMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long SoilTypeId, ref ErrorResponseModel errorResponseModel);

    }
}
