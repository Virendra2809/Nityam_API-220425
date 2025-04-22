using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IFeedbackReplyService
    {
        Message AddFeedbackReply(FeedbackReplyModel model, ref ErrorResponseModel errorResponseModel);

    }
}
