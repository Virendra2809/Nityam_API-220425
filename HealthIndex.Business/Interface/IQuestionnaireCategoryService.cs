using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IQuestionnaireCategoryService
    {
        public List<QuestionnaireCategoryModel> GetQuestionnaireCategory(ref ErrorResponseModel errorResponseModel);
    }
}