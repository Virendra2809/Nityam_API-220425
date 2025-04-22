using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class SoilTestReportModel
    {
        public int SoilTestReportId { get; set; }
        public int? FarmerCropDetailId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Ph { get; set; }
        public decimal? Ec { get; set; }
        public decimal? Nitrogen { get; set; }
        public decimal? Phosphorus { get; set; }
        public decimal? Potassium { get; set; }
        public decimal? Zink { get; set; }
        public decimal? Iron { get; set; }
        public decimal? Manganese { get; set; }
        public decimal? Copper { get; set; }
        public decimal? Boron { get; set; }
        public decimal? OcPercentage { get; set; }
        public string SoilTexture { get; set; }
        public bool? DeleteStatus { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
