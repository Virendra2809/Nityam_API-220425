using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface ICropMasterService
    {
        List<CropMasterModel> GetAllCropMaster( CropParameters cropParameters);
        string AddCropMaster(CropMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateCropMaster(CropMasterModel model, ref ErrorResponseModel errorResponseModel);
        CropMasterModel GetCropMasterById(long CropId, ref ErrorResponseModel errorResponseModel);
        string DeleteCropMaster(long CropId, ref ErrorResponseModel errorResponse);
        string DeleteImageFromDB(CropMasterModel model);
        CropDetailsModel GetByCropId(long CropId, ref ErrorResponseModel errorResponseModel);
        FertilizerCalculatorResultModel AddDataInCalculator(FertilizerCalculatorModel model, ref ErrorResponseModel errorResponseModel);
        string AddFormulae(FertCalculationFormulaModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateFormulae(FertCalculationFormulaModel model, ref ErrorResponseModel errorResponseModel);
        FertCalculationFormulaModel GetByFormulaeId(long FormulaId, ref ErrorResponseModel errorResponseModel);
        List<FertCalculationFormulaModel> GetAllFormulae();

    }
}
