using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropCultivationProcessDetailService
    {
        object Value { get; }
        List<CropCultivationProcessDetailModel> GetAll();
        string Add(CropCultivationProcessDetailModel model, ref ErrorResponseModel errorResponseModel);
        CropCultivationProcessDetailModel GetById(long CropCultivationProcessDetailId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropCultivationProcessDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropCultivationProcessDetailId, ref ErrorResponseModel errorResponseModel);

    }
}
