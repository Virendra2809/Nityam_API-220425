using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        /// <summary>
        /// This will create an error response as per status 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customErrorMessage"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        protected internal IActionResult ReturnErrorResponse(ErrorResponseModel model, string customErrorMessage = null)
        {
            if (model != null)
            {
                //this line added for testing code clone is properly or not
                var errorMessage = string.IsNullOrEmpty(customErrorMessage) ? model.Message : customErrorMessage;
                switch (model.StatusCode)
                {
                    case HttpStatusCode.BadGateway:
                        return BadRequest(errorMessage);
                    case HttpStatusCode.ServiceUnavailable:
                        return StatusCode(StatusCodes.Status503ServiceUnavailable, GlobalConstants.Status503Message);
                    default:
                        return BadRequest(errorMessage);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
    }
}
