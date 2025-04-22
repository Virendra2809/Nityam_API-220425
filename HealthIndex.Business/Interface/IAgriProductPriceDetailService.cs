using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAgriProductPriceDetailService
    {
        object Value { get; }

        List<AgriProductPriceDetailModel> GetAll();
        
        string Add(AgriProductPriceDetailModel model, ref ErrorResponseModel errorResponseModel);
        AgriProductPriceDetailModel GetById(long AgriProductPriceId, ref ErrorResponseModel errorResponseModel);
        public bool Put(AgriProductPriceDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long AgriProductPriceId, ref ErrorResponseModel errorResponseModel);
    }
}
