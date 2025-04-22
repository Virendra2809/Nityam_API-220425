using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class FarmerCropDetailsModel
    {

        public int FarmerCropDetailId { get; set; }
        public string FarmName { get; set; }

        public int FarmerId { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? SowingDate { get; set; }
        public decimal? SowingAreaInHect { get; set; }
        public decimal? SowingAreaInAcre { get; set; }
        public decimal? SowingAreaInAres { get; set; }
        public int? SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public string CropImage { get; set; }
        public string CropName { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public Boolean AvailableDetails { get; set; }
        public int FarmerSelectedCropId { get; set; }
        public int FarmerCropId1 { get; set; }
        public int[] FarmerCropId { get; set; }

    }
    public class FarmerCropModel
    {

        public int FarmerCropDetailId { get; set; }
        public int FarmerId { get; set; }
        public int CropId { get; set; }
        public string CropImage { get; set; }
        public string CropName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }


    public class FarmAddModel
    {

        public int FarmerCropDetailId { get; set; }
        public string FarmName { get; set; }
        public DateTime? SowingDate { get; set; }
        public decimal? SowingAreaInHect { get; set; }
        public decimal? SowingAreaInAcre { get; set; }
        public decimal? SowingAreaInAres { get; set; }
        public int? SoilTypeId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? FarmerSelectedCropId { get; set; }
        public bool DeleteStatus { get; set; }


    }



}
