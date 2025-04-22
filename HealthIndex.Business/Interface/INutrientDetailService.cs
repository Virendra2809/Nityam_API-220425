using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface INutrientDetailService
    {
        object Value { get; }
        List<NutrientDetailModel> GetAll();
        string Add(NutrientDetailModel model, ref ErrorResponseModel errorResponseModel);
        NutrientDetailModel GetById(long NutrientDetailModel, ref ErrorResponseModel errorResponseModel);
        public bool Put(NutrientDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long NutrientDetailId, ref ErrorResponseModel errorResponseModel);

    }
}
