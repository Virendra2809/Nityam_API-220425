using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IMachinaryBrandMasterService
    {
        List<MachinaryBrandMasterModel> GetAllMachinaryBrandMaster();
        string AddMachinaryBrandMaster(MachinaryBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateMachinaryBrandMaster(MachinaryBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        MachinaryBrandMasterModel GetMachinaryBrandMasterById(long MachinaryBrandId, ref ErrorResponseModel errorResponseModel);
        string DeleteMachinaryBrandMaster(long MachinaryBrandId, ref ErrorResponseModel errorResponseModel);
    }
}
