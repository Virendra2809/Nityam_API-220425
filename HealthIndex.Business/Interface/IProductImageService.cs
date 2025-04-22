using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IProductImageService
    {
        List<ProductImagesModel> GetAllProductImages();
        ProductImagesModel GetProductImagesById(long ProductImagesId, ref ErrorResponseModel errorResponseModel);
        string AddProductImages(ProductImagesModel model, ref ErrorResponseModel errorResponseModel);
        public bool UpdateProductImages(ProductImagesModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteProductImages(long ProductImageId, ref ErrorResponseModel errorResponseModel);
    }
}
