using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface INutrientsCategoryService
    {
        List<NutrientsCategoryModel> GetAll();
        NutrientsCategoryModel GetById(long NutrientsCategoryId, ref ErrorResponseModel errorResponseModel);
        string Add(NutrientsCategoryModel model, ref ErrorResponseModel errorResponseModel);
        public bool Update(NutrientsCategoryModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long NutrientsCategoryId, ref ErrorResponseModel errorResponseModel);

    }
}
