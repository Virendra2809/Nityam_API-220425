using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropInsectMasterservice
    {
        List<CropInsectMasterModel> GetAllCropInspect();
        string AddCrop(CropInsectMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropInsectMasterModel GetById(long CropInsectId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropInsectMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropInsectId, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(CropInsectMasterModel model);

        string DeleteSysmtom(long CropInsectSymptomId, ref ErrorResponseModel errorResponseModel);
        string DeleteManagement(long CropInsectManagementId, ref ErrorResponseModel errorResponseModel);


    }
}
