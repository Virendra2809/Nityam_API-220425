using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface INutrientMasterService
    {
        List<NutrientMasterModel> GetAll();
        NutrientMasterModel GetById(long NutrientId, ref ErrorResponseModel errorResponseModel);
        string Add(NutrientMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool Update(NutrientMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long NutrientId, ref ErrorResponseModel errorResponseModel);
    }
}
