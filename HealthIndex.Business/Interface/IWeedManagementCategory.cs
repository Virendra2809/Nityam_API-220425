using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IWeedManagementCategory
    {
        List<WeedManagementCategoryModel> GetAll();
        string Add(WeedManagementCategoryModel model, ref ErrorResponseModel errorResponseModel);
        public bool Update(WeedManagementCategoryModel model, ref ErrorResponseModel errorResponseModel);
        WeedManagementCategoryModel GetById(long WeedMgmtCategoryId, ref ErrorResponseModel errorResponseModel);
        string Delete(long WeedMgmtCategoryId, ref ErrorResponseModel errorResponseModel);

    }
}
