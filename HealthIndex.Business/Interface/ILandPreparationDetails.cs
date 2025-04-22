using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ILandPreparationDetails
    {
        object Value { get; }
        List<LandPreparationDetailsModel> GetAll();
        string Add(LandPreparationDetailsModel model, ref ErrorResponseModel errorResponseModel);
        LandPreparationDetailsModel GetById(long LandPreparationId, ref ErrorResponseModel errorResponseModel);
        public bool Put(LandPreparationDetailsModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(LandPreparationDetailsModel model);
        string Delete(long LandPreparationId, ref ErrorResponseModel errorResponseModel);
        LandPreparationModel GetByCropId(long CropId, ref ErrorResponseModel errorResponseModel);

    }
}
