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
    public class QuestionnaireCategoryController : BaseAPIController
    {
        IQuestionnaireCategoryService _questionnairecategoryservice;

        public QuestionnaireCategoryController(IQuestionnaireCategoryService questionnairecategoryservice)
        {
            _questionnairecategoryservice = questionnairecategoryservice;
        }


        [HttpGet("GetQuestionnaireCategory")]
        [Authorize]
        [ProducesResponseType(typeof(QuestionnaireCategoryModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionnaireCategory()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionnairecategorymodelList = _questionnairecategoryservice.GetQuestionnaireCategory(ref errorResponseModel);

                if (questionnairecategorymodelList != null)
                {
                    return Ok(questionnairecategorymodelList);
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
