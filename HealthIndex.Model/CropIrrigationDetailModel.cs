using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
   public class CropIrrigationDetailModel
    {
        //public CropIrrigationDetailModel()
        //{
        //    this.quantities = new List<IrrigationModel>(); 

        //}
        public int IrrigationDetailId { get; set; }
        public int? SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }
        //public List<IrrigationModel> quantities { get; set; }
        public int? CropStageId { get; set; }
        public string CropStageName { get; set; }

        public int? IrrigationDay1 { get; set; }
        public int[] IrrigationDay { get; set; }

        public List<IrrigationModel> irrigation { get; set; }

        public bool? DeleteStatus { get; set; }

    }

    public class IrrigationModel
    {
        public int IrrigationDay { get; set; }
        public int? IrrigationDetailId { get; set; }

    }


    public class irrigationModel
    {
        public irrigationModel()
        {
            this.IrrigationDays = new List<IrrigationDayModel>();

        }
        public int? SoilTypeId { get; set; }
        public string SoilTypeName { get; set; }
        public int? CropId { get; set; }
        public string CropName { get; set; }
        public int? CropStageId { get; set; }
        public string CropStageName { get; set; }
        public List<IrrigationDayModel> IrrigationDays { get; set; }


    }
    public class IrrigationDayModel
    {
        public int? IrrigationDay { get; set; }

    }
}
