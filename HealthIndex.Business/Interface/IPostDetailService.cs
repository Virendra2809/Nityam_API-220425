using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IPostDetailService
    {
        List<PostDetailModel> GetAll(PageParameters pageParameters);
        List<PostDetailModel> GetAllPost(long CategoryId, PageParameters pageParameters);
        PostDetailModel GetById(long PostId, ref ErrorResponseModel errorResponseModel);
        string Add(PostDetailModel model, ref ErrorResponseModel errorResponseModel);
        public bool Put(PostDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long PostId, ref ErrorResponseModel errorResponseModel);
        string DeleteImageFromDB(PostDetailModel model);
       
    }
}
