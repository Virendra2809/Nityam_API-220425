using CCA.Util;
using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;



namespace HealthIndex.Business.Implementation
{
    public class SubscriptionOrderService : ISubscriptionOrderService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public SubscriptionOrderService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
        public PlanSubscription GetPlanSubscription(ref ErrorResponseModel errorResponseModel)
        {
            PlanSubscription planSubscription = new PlanSubscription();
            ResponseMessage responseMessage = new ResponseMessage();
            errorResponseModel = new ErrorResponseModel();
           // var PlanSubscriptionEntityList = _healthindexdbcontext.SubscriptionPlans.ToList();

            var PlanSubscriptionEntityList = _healthindexdbcontext.SubscriptionPlans
            // .Where(plan => plan.OfferCode == null)
             .ToList();

            if (PlanSubscriptionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Feedback Details not found";
                return null;
            }
            else
            {
                var PlanSubscriptionModelList = PlanSubscriptionEntityList
                    .Where(x => x.Status == true)
                    .Select(planEntity => new PlanSubscriptionList
                    {
                        PlanId = planEntity.PlanId,
                        Code = planEntity.Code,
                        OfferCode = planEntity.OfferCode.Trim(), // Assuming OfferCode is a string
                        Amount = planEntity.Amount, // Assuming Amount is not a string
                        Name = planEntity.Name,
                        Validity = planEntity.Validity,
                    }).ToList();

                planSubscription.planSubscriptionLists = PlanSubscriptionModelList;
            }
            return planSubscription;
        }
        public PlanSubscriptionList GetPromoCode(string PromoCode, ref ErrorResponseModel errorResponseModel)
        {
            PlanSubscriptionList planSubscriptionModel = new PlanSubscriptionList();

            var PlanEntity = (from planEntity in _healthindexdbcontext.SubscriptionPlans
                              where 
                              //planEntity.OfferCode == PromoCode
                              EF.Functions.Collate(planEntity.OfferCode, "SQL_Latin1_General_CP1_CS_AS") == PromoCode // Case-sensitive comparison
                               && planEntity.Status == true
                              select new PlanSubscriptionList
                              {
                                  PlanId = planEntity.PlanId,
                                  Code = planEntity.Code,
                                  OfferCode = planEntity.OfferCode.Trim(),
                                  Amount = planEntity.Amount.Trim(),
                                  Name = planEntity.Name,
                                  Validity = planEntity.Validity,
                              }).FirstOrDefault();

            if (PlanEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Plan not found";
                return null;
            }
            else
            {
                planSubscriptionModel = PlanEntity;
            }

            return planSubscriptionModel;
        }
        public async Task<OrderNoResponse> GenerateOrderNo(GenerateOrderNoModel generateOrderNoModel)
        {
          OrderNoResponse orderNoResponse_ = new OrderNoResponse();
          InstamojoResponse paymentModel_ = new InstamojoResponse();

            try
            {
                long OrdreNo = 0;
                SubscriptionOrder subscriptionOrder = new SubscriptionOrder();

                var AppUserMastersEntity = _healthindexdbcontext.AppUserMasters
                                     .FirstOrDefault(appuserData_ => appuserData_.AppUserId == generateOrderNoModel.AppUserId);

                var planEntity = _healthindexdbcontext.SubscriptionPlans.FirstOrDefault(c => c.PlanId == generateOrderNoModel.PlanId);
                

                if (AppUserMastersEntity != null)
                {
                    // Generate a new order number 
                    long newOrderNo = GenerateNewOrderNumber();
                    
                    var queryParameter = new CCACrypto();
                    string invoiceNumber = queryParameter.Encrypt(newOrderNo.ToString(), "BE9F83432E505554F9F855360DBFB3D3");
                    // Update the entity

                    AppUserMastersEntity.OrderId = newOrderNo;

                    // Save changes to the database
                    _healthindexdbcontext.SaveChanges();


                    string PaymentLink = "https://nityamapp.com/home/Payment/" + invoiceNumber;
                    string Sms = "Your Order Number is " + newOrderNo + ". Please click the given below Payment link to make a payment: " + PaymentLink;

                    string urlpayment = "https://secure.ccavenue.com/transaction.do?command=initiateTransaction&encRequest=";
                    string accesscode = "&access_code=" + "AVRJ54LB02AB86JRBA";

                    string Encrptdata = urlpayment + (queryParameter.Encrypt
                   (BuildCcAvenueRequestParameters(newOrderNo.ToString(), planEntity.Amount.Trim()), "BE9F83432E505554F9F855360DBFB3D3")) + accesscode;

                    //subscriptionOrder Update

                    subscriptionOrder.Amount = Convert.ToDecimal(planEntity.Amount);
                    subscriptionOrder.AppUserId = generateOrderNoModel.AppUserId;
                    subscriptionOrder.OrderDate = DateTime.Now;
                    // ---------------- both are update in pyment Sucessfully. ---------------------//

                    subscriptionOrder.PlanId = generateOrderNoModel.PlanId;
                    subscriptionOrder.ValidityDays = planEntity.Validity;                   
                    subscriptionOrder.TransactionReferanceNo = "";
                    subscriptionOrder.OrderNo = newOrderNo;
                    subscriptionOrder.OrderStatus = "Failure";
                    subscriptionOrder.PaymentLink = PaymentLink;
                    subscriptionOrder.PaymentEncrData = Encrptdata;

                    _healthindexdbcontext.SubscriptionOrders.Add(subscriptionOrder);
                    _healthindexdbcontext.SaveChanges();

                    //---------------response-----------------//
                    orderNoResponse_.status = "";
                    orderNoResponse_.OrderNo = newOrderNo;
                    orderNoResponse_.RequestData = Encrptdata;
                    orderNoResponse_.Amount = planEntity.Amount.Trim();
                    orderNoResponse_.Validity = planEntity.Validity;
                    orderNoResponse_.PlanName = planEntity.Name;
                    orderNoResponse_.ExpiryDate = Convert.ToDateTime(AppUserMastersEntity.ExpiryDate).ToString("dd-MM-yyyy");
                    orderNoResponse_.Total = "";
                    orderNoResponse_.paymentThrough = "Email";
                    //--------------------------------------//

                    SendEmail(AppUserMastersEntity.EmailId, "Payment Link", Sms);

                }
                else
                {
                    // Handle the case where the user is not found              
                }
            }
            catch (Exception)
            {

                throw;
            }

           
            
            return orderNoResponse_;
        }
        private string BuildCcAvenueRequestParameters(string invoiceNumber, string amount)
        {
         long?   invoiceNumbers = (long?)(Convert.ToInt64(invoiceNumber));

            string strTid = DateTime.Now.ToString("yyyyMMddHHmmss") + invoiceNumber;

            var AppUserMastersEntity = _healthindexdbcontext.AppUserMasters
                                   .FirstOrDefault(appuserData_ => appuserData_.OrderId == invoiceNumbers);

            var queryParameters = new Dictionary<string, string>
             {
             {"billing_email",AppUserMastersEntity.EmailId },
             {"billing_name", AppUserMastersEntity.FirstName},
             {"billing_tel", AppUserMastersEntity.MobileNo},
             {"tid",strTid },
             {"order_id", invoiceNumber},
             {"merchant_id", "3215758"},
             {"amount", amount},
             {"currency","INR" },
             {"redirect_url","https://nityamapp.com/Home/PaymentSuccessful" },
             {"cancel_url","https://nityamapp.com/Home/PaymentSuccessful"},
             {"request_type","JSON" },
             {"response_type","JSON" },
             {"version","1.1" }
        }.Select(item => string.Format("{0}={1}", item.Key, item.Value));
            return string.Join("&", queryParameters);
        }

