using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class MandiMasterModel
    {
        public int MandiId { get; set; }
        public string MandiName { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public bool? IsWebApi { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public string CityName { get; set; }

    }
}
