using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropCultivationProcessService
    {
        object Value { get; }
        List<CropCultivationProcessModel> GetAll();
        string Add(CropCultivationProcessModel model, ref ErrorResponseModel errorResponseModel);
        CropCultivationProcessModel GetById(long CropCultivationProcessId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropCultivationProcessModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CropCultivationProcessId, ref ErrorResponseModel errorResponseModel);

    }
}
