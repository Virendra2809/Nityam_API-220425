using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropDiseaseDetailService
    {
        object Value { get; }
        List<CropDiseaseDetailModel> GetAll();
        string Add(CropDiseaseDetailModel model, ref ErrorResponseModel errorResponseModel);
        CropDiseaseDetailModel GetById(long CropDiseaseDetailId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropDiseaseDetailModel model, ref ErrorResponseModel errorResponseModel);
     }
}
