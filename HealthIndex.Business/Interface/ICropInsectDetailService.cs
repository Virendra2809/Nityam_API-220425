using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropInsectDetailService
    {
        List<CropInsectDetailModel> GetAllCropInsectDetail();
        string AddCropInsectDetail(CropInsectDetailModel model, ref ErrorResponseModel errorResponseModel);
        CropInsectDetailModel GetById(long CropInsectDetailId, ref ErrorResponseModel errorResponseModel);
        public bool Put(CropInsectDetailModel model, ref ErrorResponseModel errorResponseModel);
    }
}
