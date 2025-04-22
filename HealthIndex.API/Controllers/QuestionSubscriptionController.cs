using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
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
    public class QuestionSubscriptionController : BaseAPIController
    {
        IQuestionSubscriptionService _IquestionSubscriptionService;

        public QuestionSubscriptionController(IQuestionSubscriptionService questionSubscriptionService)
        {
            _IquestionSubscriptionService = questionSubscriptionService;
        }

        [HttpPost("AddQuestionSubscription")]
        [Authorize]
        [ProducesResponseType(typeof(QuestionSubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddQuestionSubscription(QuestionSubscriptionModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try  
            {
                Message questmodel = _IquestionSubscriptionService.AddQuestionSubscription(model, ref errorResponseModel);

                if (questmodel != null)
                {
                    var json = JsonConvert.SerializeObject(questmodel);
                    return Ok(questmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}
