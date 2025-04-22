using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ProductImagesModel
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage1 { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ProductImage { get; set; }

    }
    public class ProductImageModel
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ProductImage1 { get; set; }


    }


}
