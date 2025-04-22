using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthIndex.API.Controllers
{
    public class SubscriptionOrderController : BaseAPIController
    {
        ISubscriptionOrderService _SubscriptionOrderService;
        public SubscriptionOrderController(ISubscriptionOrderService subscriptionOrderService)
        {
            _SubscriptionOrderService = subscriptionOrderService;
        }

        [HttpGet("PlanList")]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionList), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPlanSubscription()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var PlanList = _SubscriptionOrderService.GetPlanSubscription(ref errorResponseModel);

                if (PlanList != null)
                {
                    return Ok(PlanList);
                }
                var json = JsonConvert.SerializeObject(PlanList);
                return Ok(PlanList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPost("AddEditPlanSubscription")]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PaymentTransaction(SubscriptionOrderModel model )
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                string paymentTransaction = _SubscriptionOrderService.PaymentTransaction(model, ref errorResponseModel);

                if (paymentTransaction != null)
                {
                    var json = JsonConvert.SerializeObject(paymentTransaction);
                    return Ok(paymentTransaction);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetPromoCode")]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPromoCode(string PromoCode)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var PromoCodeResponse = _SubscriptionOrderService.GetPromoCode(PromoCode, ref errorResponseModel);

                if (PromoCodeResponse != null)
                {
                    return Ok(PromoCodeResponse);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost("GenerateOrderNo")]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GenerateOrderNo(GenerateOrderNoModel generateOrderNoModel)
        {
            OrderNoResponse orderNoResponse = new OrderNoResponse();
            ErrorResponseModel errorResponseModel = null;
            try
            {
                orderNoResponse = await _SubscriptionOrderService.GenerateOrderNo(generateOrderNoModel);

                if (orderNoResponse != null)
                {
                    return Ok(orderNoResponse );
                }
                else
                {

                    // If paymentTransaction is null, return an error response
                    return ReturnErrorResponse(errorResponseModel);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpGet("OrderStatus")]
        [Authorize]
        [ProducesResponseType(typeof(OrderNoResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> OrderStatus(int appUserId, int OrderId)
        {
            OrderNoResponse orderNoResponse = new OrderNoResponse();
            ErrorResponseModel errorResponseModel = null;
            try
            {
                orderNoResponse = await _SubscriptionOrderService.GetPaymentReuestResponse(appUserId, OrderId);

                if (orderNoResponse != null)
                {
                    return Ok(orderNoResponse);
                }
                else
                {
                    // If paymentTransaction is null, return an error response
                    return ReturnErrorResponse(errorResponseModel);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPost("ResendPaymentLinkEmail")]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> ResendPaymentLink(int orderId)
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();

            bool isEmailSent = await _SubscriptionOrderService.ResendPaymentLinkEmail(orderId);

            if (isEmailSent)
            {
                // Email sent successfully
               // return Ok(new {);
                return Ok(new { EmailSend = isEmailSent });
            }
            else
            {
                // Handle email sending failure
                return BadRequest("Failed to send email");
            }
        }

        [HttpPost("GenerateLinkInstamojo")]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GenerateLinkInstamojo(PaymentModel paymentModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var paymentTransaction = _SubscriptionOrderService.GenerateLinkInstamojo(paymentModel);

                if (paymentTransaction != null)
                {
                    // Create an anonymous object with a property "Orderno"
                    var result = new { Link = paymentTransaction };

                    // Convert the anonymous object to JSON
                    var json = JsonConvert.SerializeObject(result);

                    // Return the JSON in the response
                    return Ok(json);
                }

                // If paymentTransaction is null, return an error response
                return ReturnErrorResponse(errorResponseModel);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        //[HttpPost("GenerateLinkInstamojo")]
        //[Authorize]
        //[ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public async Task<IActionResult> YourAction()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Post,
        //            RequestUri = new Uri("https://www.instamojo.com/api/1.1/payment-requests/"),
        //            Headers =
        //        {
        //            { "accept", "application/json" },
        //        },
        //            Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //        {
        //            { "allow_repeated_payments", "true" },
        //            { "send_email", "false" },
        //            { "send_sms", "false" },
        //        }),
        //        };

        //        using (var response = await client.SendAsync(request))
        //        {
        //            response.EnsureSuccessStatusCode();
        //            var body = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(body);
        //        }
        //    }

        //    // You can return a response or perform additional actions as needed.
        //    return Ok();
        //}

        //public ActionResult RedirectPage()
        //{
        //     Replace "https://www.example.com" with your desired redirect URL
        //    string redirectUrl = "http://healthui.meshbagroup.com";

        //     Perform the redirect
        //    return Redirect(redirectUrl);
        //}

        [HttpPost("TransactionReportExcel")]
        public IActionResult DownloadAuditorLisTransactionReportExcelt(TransactionReportModel transactionReportModel)
        {

            try
            {
                ErrorResponseModel errorResponseModel = null;
                string reportname = $"TransactionReport.xlsx";
                var auditorDataList = _SubscriptionOrderService.GetAllTransactionDetails(transactionReportModel, ref errorResponseModel);
                if (auditorDataList.Count > 0)
                {
                    var exportbytes = ExporttoExcel<TransactionRepotResponseModel>(auditorDataList, reportname);
                    return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
                }
                else
                {
                    var exportbytes = ExporttoExcel<TransactionRepotResponseModel>(auditorDataList, reportname);
                    return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
                }
            }
            catch (Exception)
            {

                throw;
            }
           



        }

        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using OfficeOpenXml.ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            // Load data with formatting for date columns
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var columnNumber = i + 1;
                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    ws.Column(columnNumber).Style.Numberformat.Format = "dd-MMM-yyyy";
                }
                // Set cell width (adjust the value as needed)
                ws.Column(columnNumber).Width = 25; // 15 is the width in characters
            }
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            // Set row height (adjust the value as needed)
            ws.DefaultRowHeight = 20; // 20 is the height in points
            return pack.GetAsByteArray();
        }


        [HttpPost("SubscriptionUserData")]
      
        [ProducesResponseType(typeof(SubscriptionUserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> SubscriptionUserData(int orderId)
        {
            SubscriptionUserModel subscriptionUserModel = new SubscriptionUserModel();

            subscriptionUserModel = _SubscriptionOrderService.SubscriptionUserData(orderId);

            if (subscriptionUserModel !=null)
            {               
                return Ok(subscriptionUserModel);
            }
            else
            {
                // Handle email sending failure
                return BadRequest("Failed to send email");
            }
        }



        [HttpGet("SubscriptionData")]
        [Authorize]
        [ProducesResponseType(typeof(SubscriptionDataModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> SubscriptionData(DateTime FromDate , DateTime ToDate)
        {
          //  SubscriptionDataModel subscriptionUserModel = new SubscriptionDataModel();

         var   subscriptionUserModel =  _SubscriptionOrderService.SubscriptionData(FromDate, ToDate);

            if (subscriptionUserModel != null)
            {
                return Ok(subscriptionUserModel);
            }
            else
            {
                // Handle email sending failure
                return BadRequest("Failed to send email");
            }
        }


        [HttpPost("AddOrderStatus")]
        [Authorize]
        [ProducesResponseType(typeof(UpdateOrderStatusModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddOrderStatus(UpdateOrderStatusModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                Message message = new Message();
                string paymentTransaction = _SubscriptionOrderService.AddOrderStatus(model, ref errorResponseModel);

                if (paymentTransaction == "T")
                {
                    var json = JsonConvert.SerializeObject(paymentTransaction);
                    message.message = "User offline Payment Successfully done";
                    return Ok(message);
                }
                else
                {
                    message.message = "User offline Payment Failed maybe User Status Already Sucess";
                    return BadRequest(message);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


    }
}
