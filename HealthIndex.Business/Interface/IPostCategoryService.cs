using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IPostCategoryService
    {
        List<PostCategoryModel> GetAll();
        PostCategoryModel GetById(long CategoryId, ref ErrorResponseModel errorResponseModel);
        string Add(PostCategoryModel model, ref ErrorResponseModel errorResponseModel);
        public bool Put(PostCategoryModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CategoryId, ref ErrorResponseModel errorResponseModel);

    }
}
