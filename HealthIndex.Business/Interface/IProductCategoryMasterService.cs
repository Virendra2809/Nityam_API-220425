using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IProductCategoryMasterService
    {
        List<ProductCategoryMasterModel> GetAllProductCategoryMaster();
        ProductCategoryMasterModel GetProductCategoryMasterById(long ProductCategoryId, ref ErrorResponseModel errorResponseModel);
        string AddProductCategoryMaster(ProductCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProductCategoryMaster(ProductCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteProductCategoryMaster(long ProductCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
