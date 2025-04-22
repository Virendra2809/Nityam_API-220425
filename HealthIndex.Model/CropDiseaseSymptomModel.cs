using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
  public  class CropDiseaseSymptomModel
    {
        public int CropDiseaseSymptomId { get; set; }
        public int? CropDiseaseId { get; set; }
        public string Symptom { get; set; }
        
    }
}
