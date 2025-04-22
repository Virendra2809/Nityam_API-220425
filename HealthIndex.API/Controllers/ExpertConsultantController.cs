using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace HealthIndex.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ExpertConsultantController : BaseAPIController
    {
        IUserModelService _userService;
        private readonly IWebHostEnvironment iwebhostingEnvironment;
        private readonly string filePath;

        private readonly IOptions<SmtpSettingModel> _emailSettings;

       
    }
}
