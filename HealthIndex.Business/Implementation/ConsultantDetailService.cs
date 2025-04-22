using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Implementation
{
    public class ConsultantDetailService : IConsultantDetailService
    {

        HealthIndexDbContext _healthindexdbcontext;
        public ConsultantDetailService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
        public MessageModel AddConsultantDetail(ConsultantDetailModel model, ref ErrorResponseModel errorResponseModel)
        {
            MessageModel messageModel = new MessageModel();
            var existingconsultant = _healthindexdbcontext.ConsultantDetails.Any(x => x.ConsultantId == model.ConsultantId);
            if (existingconsultant)
            {
                messageModel.message = GlobalConstants.ExistingConsultant;
            }
            else
            {
                var consultantEntity = new ConsultantDetail();
                consultantEntity.ConsultantId = model.ConsultantId;
                consultantEntity.ConsultingCategoryId = model.ConsultingCategoryId;
                consultantEntity.ConsultantName = model.ConsultantName;
                consultantEntity.MobileNo = model.MobileNo;
                consultantEntity.DeleteStatus = false;
                consultantEntity.EmailId = model.EmailId;
                consultantEntity.OrgName = model.OrgName;
                consultantEntity.OrgAddress = model.OrgAddress;
                _healthindexdbcontext.ConsultantDetails.Add(consultantEntity);
                _healthindexdbcontext.SaveChanges();
                messageModel.message = GlobalConstants.ConsultantSavedSuccessfully;
            }
            return messageModel;
        }

        public List<ConsultantDetailModel> GetConsultantDetail(HealthParameter parameter,ref ErrorResponseModel errorResponseModel)
        {
            var consultantModelList = new List<ConsultantDetailModel>();
            errorResponseModel = new ErrorResponseModel();
            var firmdetailEntityList = (from consult in _healthindexdbcontext.ConsultantDetails
                                        join master in _healthindexdbcontext.ConsultingCategories on
                                        consult.ConsultingCategoryId equals master.ConsultingCategoryId
                                        where
                                        consult.DeleteStatus == false
                                        select new
                                        {
                                            consult.ConsultantId,
                                            consult.ConsultantName,
                                            consult.ConsultingCategoryId,
                                            master.ConsultingCategory1,
                                            consult.DeleteStatus,
                                            consult.OrgAddress,
                                            consult.OrgName,
                                            consult.MobileNo,
                                            consult.EmailId
                                        }).Skip((parameter.PageNumber - 1) * parameter.PageSize)
                                        .Take(parameter.PageSize).ToList();

            if (firmdetailEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Consultant Details not found";
                return null;
            }

            firmdetailEntityList.ForEach(item =>
            {
                consultantModelList.Add(new ConsultantDetailModel
                {
                    ConsultantId = item.ConsultantId,
                    ConsultantName = item.ConsultantName,
                    ConsultingCategoryId = item.ConsultingCategoryId,
                    MobileNo = item.MobileNo,
                    EmailId = item.EmailId,
                    OrgAddress = item.OrgAddress,
                    OrgName = item.OrgName,
                    DeleteStatus = item.DeleteStatus,
                    ConsultingCategory1 = item.ConsultingCategory1,
                });
            });
            return consultantModelList;
        }

        public List<consultingCategory> GetConsultingCategories(ref ErrorResponseModel errorResponseModel)
        {
            var consultantModelList = new List<consultingCategory>();
            errorResponseModel = new ErrorResponseModel();
            var EntityList = _healthindexdbcontext.ConsultingCategories.ToList();
            if (EntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Consulting Categories not found";
                return null;
            }
            EntityList.ForEach(item =>
            {
                consultantModelList.Add(new consultingCategory
                {
                    ConsultingCategoryId = item.ConsultingCategoryId,
                    ConsultingCategory1 = item.ConsultingCategory1,
                });
            });
            return consultantModelList;
        }


        public string DeleteConsultantDetails(int consultantId, ref ErrorResponseModel errorResponseModel)
        {
            string message = "";
            var Entity = _healthindexdbcontext.ConsultantDetails.FirstOrDefault(x => x.ConsultantId == consultantId);


            if (Entity != null)
            {
                Entity.DeleteStatus = true;
                _healthindexdbcontext.SaveChanges();
                message = "Consultant Details Deleted Successfully";
            }
            return message;
        }

        public string EditConsultantDetails(ConsultantDetailModel consultantDetail)
        {
            var message = "";
            var MasterEntity = _healthindexdbcontext.ConsultantDetails.FirstOrDefault(x => x.ConsultantId == consultantDetail.ConsultantId);
            if (MasterEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {

                MasterEntity.ConsultantId = consultantDetail.ConsultantId;
                MasterEntity.ConsultingCategoryId = consultantDetail.ConsultingCategoryId;
                MasterEntity.ConsultantName = consultantDetail.ConsultantName;
                MasterEntity.MobileNo = consultantDetail.MobileNo;
                MasterEntity.EmailId = consultantDetail.EmailId;
                MasterEntity.OrgAddress = consultantDetail.OrgAddress;
                MasterEntity.OrgName = consultantDetail.OrgName;
                MasterEntity.DeleteStatus = false;
                _healthindexdbcontext.ConsultantDetails.Update(MasterEntity);
                _healthindexdbcontext.SaveChanges();
                message = GlobalConstants.ConsultantDetailsUpdateSuccessfully;
            }
            return message;
        }

        public List <ConsultantDetailModel> GetConsutltantDetailsById(long consultantId, ref ErrorResponseModel errorResponseModel)
        {
            var list=new List<ConsultantDetailModel>();
            errorResponseModel = new ErrorResponseModel();
            var userEntity = (from detail in _healthindexdbcontext.ConsultantDetails
                              join
                               consultant in _healthindexdbcontext.ConsultingCategories on detail.ConsultingCategoryId equals consultant.ConsultingCategoryId
                              where detail.ConsultantId == consultantId && detail.DeleteStatus == false
                              select new
                              {
                                  detail.ConsultantId,
                                  detail.ConsultingCategoryId,
                                  detail.MobileNo,
                                  detail.EmailId,
                                  detail.OrgAddress,
                                  detail.OrgName,
                                  detail.ConsultantName,
                                  detail.DeleteStatus,
                                  consultant.ConsultingCategory1
                              }
                              ).ToList();
            if (userEntity.Count==0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "consultant Details not found";
                return null;
            }
            userEntity.ForEach(item =>
            {
                list.Add(new ConsultantDetailModel
                {
                    ConsultantId = item.ConsultantId,
                    ConsultantName = item.ConsultantName,
                    ConsultingCategoryId = item.ConsultingCategoryId,
                    MobileNo = item.MobileNo,
                    EmailId = item.EmailId,
                    OrgAddress = item.OrgAddress,
                    OrgName = item.OrgName,
                    DeleteStatus = item.DeleteStatus,
                    ConsultingCategory1 = item.ConsultingCategory1,
                });
            });
            return list;
        }
        public ConsultantDetailExpert GetExpertConsultantByCode(string consultantcode, ref ErrorResponseModel errorResponseModel)
        {
            var list = new ConsultantDetailExpert();
            errorResponseModel = new ErrorResponseModel();
            var consultantDetail = (from detail in _healthindexdbcontext.ConsultantDetails
                                    join consultant in _healthindexdbcontext.ConsultingCategories on detail.ConsultingCategoryId equals consultant.ConsultingCategoryId
                                    join Quetionmaster in _healthindexdbcontext.QuestionMasters on detail.ConsultantId equals Quetionmaster.ConsultantId
                                    where detail.Consultantcode == consultantcode && detail.DeleteStatus == false
                                    select new ConsultantDetailExpert
                                    {
                                        ConsultantId = detail.ConsultantId,
                                        ConsultingCategoryId = detail.ConsultingCategoryId,
                                        MobileNo = detail.MobileNo,
                                        EmailId = detail.EmailId,
                                        OrgAddress = detail.OrgAddress,
                                        OrgName = detail.OrgName,
                                        ConsultantName = detail.ConsultantName,
                                        DeleteStatus = detail.DeleteStatus,
                                        ConsultingCategory1 = consultant.ConsultingCategory1,
                                        QuestionMasterId = Quetionmaster.QuestionMasterId
                                    }).FirstOrDefault();

            // Handle null case if needed
            if (consultantDetail == null)
            {
                // Handle the case where no data is found
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "consultant Details not found";
                return null;
            }
            else
            {

                list = consultantDetail;
                // Your logic to use the 'consultantDetail' object
            }
            
            return list;
        }
    }
}
