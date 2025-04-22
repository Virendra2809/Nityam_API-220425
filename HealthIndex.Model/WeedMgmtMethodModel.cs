using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class WeedMgmtMethodModel
    {
        //public WeedMgmtMethodModel()
        //{
        //    this.WeedImages = new List<WeedImageModel>();

        //}
        public int WeedMgmtMethodId { get; set; }
        public int? WeedMgmtCategoryId { get; set; }
        public string WeedMgmtCategoryName { get; set; }
        public string MethodImageName { get; set; }
        public string ImageUrl { get; set; }

        public string WeedMgmtMethodName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public IFormFile WeedFile { get; set; }

        //  public List<WeedImageModel> WeedImages { get; set; }


    }


    //public class WeedImageModel
    //{
    //    public string MethodImageName { get; set; }
    //    public string ImageUrl { get; set; }

    //}
}