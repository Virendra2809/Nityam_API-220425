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
    public class QuestionSubscriptionService : IQuestionSubscriptionService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public QuestionSubscriptionService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
        public Message AddQuestionSubscription(QuestionSubscriptionModel model, ref ErrorResponseModel errorResponseModel)
        {
            Message messagemodel= new Message();
            var existingRecord = _healthindexdbcontext.AppUserMasters.Where(x => x.AppUserId==model.AppUserId && x.DeleteStatus==false).ToList();
            if (existingRecord == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                messagemodel.message  = GlobalConstants.ExistingUserMessage;
                return null;
            }
            else
            {
                var questionEntity = new QuestionSubscription();
                questionEntity.QuestionSubscriptionId = model.QuestionSubscriptionId;
                questionEntity.AppUserId = model.AppUserId;
                questionEntity.QuestionMasterId = model.QuestionMasterId;
                questionEntity.SubscriptionDate = DateTime.Now;
                questionEntity.IsActive = true;
                _healthindexdbcontext.QuestionSubscriptions.Add(questionEntity);
                _healthindexdbcontext.SaveChanges();

                messagemodel.message = GlobalConstants.QuestionSubscriptionSavedSuccessfully;
            }
            return messagemodel;
        }
    }
}
