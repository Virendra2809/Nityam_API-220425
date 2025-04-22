using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IStateService
    {

        object Value { get; }
        List<StateModel> GetAll();
        string Add(StateModel model, ref ErrorResponseModel errorResponseModel);
        StateModel GetById(long StateId, ref ErrorResponseModel errorResponseModel);
        public bool Put(StateModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long StateId, ref ErrorResponseModel errorResponseModel);

    }
}
