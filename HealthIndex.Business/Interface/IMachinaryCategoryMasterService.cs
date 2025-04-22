using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IMachinaryCategoryMasterService
    {
        List<MachinaryCategoryMasterModel> GetAllMachinaryCategoryMaster();
        MachinaryCategoryMasterModel GetMachinaryCategoryMasterById(long MachinaryCategoryId, ref ErrorResponseModel errorResponseModel);
        string AddMachinaryCategoryMaster(MachinaryCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateMachinaryCategoryMaster(MachinaryCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteMachinaryCategoryMaster(long MachinaryCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
