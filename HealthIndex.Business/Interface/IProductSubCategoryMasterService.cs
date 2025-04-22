using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IProductSubCategoryMasterService
    {
        List<ProductSubCategoryMasterModel> GetAllProductSubCategoryMaster();
        List<ProductSubCategoryMasterModel> GetParentCategoryList();
        string AddProductSubCategoryMaster(ProductSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProductSubCategoryMaster(ProductSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel);
        ProductSubCategoryMasterModel GetProductSubCategoryMasterById(long SubCategoryId, ref ErrorResponseModel errorResponseModel);
        string DeleteProductSubCategoryMaster(long SubCategoryId, ref ErrorResponseModel errorResponseModel);
    }
}
