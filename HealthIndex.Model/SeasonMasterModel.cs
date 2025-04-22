using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class SeasonMasterModel
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        // [DisplayFormat(DataFormatString = "{0:MMMM}")]
  
        public string SeasonStartMonth { get; set; }
      
        public string EndMonth { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

    }
}
