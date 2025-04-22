using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
   public class SeasonMasterService:ISeasonMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;
        public SeasonMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<SeasonMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var SeasonModelList = new List<SeasonMasterModel>();
            var SeasonListEntity = _agriContext.SeasonMasters.Where(x => x.DeleteStatus == false).ToList();
            if (SeasonListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in SeasonListEntity)
            {
                var model = new SeasonMasterModel();
                DateTime startmonth = Convert.ToDateTime(item.SeasonStartMonth);
                DateTime endmonth = Convert.ToDateTime(item.EndMonth);

                model.SeasonId = item.SeasonId;
                model.SeasonName = item.SeasonName;
            
                model.SeasonStartMonth = startmonth.ToString("dd-MM-yyyy");
                model.EndMonth = endmonth.ToString("dd-MM-yyyy");
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = Convert.ToBoolean(item.DeleteStatus);
                SeasonModelList.Add(model);
            }
            return SeasonModelList;
        }

        public SeasonMasterModel GetById(long SeasonId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            
            var SeasonEntity = _agriContext.SeasonMasters.FirstOrDefault(x => x.SeasonId == SeasonId && x.DeleteStatus== false);
            if (SeasonEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            string startDate = (SeasonEntity.SeasonStartMonth).ToString();
            string EndDate = (SeasonEntity.EndMonth).ToString();
            

            return new SeasonMasterModel
            {

                SeasonId = SeasonEntity.SeasonId,
                SeasonName = SeasonEntity.SeasonName,
                SeasonStartMonth = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"),
                EndMonth = Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd"),
                EnteredBy = SeasonEntity.EnteredBy,
                EnteredDate = SeasonEntity.EnteredDate,
                ChangedBy = SeasonEntity.ChangedBy,
                ChangedDate = SeasonEntity.ChangedDate,
                DeleteStatus= (bool)SeasonEntity.DeleteStatus
                
            };
        }

        public string Add(SeasonMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existing = _agriContext.SeasonMasters.Any(x => x.SeasonName == model.SeasonName && x.SeasonStartMonth==Convert.ToDateTime(model.SeasonStartMonth) && x.EndMonth==Convert.ToDateTime(model.EndMonth) );
            if (existing)
            {
                message = "Season Already Exist";
            }
            else
            {
                SeasonMaster seasonEntity = new SeasonMaster();
                seasonEntity.SeasonId = model.SeasonId;
                seasonEntity.SeasonName = model.SeasonName;
                seasonEntity.SeasonStartMonth = Convert.ToDateTime(model.SeasonStartMonth);
                seasonEntity.EndMonth = Convert.ToDateTime(model.EndMonth);
                seasonEntity.EnteredBy = model.EnteredBy;
                seasonEntity.EnteredDate = DateTime.Now;
                seasonEntity.DeleteStatus = model.DeleteStatus;
                _agriContext.SeasonMasters.Add(seasonEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool Put(SeasonMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var SeasonId = model.SeasonId;
            var seasonEntity = _agriContext.SeasonMasters.FirstOrDefault(x => x.SeasonId == SeasonId && x.DeleteStatus==false);
            if (seasonEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                seasonEntity.SeasonId = model.SeasonId;
                seasonEntity.SeasonName = model.SeasonName;
                seasonEntity.SeasonStartMonth =Convert.ToDateTime(model.SeasonStartMonth);
                seasonEntity.EndMonth = Convert.ToDateTime(model.EndMonth);
                seasonEntity.ChangedBy = model.ChangedBy;
                seasonEntity.ChangedDate = DateTime.Now;
                seasonEntity.DeleteStatus = false;
                _agriContext.SaveChanges();
                return true;
            }
        }
 

        public string Delete(long SeasonId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            SeasonMasterModel model = new SeasonMasterModel();
            var seasonEntity = _agriContext.SeasonMasters.FirstOrDefault(x => x.SeasonId == SeasonId);
            if (seasonEntity != null)
            {
                seasonEntity.DeleteStatus = true;
                seasonEntity.ChangedBy = model.EnteredBy;
                seasonEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        
    }
}

