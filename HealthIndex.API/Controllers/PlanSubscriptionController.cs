using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanSubscriptionController : BaseAPIController
    {
        IPlanSubscription _IPlanSubscription;

        public PlanSubscriptionController(IPlanSubscription PlanSubscription)
        {
            _IPlanSubscription = PlanSubscription;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPlanSubscription()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var feedbackdetailModelList = _IPlanSubscription.GetPlanSubscription( ref errorResponseModel);

                if (feedbackdetailModelList != null)
                {
                    return Ok(feedbackdetailModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
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
        public IActionResult AddEditPlanSubscription(PlanSubscriptionModel model)
        {

            ErrorResponseModel errorResponseModel = null;
            try
            {
                Message feedbackEntity = _IPlanSubscription.AddEditPlanSubscription(model, ref errorResponseModel);

                if (feedbackEntity != null)
                {
                    var json = JsonConvert.SerializeObject(feedbackEntity);
                    return Ok(feedbackEntity);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetPlanSubscriptionById/{PlanId}")]
        [ProducesResponseType(typeof(PlanSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPlanSubscriptionById(long PlanId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var feedbackModel = _IPlanSubscription.GetPlanSubscriptionById(PlanId, ref errorResponseModel);

                if (feedbackModel != null)
                {
                    return Ok(feedbackModel);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}
