using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Model
{
    public class QuestionMasterModel
    {
        public int QuestionMasterId { get; set; }
        public string ConsultantName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string QuestionnaireCategory1 { get; set; }
        public bool? IsSelfAssessment { get; set; }
        public int? RegistrationNo { get; set; }

    }

   

   public class QuestionlistModel
    {
        public string Question1 { get; set; }
        public string QuestionImageName { get; set; }
        public string QuestionImageUrl { get; set; }

    }

    public class UploadImage
    {
        public int QuestionId { get; set; }
        public IFormFile QuestionImageUrl { get; set; }


    }
}
