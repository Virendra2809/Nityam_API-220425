using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Implementation
{
    public class FeedbackReplyService : IFeedbackReplyService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public FeedbackReplyService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }

        public Message AddFeedbackReply(FeedbackReplyModel model, ref ErrorResponseModel errorResponseModel)
        {
            Message messagemodel= new Message();
            var existingfeedback = _healthindexdbcontext.FeedbackReplies.Any(x => x.FeedbackReplyId == model.FeedbackReplyId);
            if (existingfeedback)
            {
               messagemodel. message = GlobalConstants.ExistingFeedback;
            }
            else
            {
                var feedbackEntity = new FeedbackReply();
                feedbackEntity.FeedbackReplyId = model.FeedbackReplyId;
                feedbackEntity.FeedbackId = model.FeedbackId;
                feedbackEntity.ReplyComment = model.ReplyComment;
                feedbackEntity.ReplyDate = DateTime.Now;
                feedbackEntity.EnteredBy = 1;
                _healthindexdbcontext.FeedbackReplies.Add(feedbackEntity);
                _healthindexdbcontext.SaveChanges();
               messagemodel. message = GlobalConstants.FeedbackreplySavedSuccessfully;
            }
            return messagemodel;
        }

       
    }
}
