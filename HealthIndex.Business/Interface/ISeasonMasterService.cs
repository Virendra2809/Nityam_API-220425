using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ISeasonMasterService
    {
        List<SeasonMasterModel> GetAll();
        SeasonMasterModel GetById(long SeasonId, ref ErrorResponseModel errorResponseModel);
        string Add(SeasonMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool Put(SeasonMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long SeasonId, ref ErrorResponseModel errorResponseModel);

    }
}
