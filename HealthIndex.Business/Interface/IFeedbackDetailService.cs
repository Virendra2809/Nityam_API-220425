using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace HealthIndex.Business.Interface
{
    public interface IFeedbackDetailService
    {
        List<FeedbackDetailModel> GetFeedbackDetails(ref ErrorResponseModel errorResponseModel);
        public  Message AddFeedback(FeedbackModel model,ref ErrorResponseModel errorResponseModel);
        FeedbackCountModel GetFeedbackCount(ref ErrorResponseModel errorResponseModel);
        Feedbackcomment GetFeedbackById(long feedbackId,ref ErrorResponseModel errorResponseModel);

    }
}
