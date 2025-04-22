using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropDiseaseMasterService
    {
        object Value { get; }
        List<CropDiseaseMasterModel> GetAllCropDisease();
        string AddCropDisease(CropDiseaseMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropDiseaseMasterModel GetById(long CropDiseaseId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropDiseaseMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropDiseaseId, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(CropDiseaseMasterModel model);
        string DeleteSymtom(long CropDiseaseIdSymptomId, ref ErrorResponseModel errorResponseModel);
        string DeleteManagement(long CropDiseaseIdManagementId, ref ErrorResponseModel errorResponseModel);

    }
}
