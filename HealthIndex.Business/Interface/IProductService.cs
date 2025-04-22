using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IProductService
    {
        List<ProductModel> GetAllProduct();
        ProductModel GetProductById(long ProductId, ref ErrorResponseModel errorResponseModel);
        string AddProduct(ProductModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProduct(ProductModel model, ref ErrorResponseModel errorResponseModel);
       string DeleteProduct(long ProductId, ref ErrorResponseModel errorResponseModel);

        List<ProductModel> GetProductBySubcategory(long SubCategoryId, CropParameters productsubParameters);

        List<ProductModel> GetProductBychildSubcategory(long SubCategoryId, CropParameters productchildParameters);

        List<ProductModel> GetFilterAllProducts(ProductSearch model);
        string DeleteImageFromDB(ProductModel model);
        string AddProductForWeb(ProductModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProductForWeb(ProductModel model, ref ErrorResponseModel errorResponseModel);


    }
}
