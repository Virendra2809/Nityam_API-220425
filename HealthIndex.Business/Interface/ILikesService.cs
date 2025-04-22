using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ILikesService
    {
        string Add(LikeModel model, ref ErrorResponseModel errorResponseModel);
        //string Delete(long PostId, long LikedBy, ref ErrorResponseModel errorResponseModel);

    }
}
