using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class CityMasterModel
    {

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string StateName { get; set; }
        public int LoggedUserId { get; set; }

    }
}
