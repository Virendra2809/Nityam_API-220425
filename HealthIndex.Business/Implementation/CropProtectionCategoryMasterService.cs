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
   public class CropProtectionCategoryMasterService : ICropProtectionCategoryMasterService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CropProtectionCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<CropProtectionCategoryMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var cropProtectionCategoryMasterModelList = new List<CropProtectionCategoryMasterModel>();
            var cropProtectionCategoryMasterModelListEntity = (from CropProtectionCategoryMaster in _agriContext.CropProtectionCategoryMasters
                                                               where CropProtectionCategoryMaster.DeleteStatus == false
                                                               select new
                                                               {

                                                                   CropProtectionCategoryMaster.CropProtectionCategoryId,
                                                                   CropProtectionCategoryMaster.CropProtectionCategoryName,
                                                                   CropProtectionCategoryMaster.Description,
                                                                   CropProtectionCategoryMaster.SeqNo,
                                                                   CropProtectionCategoryMaster.EnteredBy,
                                                                   CropProtectionCategoryMaster.EnteredDate,
                                                                   CropProtectionCategoryMaster.ChangedBy,
                                                                   CropProtectionCategoryMaster.ChangedDate,
                                                                   CropProtectionCategoryMaster.DeleteStatus,

                                                               }
                                  ).ToList();
            if (cropProtectionCategoryMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropProtectionCategoryMasterModelListEntity)
            {
                var model = new CropProtectionCategoryMasterModel();
                model.CropProtectionCategoryId = item.CropProtectionCategoryId;
                model.CropProtectionCategoryName = item.CropProtectionCategoryName;
                model.Description = item.Description;
                model.SeqNo = item.SeqNo;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                cropProtectionCategoryMasterModelList.Add(model);
            }
            return cropProtectionCategoryMasterModelList;
        }

        public string Add(CropProtectionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.CropProtectionCategoryMasters.Any(x => x.SeqNo == model.SeqNo);
                if (existing)
                {
                    return null;
                }
                else
                {
                    var CropProtectionCategoryMasterEntity = new CropProtectionCategoryMaster();

                    CropProtectionCategoryMasterEntity.CropProtectionCategoryId = model.CropProtectionCategoryId;
                    CropProtectionCategoryMasterEntity.CropProtectionCategoryName = model.CropProtectionCategoryName;
                    CropProtectionCategoryMasterEntity.Description = model.Description;
                    CropProtectionCategoryMasterEntity.SeqNo = model.SeqNo;
                    CropProtectionCategoryMasterEntity.EnteredBy = model.EnteredBy;
                    CropProtectionCategoryMasterEntity.EnteredDate = DateTime.Now;
                    CropProtectionCategoryMasterEntity.DeleteStatus = model.DeleteStatus;

                    _agriContext.CropProtectionCategoryMasters.Add(CropProtectionCategoryMasterEntity);
                    _agriContext.SaveChanges();

                    message = "  CropProtectionCategoryMaster Added Succesfully ";
                }
                return message;
            }

        }

        CropProtectionCategoryMasterModel ICropProtectionCategoryMasterService.GetById(long CropProtectionCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropProtectionCategoryMasterEntity = _agriContext.CropProtectionCategoryMasters.FirstOrDefault(x => x.CropProtectionCategoryId == CropProtectionCategoryId && !x.DeleteStatus);
            if (cropProtectionCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropProtectionCategoryMasterModel
            {
                CropProtectionCategoryId= cropProtectionCategoryMasterEntity.CropProtectionCategoryId,
                CropProtectionCategoryName= cropProtectionCategoryMasterEntity.CropProtectionCategoryName,
                Description = cropProtectionCategoryMasterEntity.Description,
                SeqNo= cropProtectionCategoryMasterEntity.SeqNo,
                EnteredBy = cropProtectionCategoryMasterEntity.EnteredBy,
                EnteredDate = cropProtectionCategoryMasterEntity.EnteredDate,
                ChangedBy = cropProtectionCategoryMasterEntity.ChangedBy,
                ChangedDate = cropProtectionCategoryMasterEntity.ChangedDate,
                DeleteStatus = cropProtectionCategoryMasterEntity.DeleteStatus,
            };

        }
    

        public bool Put(CropProtectionCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var CropProtectionCategoryId = Convert.ToInt32(model.CropProtectionCategoryId);
            var CropProtectionCategoryMasterEntity = _agriContext.CropProtectionCategoryMasters.FirstOrDefault(x => x.CropProtectionCategoryId == CropProtectionCategoryId && !x.DeleteStatus);
            if (CropProtectionCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                CropProtectionCategoryMasterEntity.CropProtectionCategoryId = model.CropProtectionCategoryId;
                CropProtectionCategoryMasterEntity.CropProtectionCategoryName = model.CropProtectionCategoryName;
                CropProtectionCategoryMasterEntity.Description = model.Description;
                CropProtectionCategoryMasterEntity.SeqNo = model.SeqNo;
                CropProtectionCategoryMasterEntity.ChangedBy = model.ChangedBy;
                CropProtectionCategoryMasterEntity.ChangedDate = DateTime.Now;
                CropProtectionCategoryMasterEntity.DeleteStatus = model.DeleteStatus;
           

                _agriContext.SaveChanges();
                return true;
            }
        }
        public string Delete(long CropProtectionCategoryId, ref ErrorResponseModel errorResponseModel)
        {

         var message = "";
        errorResponseModel = new ErrorResponseModel();
            CropProtectionCategoryMasterModel model = new CropProtectionCategoryMasterModel();
        var cropProtectionCategoryEntity = _agriContext.CropProtectionCategoryMasters.FirstOrDefault(x => x.CropProtectionCategoryId == CropProtectionCategoryId);
            if (cropProtectionCategoryEntity != null)
            {
                cropProtectionCategoryEntity.DeleteStatus = true;
                cropProtectionCategoryEntity.ChangedBy = model.ChangedBy;
                cropProtectionCategoryEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

    }




}



