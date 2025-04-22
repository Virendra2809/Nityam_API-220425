using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IRoleService
    {
        object Value { get; }
        List<RoleModel> GetAllRole();

    }
}
