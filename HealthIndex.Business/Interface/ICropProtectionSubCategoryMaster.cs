using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public  interface ICropProtectionSubCategoryMaster
    {
        object Value { get; }

        List<CropProtectionSubCategoryMasterModel> GetAll();
        string Add(CropProtectionSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropProtectionSubCategoryMasterModel GetById(long CropProtectionSubCategoryId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropProtectionSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete( long CropProtectionSubCategoryId, ref ErrorResponseModel errorResponseModel);

    }
}
