using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropProtectionCategoryMasterService
    {
        object Value { get; }

        List<CropProtectionCategoryMasterModel> GetAll();
        string Add(CropProtectionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropProtectionCategoryMasterModel GetById(long CropProtectionCategoryId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropProtectionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropProtectionCategoryId, ref ErrorResponseModel errorResponseModel);
       
    }
}
