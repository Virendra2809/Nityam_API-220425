using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface ISubscriptionOrderService
    {
        PlanSubscription GetPlanSubscription(ref ErrorResponseModel errorResponseModel);
        PlanSubscriptionList GetPromoCode(string PromoCode, ref ErrorResponseModel errorResponseModel);
        Task<OrderNoResponse> GenerateOrderNo(GenerateOrderNoModel generateOrderNoModel);
         Task<OrderNoResponse> GetPaymentReuestResponse(int appUserId, int OrderId);
        Task<bool> ResendPaymentLinkEmail(int OrderId);
        string PaymentTransaction(SubscriptionOrderModel model, ref ErrorResponseModel errorResponseModel);
        public List<TransactionRepotResponseModel> GetAllTransactionDetails(TransactionReportModel transactionReportModel, ref ErrorResponseModel errorResponseModel);
       Task<InstamojoResponse> GenerateLinkInstamojo(PaymentModel paymentModel);
        SubscriptionUserModel SubscriptionUserData(int OrderId);
        List<SubscriptionDataModel> SubscriptionData(DateTime FromDate,DateTime Todate);   
        string AddOrderStatus(UpdateOrderStatusModel model, ref ErrorResponseModel errorResponseModel);


    }
}
