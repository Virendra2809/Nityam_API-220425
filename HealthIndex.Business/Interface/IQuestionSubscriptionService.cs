using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IQuestionSubscriptionService
    {
        public Message AddQuestionSubscription(QuestionSubscriptionModel model, ref ErrorResponseModel errorResponseModel);
    }
}
