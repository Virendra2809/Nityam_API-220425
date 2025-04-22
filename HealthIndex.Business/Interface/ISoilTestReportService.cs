using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ISoilTestReportService
    {
        object Value { get; }

        List<SoilTestReportModel> GetAll();
        string Add(SoilTestReportModel model, ref ErrorResponseModel errorResponseModel);
        SoilTestReportModel GetById(long SoilTestReportId, ref ErrorResponseModel errorResponseModel);
        public bool Put(SoilTestReportModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long SoilTestReportId, ref ErrorResponseModel errorResponseModel);
        List<SoilTestReportModel> GetAllFarmDetails(int FarmerCropDetailId);


    }
}
