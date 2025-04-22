using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface 
{
    public interface IPlanSubscription 
    {
        List<PlanSubscriptionModel> GetPlanSubscription( ref ErrorResponseModel errorResponseModel);
        public Message AddEditPlanSubscription(PlanSubscriptionModel model, ref ErrorResponseModel errorResponseModel);
        PlanSubscriptionModel GetPlanSubscriptionById(long PlanId, ref ErrorResponseModel errorResponseModel);



    }
}
