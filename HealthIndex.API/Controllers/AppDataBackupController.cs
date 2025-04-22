using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppDataBackupController : BaseAPIController
    {
        IAppDataBackupService _appDataBackupService;

        public AppDataBackupController(IAppDataBackupService appDataBackupService)
        {
            _appDataBackupService = appDataBackupService;
        }

        [HttpPost("AddAppDataBackup")]
      //[Authorize]
        [ProducesResponseType(typeof(AppDataBackupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
       public IActionResult AddAppDataBackup()
        {
            AppDataBackupModel model= new AppDataBackupModel();
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var request = HttpContext.Request;
                model. AppUserId = Convert.ToInt32(request.Form["appuserId"]);
                var photo = request.Form.Files["file"];
                string extn = Path.GetExtension(photo.FileName);
                var serverPath =AppDomain.CurrentDomain.BaseDirectory + "Resource/AppUserDataBackup/" + photo.FileName;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), serverPath);
                var file = System.IO.File.Create(pathToSave);
                photo.CopyTo(file);
                file.Close();
                model.BackupPath=pathToSave;
                ResponseMessage appdataEntity = _appDataBackupService.AddAppDataBackup(model, ref errorResponseModel);

                if (appdataEntity != null)
                {
                    var json = JsonConvert.SerializeObject(appdataEntity);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("RestoreBackup/{appUserId:int}")]
        public async Task<ActionResult> RestoreBackup(int appUserId)
        {
            ErrorResponseModel errorResponseModel = null;
            ResponseMessage response = new ResponseMessage();

            string message;
            var provider = new FileExtensionContentTypeProvider();
            string file = _appDataBackupService.GetFilePath(appUserId);

            if (string.IsNullOrEmpty(file))
            {
                message = "file not found.";
                var responseObj = new { message }; // Create anonymous object
                string jsonResponse = System.Text.Json.JsonSerializer.Serialize(responseObj); // Convert to JSON
                return Content(jsonResponse, "application/json"); // 
            }

            var filePath = file; // Here, you should validate the request and the existance of the file. var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
             _appDataBackupService.AppDataRestore(appUserId, ref errorResponseModel);
            return File( bytes,contentType, Path.GetFileName(filePath));
        }

        [Route("upload")]
        [HttpPost]
        public HttpResponseMessage upload()
        {

            try
            {               
                var request = HttpContext.Request;
                var appuserId = request.Form["appuserId"];
                var photo = request.Form.Files["file"];
                var serverPath = AppDomain.CurrentDomain.BaseDirectory + "Resource/AppUserDataBackup/" + photo.FileName;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),serverPath );
                photo.CopyTo(System.IO.File.Create(pathToSave));
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new  HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("GetFileName")]
        [ProducesResponseType(typeof(RestoreFileData), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFileName(int? appUserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var feedbackdetailList = _appDataBackupService.GetFileName(appUserId);

                if (feedbackdetailList != null)
                {
                    return Ok(feedbackdetailList);
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


   

    

