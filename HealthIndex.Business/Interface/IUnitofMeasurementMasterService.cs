using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IUnitofMeasurementMasterService
    {

        object Value { get; }
        List<UnitofMeasurementMasterModel> GetAll();
        string Add(UnitofMeasurementMasterModel model, ref ErrorResponseModel errorResponseModel);
        UnitofMeasurementMasterModel GetById(long UnitId, ref ErrorResponseModel errorResponseModel);
        public bool Put(UnitofMeasurementMasterModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long UnitId, ref ErrorResponseModel errorResponseModel);

    }
}
