
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class ResponseModel
    {
        //[JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        //[JsonProperty("message")]
        public string Message { get; set; }

        public string data { get; set; }
    }

}
