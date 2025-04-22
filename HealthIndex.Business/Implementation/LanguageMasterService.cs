using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class LanguageMasterService:ILanguageMasterservice
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public LanguageMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<LanguageModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var LanguageModelList = new List<LanguageModel>();
            var LanguageListEntity = (from LanguageMaster in _agriContext.LanguageMasters
                                   where LanguageMaster.DeleteStatus == false
                                   select new
                                   {
                                       LanguageMaster.LanguageId,
                                       LanguageMaster.LanguageName,
                                       LanguageMaster.EnteredBy,
                                       LanguageMaster.EnteredDate,
                                       LanguageMaster.ChangedBy,
                                       LanguageMaster.ChangedDate,
                                       LanguageMaster.DeleteStatus,
                                   }
                                  ).ToList();
            if (LanguageListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in LanguageListEntity)
            {
                var model = new LanguageModel();
                model.LanguageId = item.LanguageId;
                model.LanguageName = item.LanguageName;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                LanguageModelList.Add(model);
            }
            return LanguageModelList;



        }
        LanguageModel ILanguageMasterservice.GetById(long LanguageId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var LanguageEntity = _agriContext.LanguageMasters.FirstOrDefault(x => x.LanguageId == LanguageId && !x.DeleteStatus);
            if (LanguageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new LanguageModel
            {
                LanguageId = LanguageEntity.LanguageId,
                LanguageName = LanguageEntity.LanguageName,
                EnteredBy = LanguageEntity.EnteredBy,
                EnteredDate = LanguageEntity.EnteredDate,
                ChangedBy = LanguageEntity.ChangedBy,
                ChangedDate = LanguageEntity.ChangedDate,
                DeleteStatus = LanguageEntity.DeleteStatus,
            };

        }


        public string Add(LanguageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.LanguageMasters.Any(x => x.LanguageId == model.LanguageId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else

            {
                var LanguageEntity = new LanguageMaster();

                LanguageEntity.LanguageId = model.LanguageId;
                LanguageEntity.LanguageName = model.LanguageName;
                LanguageEntity.EnteredBy = model.EnteredBy;
                LanguageEntity.EnteredDate = DateTime.Now;
                LanguageEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.LanguageMasters.Add(LanguageEntity);
                _agriContext.SaveChanges();

                message = "Language Added Succesfully ";
            }
            return message;
        }

        public bool Put(LanguageModel model, ref ErrorResponseModel errorResponseModel)
        {
            var LanguageId = Convert.ToInt32(model.LanguageId);
            var LanguageEntity = _agriContext.LanguageMasters.FirstOrDefault(x => x.LanguageId == LanguageId && !x.DeleteStatus);
            if (LanguageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                LanguageEntity.LanguageId = model.LanguageId;
                LanguageEntity.LanguageName = model.LanguageName;
                LanguageEntity.ChangedBy = model.ChangedBy;
                LanguageEntity.ChangedDate = DateTime.Now;
                LanguageEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long LanguageId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            LanguageModel model = new LanguageModel();
            var languageMasterEntity = _agriContext.LanguageMasters.FirstOrDefault(x => x.LanguageId == LanguageId);
            if (languageMasterEntity != null)
            {
                languageMasterEntity.DeleteStatus = true;
                languageMasterEntity.ChangedBy = model.ChangedBy;
                languageMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }
}

