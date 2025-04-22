using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IQuestionMasterSevice
    {
        public List<QuestionMasterModel> GetQuestionSet(ref ErrorResponseModel errorResponseModel);
        public List<QuestionlistModel> GetAllQuestions(long QuestionMasterId, ref ErrorResponseModel errorResponseModel);

        public ResponseMessage UploadQuestionImage(UploadImage upload);

    }
}
