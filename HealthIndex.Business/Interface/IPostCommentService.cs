using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface IPostCommentService
    {
        List<PostCommentDetailModel> GetAll();
        PostCommentDetailModel GetById(long CommentId, ref ErrorResponseModel errorResponseModel);
        string Add(PostCommentDetailModel model, ref ErrorResponseModel errorResponseModel);
        public bool Put(PostCommentDetailModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long CommentId, ref ErrorResponseModel errorResponseModel);
        List<PostCommentDetailModel> GetAllComments(long PostId);
    }
}
