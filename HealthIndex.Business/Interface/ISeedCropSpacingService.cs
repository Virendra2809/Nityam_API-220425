using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ISeedCropSpacingService
    {

        List<SeedCropSpacingModel> GetAllSeedCropSpacing ();
        string AddSeedCropSpacing(SeedCropSpacingModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateSeedCropSpacing(SeedCropSpacingModel model, ref ErrorResponseModel errorResponseModel);
        SeedCropSpacingModel GetSeedCropSpacingById(long SeedCropSpacingId, ref ErrorResponseModel errorResponseModel);
        string DeleteSeedCropSpacing(long SeedCropSpacingId, ref ErrorResponseModel errorResponseModel);


    }
}
