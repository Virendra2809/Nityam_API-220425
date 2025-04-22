using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IMachinaryProductMasterService
    {
        object Value { get; }

        List<MachinaryProductMasterModel> GetAll();
        string Add(MachinaryProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        MachinaryProductMasterModel GetById(long MachinaryProductId, ref ErrorResponseModel errorResponseModel);
        public bool Put(MachinaryProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete( long MachinaryProductId, ref ErrorResponseModel errorResponseModel);

    }
}
