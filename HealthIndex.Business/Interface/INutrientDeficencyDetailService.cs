using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface INutrientDeficencyDetailService
    {
        object Value { get; }
        List<NutrientDeficencyDetailModel> GetAll();
        string Add(NutrientDeficencyDetailModel model, ref ErrorResponseModel errorResponseModel);
        NutrientDeficencyDetailModel GetById(long NutrientDeficencyId, ref ErrorResponseModel errorResponseModel);
        public bool Put(NutrientDeficencyDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long NutrientDeficencyId, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(NutrientDeficencyDetailModel model);
        string DeleteSymptom(long NutrientDeficencySymptomId, ref ErrorResponseModel errorResponseModel);
        string DeleteManagement(long NutrientDeficencyManagementId, ref ErrorResponseModel errorResponseModel);


    }
}
