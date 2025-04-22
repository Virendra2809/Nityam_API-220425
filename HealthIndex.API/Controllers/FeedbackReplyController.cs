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
    public class FeedbackReplyController : BaseAPIController
    {
        IFeedbackReplyService _feedbackReplyService;

        public FeedbackReplyController(IFeedbackReplyService feedbackReplyService)
        {
            _feedbackReplyService = feedbackReplyService;
        }

        [HttpPost("AddFeedbackReply")]
        [Authorize]
        [ProducesResponseType(typeof(FeedbackReplyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddFeedbackReply(FeedbackReplyModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                Message feedbackEntity = _feedbackReplyService.AddFeedbackReply(model, ref errorResponseModel);

                if (feedbackEntity != null)
                {
                    var json = JsonConvert.SerializeObject(feedbackEntity);

                    return Ok(feedbackEntity);
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
