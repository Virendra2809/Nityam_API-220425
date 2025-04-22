using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ILanguageMasterservice
    {
        object Value { get; }
        List<LanguageModel> GetAll();
        string Add(LanguageModel model, ref ErrorResponseModel errorResponseModel);
        LanguageModel GetById(long LanguageId, ref ErrorResponseModel errorResponseModel);
        public bool Put(LanguageModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long LanguageId, ref ErrorResponseModel errorResponseModel);

    }
}
