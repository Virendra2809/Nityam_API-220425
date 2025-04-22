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
   public class CropProtectionSubCategoryMasterService:ICropProtectionSubCategoryMaster
    {

        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CropProtectionSubCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<CropProtectionSubCategoryMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var userExist = _agriContext.CropProtectionSubCategoryMasters.FirstOrDefault(x =>x.DeleteStatus == false);

            var cropProtectionSubCategoryMasterModelList = new List<CropProtectionSubCategoryMasterModel>();
            var cropProtectionSubCategoryMasterModelListEntity = (from CropProtectionSubCategoryMaster in _agriContext.CropProtectionSubCategoryMasters
                                                                  join CropProtectionCategoryMaster in _agriContext.CropProtectionCategoryMasters
                                                                  on CropProtectionSubCategoryMaster.CropProtectionCategoryId equals CropProtectionCategoryMaster.CropProtectionCategoryId
                                                                  where CropProtectionSubCategoryMaster.DeleteStatus == false

                                                                  select new
                                                                  {

                                                                      CropProtectionSubCategoryMaster.CropProtectionSubCategoryId,
                                                                      CropProtectionCategoryMaster.CropProtectionCategoryId,
                                                                      CropProtectionSubCategoryMaster.CropProtectionSubCategoryName,
                                                                      CropProtectionSubCategoryMaster.Description,
                                                                      CropProtectionSubCategoryMaster.EnteredBy,
                                                                      CropProtectionSubCategoryMaster.EnteredDate,
                                                                      CropProtectionSubCategoryMaster.ChangedBy,
                                                                      CropProtectionSubCategoryMaster.ChangedDate,
                                                                      CropProtectionSubCategoryMaster.DeleteStatus,
                                                                      CropProtectionCategoryMaster.CropProtectionCategoryName

                                                               }
                                  ).ToList();
            if (cropProtectionSubCategoryMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropProtectionSubCategoryMasterModelListEntity)
            {
                var model = new CropProtectionSubCategoryMasterModel();
                model.CropProtectionSubCategoryId = item.CropProtectionSubCategoryId;
                model.CropProtectionCategoryId = (int)(int?)item.CropProtectionCategoryId;
                model.CropProtectionSubCategoryName = item.CropProtectionSubCategoryName;
                model.Description = item.Description;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.CropProtectionCategoryName = item.CropProtectionCategoryName;

                cropProtectionSubCategoryMasterModelList.Add(model);
            }
            return cropProtectionSubCategoryMasterModelList;
        }

        public string Add(CropProtectionSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.CropProtectionSubCategoryMasters.Any(x => x.CropProtectionSubCategoryId == model.CropProtectionSubCategoryId);
                if (existing)
                {
                    message = GlobalConstants.NotFoundMessage;
                }
                else
                {
                    var CropProtectionSubCategoryMasterEntity = new CropProtectionSubCategoryMaster();

                    CropProtectionSubCategoryMasterEntity.CropProtectionCategoryId = Convert.ToInt32(model.CropProtectionCategoryId);
                    CropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryId = model.CropProtectionSubCategoryId;
                    CropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryName = model.CropProtectionSubCategoryName;
                    CropProtectionSubCategoryMasterEntity.Description = model.Description;
                    CropProtectionSubCategoryMasterEntity.EnteredBy = model.EnteredBy;
                    CropProtectionSubCategoryMasterEntity.EnteredDate = DateTime.Now;;
                    CropProtectionSubCategoryMasterEntity.DeleteStatus = model.DeleteStatus;

                    _agriContext.CropProtectionSubCategoryMasters.Add(CropProtectionSubCategoryMasterEntity);
                    _agriContext.SaveChanges();

                    message = "  CropProtectionSubCategoryMaster Added Succesfully ";
                }
                return message;
            }

        }

        CropProtectionSubCategoryMasterModel ICropProtectionSubCategoryMaster.GetById(long CropProtectionSubCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropProtectionSubCategoryMasterEntity = (from CropProtectionSubCategoryMaster in _agriContext.CropProtectionSubCategoryMasters
                                                         join CropProtectionCategoryMaster in _agriContext.CropProtectionCategoryMasters
                                                         on CropProtectionSubCategoryMaster.CropProtectionCategoryId equals CropProtectionCategoryMaster.CropProtectionCategoryId
                                                         where CropProtectionSubCategoryMaster.DeleteStatus == false && CropProtectionSubCategoryMaster.CropProtectionSubCategoryId==CropProtectionSubCategoryId

                                                         select new
                                                         {

                                                             CropProtectionSubCategoryMaster.CropProtectionSubCategoryId,
                                                             CropProtectionCategoryMaster.CropProtectionCategoryId,
                                                             CropProtectionSubCategoryMaster.CropProtectionSubCategoryName,
                                                             CropProtectionSubCategoryMaster.Description,
                                                             CropProtectionSubCategoryMaster.EnteredBy,
                                                             CropProtectionSubCategoryMaster.EnteredDate,
                                                             CropProtectionSubCategoryMaster.ChangedBy,
                                                             CropProtectionSubCategoryMaster.ChangedDate,
                                                             CropProtectionSubCategoryMaster.DeleteStatus,
                                                             CropProtectionCategoryMaster.CropProtectionCategoryName

                                                         }
                                  ).FirstOrDefault();
            if (cropProtectionSubCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropProtectionSubCategoryMasterModel
            {
                CropProtectionSubCategoryId = cropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryId,
                CropProtectionCategoryId = cropProtectionSubCategoryMasterEntity.CropProtectionCategoryId,
                CropProtectionSubCategoryName = cropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryName,
                Description = cropProtectionSubCategoryMasterEntity.Description,
                EnteredBy = cropProtectionSubCategoryMasterEntity.EnteredBy,
                EnteredDate = cropProtectionSubCategoryMasterEntity.EnteredDate,
                ChangedBy = cropProtectionSubCategoryMasterEntity.ChangedBy,
                ChangedDate = cropProtectionSubCategoryMasterEntity.ChangedDate,
                DeleteStatus = cropProtectionSubCategoryMasterEntity.DeleteStatus,
                CropProtectionCategoryName=cropProtectionSubCategoryMasterEntity.CropProtectionCategoryName,
            };

        }


        public bool Put(CropProtectionSubCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var CropProtectionSubCategoryId = Convert.ToInt32(model.CropProtectionSubCategoryId);
            var CropProtectionSubCategoryMasterEntity = _agriContext.CropProtectionSubCategoryMasters.FirstOrDefault(x => x.CropProtectionSubCategoryId == CropProtectionSubCategoryId && !x.DeleteStatus);
            if (CropProtectionSubCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                CropProtectionSubCategoryMasterEntity.CropProtectionCategoryId = Convert.ToInt32( model.CropProtectionCategoryId);
                CropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryId = model.CropProtectionSubCategoryId;
                CropProtectionSubCategoryMasterEntity.CropProtectionSubCategoryName = model.CropProtectionSubCategoryName;
                CropProtectionSubCategoryMasterEntity.Description = model.Description;
                CropProtectionSubCategoryMasterEntity.ChangedBy = model.ChangedBy;
                CropProtectionSubCategoryMasterEntity.ChangedDate = DateTime.Now;
                CropProtectionSubCategoryMasterEntity.DeleteStatus = model.DeleteStatus;


                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long CropProtectionSubCategoryId, ref ErrorResponseModel errorResponseModel)
        {

            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropProtectionSubCategoryMasterModel model = new CropProtectionSubCategoryMasterModel();
            var cropProtectionSubCategoryEntity = _agriContext.CropProtectionSubCategoryMasters.FirstOrDefault(x => x.CropProtectionSubCategoryId == CropProtectionSubCategoryId);
            if (cropProtectionSubCategoryEntity != null)
            {
                cropProtectionSubCategoryEntity.DeleteStatus = true;
                cropProtectionSubCategoryEntity.ChangedBy = model.ChangedBy;
                cropProtectionSubCategoryEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }

}

