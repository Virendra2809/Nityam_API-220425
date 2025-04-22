using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public  interface IFirmDetail
    {
        object Value { get; }

        List<FirmdetailModel> GetAllFirm();
        string AddFirm(FirmdetailModel model, ref ErrorResponseModel errorResponseModel);
        FirmdetailModel GetById(long FirmId, ref ErrorResponseModel errorResponseModel);
        public bool Put(FirmdetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long FirmId, ref ErrorResponseModel errorResponseModel);
        
    }
}
