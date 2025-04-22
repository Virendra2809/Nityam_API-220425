using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace HealthIndex.Business.Implementation
{
    public class FeedbackDetailService : IFeedbackDetailService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public FeedbackDetailService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }

        public List<FeedbackDetailModel> GetFeedbackDetails(ref ErrorResponseModel errorResponseModel)
        {
            var feedbackdetailModelList = new List<FeedbackDetailModel>();
            ResponseMessage responseMessage = new ResponseMessage();
            errorResponseModel = new ErrorResponseModel();
            var feedbackdetailEntityList = (from feed in _healthindexdbcontext.FeedbackDetails
                                            join
                                            user in _healthindexdbcontext.AppUserMasters
                                            on feed.UserId equals user.AppUserId
                                            select new
                                            {
                                                feed.FeedbackId,
                                                feed.Rating,
                                                feed.Comments,
                                                feed.Feedbackdate,
                                                user.AppUserId,
                                                user.FirstName,
                                                user.LastName,
                                                user.EmailId
                                            }
                                           ).ToList();
            if (feedbackdetailEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Feedback Details not found";
                return null;
            }

            feedbackdetailEntityList.ForEach(item =>
            {
                feedbackdetailModelList.Add(new FeedbackDetailModel
                {
                    FeedbackId = item.FeedbackId,
                    UserId = item.AppUserId,
                    Rating = item.Rating,
                    Comments = item.Comments,
                    Feedbackdate = item.Feedbackdate,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EmailId = item.EmailId,
                });
            });
            return feedbackdetailModelList;
        }


        public Model.Message AddFeedback(FeedbackModel model, ref ErrorResponseModel errorResponseModel)
        {
           Model. Message messagemodel=new Model.Message();
            var existingfeedback = _healthindexdbcontext.FeedbackDetails.Any(x => x.FeedbackId == model.FeedbackId);
            if (existingfeedback)
            {
               messagemodel. message = GlobalConstants.ExistingFeedback;
            }
            else
            {
                var feedbackEntity = new FeedbackDetail();
                feedbackEntity.FeedbackId = model.FeedbackId;
                feedbackEntity.UserId = model.UserId;
                feedbackEntity.Rating = model.Rating;
                feedbackEntity.Comments = model.Comments;
                feedbackEntity.Feedbackdate = DateTime.Now;
                _healthindexdbcontext.FeedbackDetails.Add(feedbackEntity);
                _healthindexdbcontext.SaveChanges();
               messagemodel. message = GlobalConstants.FeedbackSavedSuccessfully;
            }
            return messagemodel;
        }

        public FeedbackCountModel GetFeedbackCount(ref ErrorResponseModel errorResponseModel)
        {
            FeedbackCountModel bjUserCount = new FeedbackCountModel();
            var count = _healthindexdbcontext.FeedbackDetails.ToList().Count();
            bjUserCount.FeedbackDetailCount = count;
            return bjUserCount;
        }

        public Feedbackcomment GetFeedbackById(long feedbackId, ref ErrorResponseModel errorResponseModel)
        {
            var feedbackEntity = _healthindexdbcontext.FeedbackDetails.Where(x => x.FeedbackId == feedbackId).FirstOrDefault();
            if (feedbackEntity == null)
            {
                errorResponseModel.StatusCode= HttpStatusCode.OK;
                errorResponseModel.Message = "Feedback not found";
                return null;
            }
            return new Feedbackcomment
            {
                Comments = feedbackEntity.Comments,

            };
        }
    }
}
