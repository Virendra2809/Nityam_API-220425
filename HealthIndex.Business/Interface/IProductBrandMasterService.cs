using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IProductBrandMasterService
    {
        List<ProductBrandMasterModel> GetAllProductBrandMaster();
        ProductBrandMasterModel GetProductBrandMasterById(long ProductBrandId, ref ErrorResponseModel errorResponseModel);
        string AddProductBrandMaster(ProductBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProductBrandMaster(ProductBrandMasterModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteProductBrandMaster(long ProductBrandId, ref ErrorResponseModel errorResponseModel);
        List<MenuProductCategoryModel> BrandList();
    }
}
