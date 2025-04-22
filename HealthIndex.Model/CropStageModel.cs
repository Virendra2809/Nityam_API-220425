using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropStageModel
    {
        public int CropStageId { get; set; }
        public string CropStageName { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }
        public int? SeqNo { get; set; }
        public bool? DeleteStatus { get; set; }

    }
    public class CropStageList
    {
        public int? CropId { get; set; }
        public string CropName { get; set; }
    }

    public class StageModel
    {
        public int CropStageId { get; set; }
        public string CropStageName { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }
        public int? SeqNo { get; set; }

    }
}
