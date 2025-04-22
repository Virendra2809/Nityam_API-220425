using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace HealthIndex.Business.Implementation
{
    public class QuestionMasterSevice : IQuestionMasterSevice
    {
        HealthIndexDbContext _healthindexdbcontext;

        public QuestionMasterSevice(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }

       
        public List<QuestionlistModel> GetAllQuestions(long QuestionMasterId, ref ErrorResponseModel errorResponseModel)
        {
            
            var questionsModelList = new List<QuestionlistModel>();
            errorResponseModel = new ErrorResponseModel();
            var questionmasterEntityList = (from questionmaster in _healthindexdbcontext.QuestionMasters
                                            join
                                            que in _healthindexdbcontext.Questions
                                            on questionmaster.QuestionMasterId equals que.QuestionMasterId
                                            where que.QuestionMasterId==QuestionMasterId
                                            select new
                                            {
                                                que.Question1,
                                                que.SeqNo,
                                                que.QuestionImageUrl,
                                                que.QuestionImageName
                                            }
                                            ).OrderBy(que=>que.SeqNo).ToList();
            if (questionmasterEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = GlobalConstants.QuestionsListnotfound;
                return null;
            }

            questionmasterEntityList.ForEach(item =>
            {
                questionsModelList.Add(new QuestionlistModel
                {
                    Question1=item.Question1,  
                    QuestionImageName=item.QuestionImageName,
                    QuestionImageUrl= item.QuestionImageUrl,
                    
                });
            });
            return questionsModelList;
             
        } 

        public List<QuestionMasterModel> GetQuestionSet(ref ErrorResponseModel errorResponseModel)
        {
            var questionmastermodelList = new List<QuestionMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var questionmasterEntityList = (from que in _healthindexdbcontext.QuestionMasters
                                            join
                                            consult in _healthindexdbcontext.ConsultantDetails
                                            on que.ConsultantId equals consult.ConsultantId
                                            join 
                                            category in _healthindexdbcontext.QuestionnaireCategories
                                            on que.QuestionnaireCategoryId equals category.QuestionnaireCategoryId
                                            
                                            select new
                                            {
                                                que.QuestionMasterId,
                                                consult.ConsultantName,    
                                                consult.EmailId,
                                                category.QuestionnaireCategory1,
                                                consult.MobileNo,
                                                                                           
                                            }
                                           ).ToList();
            if (questionmasterEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Question Master Details not found";
                return null;
            }

            questionmasterEntityList.ForEach(item =>
            {
                questionmastermodelList.Add(new QuestionMasterModel
                {
                    QuestionMasterId = item.QuestionMasterId,
                    ConsultantName = item.ConsultantName,
                    EmailId = item.EmailId,
                    QuestionnaireCategory1 = item.QuestionnaireCategory1,
                    MobileNo = item.MobileNo,
                    IsSelfAssessment = false

                }) ;
            });
            return questionmastermodelList;
        }

        public ResponseMessage UploadQuestionImage(UploadImage upload)
        {
            ResponseMessage messagemodel = new ResponseMessage();
            if (upload.QuestionImageUrl == null)
            {
                messagemodel.message = "The uploaded file is empty";
                return messagemodel;
            }
            else
            {
                string filePathtoSave = "";
                string filename = "";
                var existingUser = _healthindexdbcontext.Questions.Where(x => x.QuestionId == upload.QuestionId).FirstOrDefault();
                if (existingUser != null)
                {
                    var serverPath = AppDomain.CurrentDomain.BaseDirectory + "/Resource/QuestionImage/";
                    string extn = System.IO.Path.GetExtension(upload.QuestionImageUrl.FileName);
                    var filePath = Path.Combine(serverPath + upload.QuestionImageUrl.FileName);
                    new FileInfo(filePath).Directory?.Create();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        upload.QuestionImageUrl.CopyToAsync(stream);
                    }
                    filePathtoSave ="/Resource/QuestionImage/" + upload.QuestionImageUrl.FileName;
                    filename=Path.GetFileName(filePathtoSave);
                    existingUser.QuestionImageUrl = filePathtoSave;
                    existingUser.QuestionImageName = filename;
                    messagemodel.message = "Question Image Uploaded Successfully";
                    _healthindexdbcontext.SaveChanges();
                }
                return messagemodel;
                  
            }// Save 

        }
    }
    

}
