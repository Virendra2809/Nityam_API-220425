using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class ProductBrandMasterModel
    {
        public int ProductBrandId { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductBrandName { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string ProductCategoryName { get; set; }
        public bool DeleteStatus { get; set; }

    }

    public class MenuProductCategoryModel
    {
        public MenuProductCategoryModel()
        {
            this.productBrandList = new List<MenuProductBrandModel>();
        }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public List<MenuProductBrandModel> productBrandList { get; set; }
    }
    public class MenuProductBrandModel
    {
        
        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
    }
   
}
