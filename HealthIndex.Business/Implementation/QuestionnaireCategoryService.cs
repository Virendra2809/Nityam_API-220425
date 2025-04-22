using HealthIndex.Business.Interface;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Implementation
{
    public class QuestionnaireCategoryService : IQuestionnaireCategoryService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public QuestionnaireCategoryService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
        public List<QuestionnaireCategoryModel> GetQuestionnaireCategory(ref ErrorResponseModel errorResponseModel)
        {
            {
                var questionnairecategorymodelList = new List<QuestionnaireCategoryModel>();
                errorResponseModel = new ErrorResponseModel();
                var questionnairecategoryEntityList = _healthindexdbcontext.QuestionnaireCategories.ToList();
                if (questionnairecategoryEntityList.Count == 0)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.OK;
                    errorResponseModel.Message = " Questionnaire Category not found";
                    return null;
                }

                questionnairecategoryEntityList.ForEach(item =>
                {
                    questionnairecategorymodelList.Add(new QuestionnaireCategoryModel
                    {
                        QuestionnaireCategoryId= item.QuestionnaireCategoryId,
                        QuestionnaireCategory1= item.QuestionnaireCategory1,
                    });
                });
                return questionnairecategorymodelList;
            }
        }
    }
}
