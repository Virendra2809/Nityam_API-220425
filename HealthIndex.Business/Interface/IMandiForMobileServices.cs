using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
  public interface IMandiForMobileServices
    {
        List<MandiMasterModel> GetAllMandiWithCity(int CityId);
        List<AgriProductPriceDetailModel> GetAllMandi(long MandiId, long AgriProductId, DateTime Date);

        List<AgriProductMasterModel> GetAllAgriProductMaster(int MandiId);

    }
}
