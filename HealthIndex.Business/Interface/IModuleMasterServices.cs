using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
  public  interface IModuleMasterServices
    {
        object Value { get; }

        List<ModuleMasterModel> GetAllModulemaster();
        ModuleMasterModel GetById(long ModuleId, ref ErrorResponseModel errorResponseModel);
        string AddModule(ModuleMasterModel model, ref ErrorResponseModel errorResponseModel);
     
        public bool Put(ModuleMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteModule(long ModuleId, ref ErrorResponseModel errorResponseModel);
        
    }
}
