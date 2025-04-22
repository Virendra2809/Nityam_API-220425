using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IConsultantDetailService
    {
        List<ConsultantDetailModel> GetConsultantDetail(HealthParameter parameter,ref ErrorResponseModel errorResponseModel);
        List< ConsultantDetailModel> GetConsutltantDetailsById(long consultantId, ref ErrorResponseModel errorResponseModel);
        public MessageModel AddConsultantDetail(ConsultantDetailModel model, ref ErrorResponseModel errorResponseModel);
        List<consultingCategory> GetConsultingCategories(ref ErrorResponseModel errorResponseModel);
        public string EditConsultantDetails(ConsultantDetailModel consultantDetail );
        ConsultantDetailExpert GetExpertConsultantByCode(string consultantcode, ref ErrorResponseModel errorResponseModel);
        public string DeleteConsultantDetails(int consultantId, ref ErrorResponseModel errorResponseModel);

    }
}