        private long GenerateNewOrderNumber()
        {
            // Assuming you want a combination of date and a unique identifier for the order number
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 6); // Using a portion of a GUID for uniqueness

            string newOrderNumber = uniquePart;

            // Using a hash function (e.g., GetHashCode()) to convert the string into a long
            long hashedOrderNumber = newOrderNumber.GetHashCode();

            // Take the absolute value to remove the negative sign
            long absoluteOrderNumber = Math.Abs(hashedOrderNumber);

            return absoluteOrderNumber;
        }

        //paymenttrue = Email;
        public string PaymentTransaction(SubscriptionOrderModel model, ref ErrorResponseModel errorResponseModel)
        {
            try
            {
                if (model.SubscriptionOrderId == 0)
                {
                    // Creating a new subscription order
                    SubscriptionOrder subscriptionOrder = new SubscriptionOrder
                    {
                        AppUserId = model.AppUserId,
                        OrderDate = model.OrderDate,
                        PlanId = model.PlanId,
                        ValidityDays = model.ValidityDays,
                        Amount = model.Amount,
                        TransactionToken = model.TransactionToken,
                        ModeOfPayment = model.ModeOfPayment,
                        TransactionReferanceNo = model.TransactionReferanceNo,
                        OrderStatus = model.OrderStatus
                    };

                    _healthindexdbcontext.SubscriptionOrders.Add(subscriptionOrder);
                    _healthindexdbcontext.SaveChanges();
                }
                else
                {
                    // Updating an existing subscription order
                    var existingOrder = _healthindexdbcontext.SubscriptionOrders
                        .FirstOrDefault(o => o.SubscriptionOrderId == model.SubscriptionOrderId);

                    if (existingOrder != null)
                    {
                        // Update properties
                        existingOrder.AppUserId = model.AppUserId;
                        existingOrder.OrderDate = model.OrderDate;
                        existingOrder.PlanId = model.PlanId;
                        existingOrder.ValidityDays = model.ValidityDays;
                        existingOrder.Amount = model.Amount;
                        existingOrder.TransactionToken = model.TransactionToken;
                        existingOrder.ModeOfPayment = model.ModeOfPayment;
                        existingOrder.TransactionReferanceNo = model.TransactionReferanceNo;
                        existingOrder.OrderStatus = model.OrderStatus;

                        // Save changes to the database
                        _healthindexdbcontext.SaveChanges();
                    }
                    else
                    {
                        // Handle case where the order is not found
                        throw new InvalidOperationException("Subscription order not found.");
                    }
                }

                // Return a success message or identifier if needed
                return "Payment transaction completed successfully";
            }
            catch (InvalidOperationException ex)
            {
                // Handle the exception according to your specific requirements
                Console.WriteLine($"Error processing payment transaction: {ex.Message}");

                // Set the error response model
                errorResponseModel.Message = ex.Message;

                // You might log the exception, notify administrators, etc.

                // Return an error message or identifier if needed
                return "Payment transaction failed";
            }
        } 
        public async Task<InstamojoResponse> GenerateLinkInstamojo(PaymentModel paymentModel)
        {
            InstamojoResponse paymentModel_ = new InstamojoResponse();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,

                   RequestUri = new Uri("https://api.instamojo.com/oauth2/token/"),

               // RequestUri = new Uri("https://test.instamojo.com/oauth2/token/"),
                Headers =  {
                                { "accept", "application/json" },
                            },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                            {
                                { "grant_type", "client_credentials" },
                                //{ "client_id", "test_7A7bbAqr5TL3ls4M5LlaZDmnW5NoZSeLDzG" },
                                //{ "client_secret", "test_KRogLQbRSfFnUq6VoOg5TqSDeolm9Q3yAxKlosMs8cilHGAcAlFbDHE8i5NJgZWs0JzQ0R6nMLqtrgZ9yUb5V1IfQMaO2KK6p6BCR3ZhrIeY3rzhiyR4P5mDM72" },
                              { "client_id", "gNpkedG4p7ijc5he9vQp6gROzRsX7PeTu83b7Crx"},
                              { "client_secret", "rCtbK8nYqdOSmjIPpcAUiFujQ5TALtwP2Ks4aIoEPjNePSvLlziS3vGm2yOI16ze14kxIUTIzbMSYabftO4gY2XLJlhh3cACD8MnSAi4sCie3l00W34P6oc1rCQ5TTob" },
                            
                }),
            };
            InstamojoAccessTokenResponse accessTokenResponse;

            using (var Tokenresponse = await client.SendAsync(request))
            {
                Tokenresponse.EnsureSuccessStatusCode();
                var body = await Tokenresponse.Content.ReadAsStringAsync();

                // Deserialize the JSON response into the InstamojoAccessTokenResponse class
                accessTokenResponse = JsonConvert.DeserializeObject<InstamojoAccessTokenResponse>(body);

                // Access the properties of the deserialized object
                Console.WriteLine("Access Token: " + accessTokenResponse.access_token);
                Console.WriteLine("Expires In: " + accessTokenResponse.expires_in);
                Console.WriteLine("Scope: " + accessTokenResponse.scope);
                Console.WriteLine("Token Type: " + accessTokenResponse.token_type);

            }
            
            var paymentRquest = new HttpRequestMessage
            {

                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.instamojo.com/v2/payment_requests/"),
               // RequestUri = new Uri("https://test.instamojo.com/v2/payment_requests/"),
                Headers = {
                    { "accept", "application/json" },
                    { "Authorization","Bearer "+accessTokenResponse.access_token },
                },

                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {

                    {"email" , paymentModel.Email},
                    { "phone" , paymentModel.Phone},
                    { "amount", paymentModel.Amount },
                    { "purpose", paymentModel.Purpose },
                    { "send_email", "true" },
                    { "redirect_url","http://healthui.meshbagroup.com/#/loginpage"}
                }),
            };

            var response = await client.SendAsync(paymentRquest);
            //using ( response = await client.SendAsync(paymentRquest))
            // {
            //     response.EnsureSuccessStatusCode();
            //     var body = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(body);
            // }


            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse the response body or handle it as needed to populate your PaymentModel
            // For example, you might use JsonConvert.DeserializeObject to deserialize the JSON response
            paymentModel_ = JsonConvert.DeserializeObject<InstamojoResponse>(responseBody);

            // You may want to set properties of paymentModel_ based on the response

            return paymentModel_;
        }
        static void SendEmail(string to, string subject, string body)
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
              
                    smtpClient.Credentials = new NetworkCredential("dipalivarute96@gmail.com", "dufmtrwjbthlasfp");
                    smtpClient.EnableSsl = true;
                 
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("dipalivarute96@gmail.com");
                        mailMessage.To.Add(to);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false;

                        smtpClient.Send(mailMessage);
                    }
                }
            }
        public List<TransactionRepotResponseModel> GetAllTransactionDetails(TransactionReportModel transactionReportModel, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var AuditorModel = (from auditor in _healthindexdbcontext.SubscriptionOrders
                                join appuser in _healthindexdbcontext.AppUserMasters
                                on auditor.AppUserId equals appuser.AppUserId
                                join subplan in _healthindexdbcontext.SubscriptionPlans
                             on appuser.PlanId equals subplan.PlanId
                                where auditor.OrderDate >= transactionReportModel.FromDate && auditor.OrderDate <= transactionReportModel.ToDate

                                select new TransactionRepotResponseModel
                                {
                                 //   SubscriptionOrderId = auditor.SubscriptionOrderId,
                                    AppUserId = auditor.AppUserId,
                                    OrderDate =auditor.OrderDate,
                                    PlanCode = subplan.Code,
                                    ValidityDays =auditor.ValidityDays,
                                    Amount = auditor.Amount,
                                    TransactionToken = auditor.TransactionToken,
                                    ModeOfPayment = auditor.ModeOfPayment,
                                    Name = appuser.FirstName + appuser.LastName,
                                    OrderNo = appuser.OrderId,
                                    MobileNo = appuser.MobileNo,
                                    OrderStatus = auditor.OrderStatus


    }).ToList();
            if (AuditorModel.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
            }
            return AuditorModel.ToList();
        }
        public async Task<OrderNoResponse> GetPaymentReuestResponse(int appUserId ,int OrderId)
        {
            OrderNoResponse orderNoResponse = new OrderNoResponse();
            try
            {
                string Ordstatus = "";

                var SubscriptionOrderEntity = (from order_ in _healthindexdbcontext.SubscriptionOrders
                                               where order_.AppUserId == appUserId && order_.OrderNo == OrderId
                                               select order_).FirstOrDefault();


                var paymentData = (from appuser_ in _healthindexdbcontext.AppUserMasters
                                   join order_ in _healthindexdbcontext.SubscriptionOrders
                                       on appuser_.AppUserId equals order_.AppUserId
                                   join plan_ in _healthindexdbcontext.SubscriptionPlans
                                       on order_.PlanId equals plan_.PlanId
                                   where appuser_.AppUserId == appUserId && appuser_.OrderId == OrderId
                                   orderby order_.SubscriptionOrderId // Order by a unique identifier
                                   select new OrderNoResponse
                                   {
                                       RequestData = order_.PaymentEncrData,
                                       OrderNo = appuser_.OrderId,
                                       status = order_.OrderStatus.Trim(),
                                       //email / Mobile = collection Mode : mobile
                                       //provid link with change transaction id with encrpted
                                       ExpiryDate = Convert.ToDateTime(appuser_.ExpiryDate).ToString("dd-MM-yyyy"),
                                       Validity = plan_.Validity,
                                       PlanName = plan_.Name,
                                       Amount = plan_.Amount,
                                       paymentThrough = "Mobile",
                                       

                                   }).LastOrDefault();

             

                string PaymentLink = "";
                string Sms = "";
           
                if (paymentData != null && paymentData.status != "success")
                {
                   var queryParameter = new CCACrypto();
                
                    string urlpayment = "https://secure.ccavenue.com/transaction.do?command=initiateTransaction&encRequest=";
                    string accesscode = "&access_code=" + "AVRJ54LB02AB86JRBA";

                
                    string Encrptdata = urlpayment + (queryParameter.Encrypt(BuildCcAvenueRequestParameters(paymentData.OrderNo.ToString(), paymentData.Amount.Trim()), "BE9F83432E505554F9F855360DBFB3D3")) + accesscode;

                    SubscriptionOrderEntity.PaymentEncrData = Encrptdata;
                    _healthindexdbcontext.SaveChanges();

                    paymentData.RequestData = Encrptdata;
                    

                }



                orderNoResponse = paymentData;

              

                return orderNoResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> ResendPaymentLinkEmail(int OrderId)
        {
            try
            {
                var paymentlink = await (from appuser_ in _healthindexdbcontext.AppUserMasters
                                         join order_ in _healthindexdbcontext.SubscriptionOrders
                                             on appuser_.AppUserId equals order_.AppUserId
                                         where order_.OrderNo == OrderId
                                         select new
                                         {
                                             Email = appuser_.EmailId,
                                             UserPaymentLink = order_.PaymentLink,
                                         }).FirstOrDefaultAsync();

                string to = paymentlink.Email;  // Corrected variable name
                string subject = "Resend Email";  // Provide a meaningful subject
                string body = paymentlink.UserPaymentLink;
                SendEmail(to,subject,body);

                // Your asynchronous email sending logic here
                // You may use a library like SmtpClient or another email service

                // Assuming the email was sent successfully
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or update the errorResponseModel
                return false;
            }
        }
        public SubscriptionUserModel SubscriptionUserData(int OrderId)
        {
            SubscriptionUserModel subscriptionUserModel = new SubscriptionUserModel();


             subscriptionUserModel = (from appuser_ in _healthindexdbcontext.AppUserMasters
                                      join plan_ in _healthindexdbcontext.SubscriptionPlans
                                      on appuser_.PlanId equals plan_.PlanId
                                      join SubO_ in _healthindexdbcontext.SubscriptionOrders
                                      on appuser_.AppUserId equals SubO_.AppUserId
                                     where SubO_.OrderNo == OrderId
                                    select new SubscriptionUserModel
                                    {
                                     Name = appuser_.FirstName + appuser_.LastName,
                                     Email = appuser_.EmailId,
                                     MobileNumber = appuser_.MobileNo,
                                     Amount = plan_.Amount
                                    }).FirstOrDefault();


            return subscriptionUserModel;
        }
        public List<SubscriptionDataModel> SubscriptionData(DateTime FromDate, DateTime Todate)
        {
            List<SubscriptionDataModel> subscriptionUserModel = new List<SubscriptionDataModel>();


            var allData = (from appuser_ in _healthindexdbcontext.AppUserMasters
                           join sibscribtion_ in _healthindexdbcontext.SubscriptionOrders
                           on appuser_.AppUserId equals sibscribtion_.AppUserId
                           join plan_ in _healthindexdbcontext.SubscriptionPlans
                           on sibscribtion_.PlanId equals plan_.PlanId
                           where sibscribtion_.OrderDate.Value.Date >= FromDate.Date
                           && sibscribtion_.OrderDate.Value.Date <= Todate.Date
                           select new SubscriptionDataModel
                           {
                               SubscriptionOrderId = sibscribtion_.SubscriptionOrderId,
                               Name = appuser_.FirstName + " " + appuser_.LastName,
                               Email = appuser_.EmailId,
                               MobileNumber = appuser_.MobileNo,
                               Plan = plan_.Name,
                               AppUserId = appuser_.AppUserId,
                               OrderStatus = sibscribtion_.OrderStatus.Trim(),
                               OrderId = appuser_.OrderId,
                               Amount = plan_.Amount,
                               Date = sibscribtion_.OrderDate
                           }).ToList();

            var paymentlink = allData.GroupBy(x => new { x.AppUserId, x.OrderId })
                                     .SelectMany(group => group.Where(item => item.OrderStatus == "Success"))
                                     .ToList().DistinctBy(x => x.AppUserId);

            var skippedData = allData.Where(item => !paymentlink.Select(pl => pl.SubscriptionOrderId).Contains(item.SubscriptionOrderId)).ToList();
          
             subscriptionUserModel = paymentlink.Concat(skippedData).ToList();

            return subscriptionUserModel;
        }

        public string AddOrderStatus(UpdateOrderStatusModel model, ref ErrorResponseModel errorResponseModel)
        {
            string result = "F";

            var AppUserMastersEntity = _healthindexdbcontext.AppUserMasters
                                       .FirstOrDefault(appuserData_ => appuserData_.OrderId == model.OrderId);

            var existingOrder = (from suborder in _healthindexdbcontext.SubscriptionOrders
                                 where suborder.OrderNo == model.OrderId && suborder.AppUserId == model.AppUserId
                                 select suborder).FirstOrDefault();

            DateTime newExpiryDate;
            if (AppUserMastersEntity.ExpiryDate >= DateTime.Today.Date )
            {
                 newExpiryDate = AppUserMastersEntity.ExpiryDate.Value.AddDays((int)existingOrder.ValidityDays);
            }
            else
            {
                 newExpiryDate = DateTime.Today.Date.AddDays((int)existingOrder.ValidityDays);
            }

           

            if (existingOrder != null && existingOrder.OrderStatus.Trim() != "Success")
            {
                if (existingOrder.ValidityDays > 0 || existingOrder.ValidityDays != null)
                {
                    if (AppUserMastersEntity != null)
                    {
                        AppUserMastersEntity.ExpiryDate = newExpiryDate;
                        AppUserMastersEntity.OrderId = model.OrderId;
                        AppUserMastersEntity.PlanId = existingOrder.PlanId;

                        _healthindexdbcontext.SaveChanges();
                    }
                }


                existingOrder.ModeOfPayment = model.ModeOfPayment;          
                existingOrder.OrderStatus = "Success";               
                _healthindexdbcontext.SaveChanges();

                result = "T";
            }
            else
            {
                result = "F";
            }

            return result;

        }

       
    }
}





