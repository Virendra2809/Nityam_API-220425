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
    public class FirmDetailsService : IFirmDetailService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public FirmDetailsService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
        public List<FirmDetailModel> GetFirmDetails(ref ErrorResponseModel errorResponseModel)
        {
            var firmdetailModelList = new List<FirmDetailModel>();
            errorResponseModel = new ErrorResponseModel();
            var firmdetailEntityList = _healthindexdbcontext.FirmDetails.Where(x=>x.DeleteStatus==false).ToList();
            if (firmdetailEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Firm Details not found";
                return null;
            }

            firmdetailEntityList.ForEach(item =>
            {
                firmdetailModelList.Add(new FirmDetailModel
                {
                     FirmId=item.FirmId,
                     FirmName=item.FirmName,
                     FirmRegNumber=item.FirmRegNumber,
                     FirmRegDate=item.FirmRegDate,
                     FirmBranchName=item.FirmBranchName,
                     FirmOfficeAddress=item.FirmOfficeAddress,
                     FirmLogo=item.FirmLogo,
                     FirmPhoneNumber=item.FirmPhoneNumber,
                     FirmFaxNumber=item.FirmFaxNumber,
                     FirmEmailIid=item.FirmEmailIid,
                     MailPassword=item.MailPassword,
                     IsFederation=item.IsFederation,
                     FirmConnectionPath=item.FirmConnectionPath,
                     ParentFirmId=item.ParentFirmId,
                     EnteredBy=item.EnteredBy,
                     EnteredDate=item.EnteredDate,
                     ChangedBy=item.ChangedBy,
                     ChangedDate=item.ChangedDate,
                     DeleteStatus=item.DeleteStatus,
                });
            });
            return firmdetailModelList;
        }
    }
}
