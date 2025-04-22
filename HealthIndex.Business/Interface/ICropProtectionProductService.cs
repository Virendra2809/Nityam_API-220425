using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropProtectionProductService
    {
        object Value { get; }

        List<CropProtectionProductMasterModel> GetAll();
        string Add(CropProtectionProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropProtectionProductMasterModel GetById(long CropProtectionProductId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropProtectionProductMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropProtectionProductId, ref ErrorResponseModel errorResponseModel);

    }
}
